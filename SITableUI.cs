using Npgsql;
using System;
using TMPro;
using UnityEngine;

public class SITableUI : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField _number;
    [SerializeField]
    private TMP_Dropdown _budget;
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
            StateInstitution SI = CreateSI();
            if (CRUD.Add(SI))
            {
                Database.SITable._stateInstitutions.Add(SI.NumberSMI, SI);
                GameObject line = Instantiate(prefab, content);
                if (counter % 2 == 0)
                    line.GetComponent<ItemSIUI>().Init(SI, bright);
                else
                    line.GetComponent<ItemSIUI>().Init(SI, dim);
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
            StateInstitution SI = CreateSI(reader);
            Database.SITable._stateInstitutions.Add(SI.NumberSMI, SI);
            GameObject line = Instantiate(prefab, content);
            if (counter % 2 == 0)
                line.GetComponent<ItemSIUI>().Init(SI, bright);
            else
                line.GetComponent<ItemSIUI>().Init(SI, dim);
            counter++;
        }
        catch (Exception e)
        {
            Database.errorDisplayer.DisplayError(e);
        }
    }
    private StateInstitution CreateSI(NpgsqlDataReader reader)
    {
        StateInstitution newSI = new StateInstitution();
        
        long? number = reader["Номер_ГМУ"] as long?;
        if (number == null)
            throw new ArgumentNullException();   
        string address = reader["Адрес"] as string;
        string budget = reader["Бюджет"] as string;
        if (!Enum.TryParse(budget, out Budget b))
            throw new Exception("Такого получения бюджета не бывает");
        newSI.NumberSMI = (long)number;
        newSI.Address = address;
        newSI.BudgetFrom = b;
        
        return newSI;
    }
    private StateInstitution CreateSI()
    {
        StateInstitution newSi = new StateInstitution();
        int number;
        if (!int.TryParse(_number.text, out number))
        {
            throw new Exception($"Неправильно введен номер паспорта: {number}");
        }
        newSi.NumberSMI = number;
        newSi.Address = _placeOfResident.text;
        newSi.BudgetFrom = (Budget)_budget.value;

        return newSi;
    }
    public void OpenCloseCreateWindow() 
    {
        _createWindow.SetActive(!_createWindow.activeSelf);
    }
    
}
