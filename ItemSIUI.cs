using System;
using UnityEngine;
using UnityEngine.UI;

public class ItemSIUI : MonoBehaviour
{
    [SerializeField]
    public TMPro.TMP_InputField _numberField, _addressField;
    [SerializeField]
    public TMPro.TMP_Dropdown _budget;
    [SerializeField]
    public GameObject _updateBtn, _saveBtn, _cancelBtn, _deleteBtn;
    [SerializeField]
    private Image _background;

    private Color _color;
    public StateInstitution _SI;

    public static ItemSIUI _curChange = null;
    public static string[] _curUpdatedValues = new string[3];

    public void Init(StateInstitution SI, Color color)
    {
        _SI = SI;
        _color = color;
        _background.color = color;
        _budget.GetComponent<Image>().color = color;
        _budget.GetComponentInChildren<Image>().color = color;
        
        _numberField.text = SI.NumberSMI.ToString();
        _budget.value = (int)SI.BudgetFrom;
        _addressField.text = SI.Address;
    }
    public void UpdateButton()
    {
        
        CancelButton();

        _curChange = this;

        _curUpdatedValues[0] = _numberField.text;        
        _curUpdatedValues[1] = _addressField.text;
        _curUpdatedValues[2] = _budget.value.ToString();

        _numberField.enabled = true;
        _budget.enabled = true;
        _addressField.enabled = true;

        _updateBtn.SetActive(false);
        _deleteBtn.SetActive(false);
        _saveBtn.SetActive(true);
        _cancelBtn.SetActive(true);
    }
    public void DeleteButton() 
    {
        try
        {
            if (CRUD.Delete(_SI))
            {
                Database.SITable._stateInstitutions.Remove(_SI.NumberSMI);
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
            StateInstitution newSI = new StateInstitution();
            int number;
            if (!int.TryParse(_numberField.text, out number))
            {
                throw new Exception("Bad parsing");
            }
            newSI.NumberSMI = number;            
            newSI.Address = _addressField.text;
            newSI.BudgetFrom = (Budget)_budget.value;
            if (CRUD.UpdateElement(_SI, newSI))
            {
                Database.SITable._stateInstitutions.Remove(_SI.NumberSMI);
                Database.SITable._stateInstitutions.Add(newSI.NumberSMI, newSI);
                _SI = newSI;

                _curUpdatedValues[0] = _numberField.text;                
                _curUpdatedValues[1] = _addressField.text;
                _curUpdatedValues[2] = _budget.value.ToString();
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
        _curChange._numberField.enabled = false;
        _curChange._budget.enabled = false;
        _curChange._addressField.enabled=false;

        _curChange._saveBtn.SetActive(false);
        _curChange._cancelBtn.SetActive(false);
        _curChange._updateBtn.SetActive(true);
        _curChange._deleteBtn.SetActive(true);

        _curChange._numberField.text = _curUpdatedValues[0];        
        _curChange._addressField.text = _curUpdatedValues[1];
        if (!int.TryParse(_curUpdatedValues[2], out int value))
            throw new Exception("Bad parsing");
        _curChange._budget.value = value;

        _curChange = null;
    }
}
