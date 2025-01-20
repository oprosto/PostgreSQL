using System;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class ItemCivilUI : MonoBehaviour
{
    [SerializeField]
    public TMPro.TMP_InputField _seriesField, _numberField, _fullNameField, _placeOfResidentField;
    [SerializeField]
    public GameObject _updateBtn, _saveBtn, _cancelBtn, _deleteBtn;
    [SerializeField]
    private Image _background;

    public Color _color;
    public Citizen _citizen;

    public static ItemCivilUI _curChange = null;
    public static string[] _curUpdatedValues = new string[4];

    public void Init(Citizen citizen, Color color)
    {
        _citizen = citizen;
        _color = color;
        _background.color = color;

        _seriesField.text = citizen.Series.ToString();
        _numberField.text = citizen.Number.ToString();
        _fullNameField.text = citizen.FullName;
        _placeOfResidentField.text = citizen.PlaceOfResidence;
    }
    public void UpdateButton()
    {
        
        CancelButton();

        _curChange = this;

        _curUpdatedValues[0] = _seriesField.text;
        _curUpdatedValues[1] = _numberField.text;
        _curUpdatedValues[2] = _fullNameField.text;
        _curUpdatedValues[3] = _placeOfResidentField.text;

        _seriesField.enabled = true;
        _numberField.enabled = true;
        _fullNameField.enabled = true;
        _placeOfResidentField.enabled = true;

        _updateBtn.SetActive(false);
        _deleteBtn.SetActive(false);
        _saveBtn.SetActive(true);
        _cancelBtn.SetActive(true);
    }
    public void DeleteButton() 
    {
        try
        {
            if (CRUD.Delete(_citizen))
            {
                Database.citizenTable._citizens.Remove(_citizen.SeriesAndNumber);
                Destroy(this.gameObject);
            }
        }
        catch (Exception e)
        {
            Database.errorDisplayer.DisplayError(e);
        }
    }
    public void SaveButton()
    {
        try
        {
            Citizen newCitizen = new Citizen();
            int series;
            if (!int.TryParse(_seriesField.text, out series))
            {
                Debug.Log("SaveButton series dont parsed");
                return;
            }
            newCitizen.Series = series;
            int number;
            if (!int.TryParse(_numberField.text, out number))
            {
                Debug.Log("SaveButton number dont parsed");
                return;
            }
            newCitizen.Number = number;
            newCitizen.FullName = _fullNameField.text;
            newCitizen.PlaceOfResidence = _placeOfResidentField.text;
            if (CRUD.UpdateElement(_citizen, newCitizen))
            {
                Database.citizenTable._citizens.Remove(_citizen.SeriesAndNumber);
                Database.citizenTable._citizens.Add(newCitizen.SeriesAndNumber, newCitizen);
                _citizen = newCitizen;

                _curUpdatedValues[0] = _seriesField.text;
                _curUpdatedValues[1] = _numberField.text;
                _curUpdatedValues[2] = _fullNameField.text;
                _curUpdatedValues[3] = _placeOfResidentField.text;
            }            
        }
        catch (Exception e)
        {
            Database.errorDisplayer.DisplayError(e);
            return;
        }
        CancelButton();
    }
    public void CancelButton() 
    {
        if (_curChange == null)
            return;
        _curChange._seriesField.enabled = false;
        _curChange._numberField.enabled = false;
        _curChange._fullNameField.enabled = false;
        _curChange._placeOfResidentField.enabled=false;

        _curChange._saveBtn.SetActive(false);
        _curChange._cancelBtn.SetActive(false);
        _curChange._updateBtn.SetActive(true);
        _curChange._deleteBtn.SetActive(true);

        _curChange._seriesField.text = _curUpdatedValues[0];
        _curChange._numberField.text = _curUpdatedValues[1];
        _curChange._fullNameField.text = _curUpdatedValues[2];
        _curChange._placeOfResidentField.text = _curUpdatedValues[3];

        _curChange = null;
    }
}
