using Npgsql;
using System;
using TMPro;
using UnityEngine;

public class CitizenTableUI : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField _series;
    [SerializeField]
    private TMP_InputField _number;
    [SerializeField]
    private TMP_InputField _fullName;
    [SerializeField]
    private TMP_InputField _placeOfResident;
    [SerializeField]
    private CRUD crud;

    [SerializeField]
    private GameObject prefab;
    [SerializeField]
    private Transform content;

    private static Color bright = new Color(0f, 0f, 0f, 0.4f);
    private static Color dim = new Color(0f, 0f, 0f, 0.1f);
    private static int counter = 0;

    [SerializeField]
    private GameObject _createWindow;

    public void Create()
    {
        try 
        {
            Citizen citizen = CreateCitizen();
            if (CRUD.Add(citizen))
            {
                Database.citizenTable._citizens.Add(citizen.SeriesAndNumber, citizen);
                GameObject line = Instantiate(prefab, content);
                if (counter % 2 == 0)
                    line.GetComponent<ItemCivilUI>().Init(citizen, bright);
                else
                    line.GetComponent<ItemCivilUI>().Init(citizen, dim);
                counter++;
            }
        }
        catch (Exception e) 
        {
            Database.errorDisplayer.DisplayError(e);
        }
    }
    public void CreateFromRead(NpgsqlDataReader reader) 
    {
        try
        {
            Citizen citizen = CreateCitizen(reader);
            Database.citizenTable._citizens.Add(citizen.SeriesAndNumber, citizen);
            GameObject line = Instantiate(prefab, content);
            if (counter % 2 == 0)
                line.GetComponent<ItemCivilUI>().Init(citizen, bright);
            else
                line.GetComponent<ItemCivilUI>().Init(citizen, dim);
            counter++;
        }
        catch (Exception e)
        {
            Database.errorDisplayer.DisplayError(e);
        }
    }
    private Citizen CreateCitizen(NpgsqlDataReader reader)
    {
        Citizen newCitizen = new Citizen();
        
        int? series = reader["Серия"] as int?;
        if (series == null)
            throw new ArgumentNullException();
        int? number = reader["Номер"] as int?;
        if (number == null)
            throw new ArgumentNullException();
        string fullName = reader["ФИО"] as string;
        string placeOfResidence = reader["Регистрация"] as string;
        newCitizen.Series = (int)series;        
        newCitizen.Number = (int)number;
        newCitizen.FullName = fullName;
        newCitizen.PlaceOfResidence = placeOfResidence;
        
        return newCitizen;
    }
    private Citizen CreateCitizen()
    {
        Citizen newCitizen = new Citizen();
        int series;
        if (!int.TryParse(_series.text, out series))
        {
            throw new Exception($"Неправильно введена серия паспорта: {series}");
        }
        newCitizen.Series = series;
        int number;
        if (!int.TryParse(_number.text, out number))
        {
            throw new Exception($"Неправильно введен номер паспорта: {number}");
        }
        newCitizen.Number = number;
        newCitizen.FullName = _fullName.text;
        newCitizen.PlaceOfResidence = _placeOfResident.text;

        return newCitizen;
    }
    public void OpenCloseCreateWindow() 
    {
        _createWindow.SetActive(!_createWindow.activeSelf);
    }
    
}
