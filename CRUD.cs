using Npgsql;
using System;
using Unity.VisualScripting;
using UnityEngine;

public enum Tables
{
    None = -1,
    ��������� = 0,
    ���� = 1,
    ��� = 2,
    ����������� = 3,
    ��������� = 4
}
public class CRUD : MonoBehaviour
{
    //[SerializeField]private TMPro.TMP_Dropdown _optionChanger, _tableChanger;
    //[SerializeField]private TMPro.TMP_InputField _commandBlock;
    private static ITable _curTable = null;
    private static string command = null;

    private void Awake()
    {
        ChooseTable.ChangeTable.AddListener(ChangeTable);
    }
    public static void ChangeTable(Tables table) 
    {
        switch (table) 
        {
            case Tables.���������:
                _curTable = Database.citizenTable;                
                break;
            case Tables.���:
                _curTable = Database.SITable;
                break;
        }
        ReadFromTable();
    }
    
    public static bool Add(ITableElement element)
    {
        try 
        {
            GenerateCommandAdd();
            NpgsqlCommand cmd = new NpgsqlCommand(command, Database.dbConnection);
            for (int i = 0; i < _curTable.COLUMNS.Length; i++)
            {
                cmd.Parameters.AddWithValue(_curTable.COLUMNS[i], element.Data[i]);
            }
            cmd.ExecuteNonQuery();
        }
        catch (Exception e) 
        {
            Database.errorDisplayer.DisplayError(e);
            return false;
        }
        Debug.Log("�� ������");
        return true;
    }
    private static void GenerateCommandAdd() 
    {
        command = "INSERT INTO ";
        command += _curTable.TABLE_NAME + " (";
        Debug.Log(_curTable.TABLE_NAME);
        foreach (string str in _curTable.COLUMNS)
        {
            command += str + ", ";
        }
        command = command.Substring(0, command.Length - 2);
        command += ") VALUES (";
        foreach (string str in _curTable.COLUMNS)
        {
            command += "@" + str + ", ";
        }
        command = command.Substring(0, command.Length - 2);
        command += ")";
    }
    public void Read()
    {
        //command = _commandBlock.text;
        NpgsqlCommand cmd = new NpgsqlCommand(command, Database.dbConnection);
        cmd.ExecuteNonQuery();
    }
    public static bool ReadFromTable()
    {
        try
        {
            command = "SELECT * FROM " + _curTable.TABLE_NAME;
            using (NpgsqlCommand cmd = new NpgsqlCommand(command, Database.dbConnection)) 
            {
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        _curTable.CreateFromRead(reader);
                    }
                }
            }                
            
            //Debug.Log("� �����");
        }
        catch (Exception e)
        {
            Database.errorDisplayer.DisplayError(e);
            return false;
        }
        return true;
    }

    public static bool UpdateElement(ITableElement oldElement, ITableElement newElement)
    {
        try
        {
            GenerateCommandUpdate();
            NpgsqlCommand cmd = new NpgsqlCommand(command, Database.dbConnection);
            for (int i = 0; i < _curTable.COLUMNS.Length; i++)
            {
                cmd.Parameters.AddWithValue(_curTable.COLUMNS[i], newElement.Data[i]);
            }
            for (int i = 0; i < _curTable.P_KEY.Length; i++)
            {
                cmd.Parameters.AddWithValue(_curTable.P_KEY[i], oldElement.Data[i]);
            }
            cmd.ExecuteNonQuery();
        }
        catch (Exception e) 
        {
            Database.errorDisplayer.DisplayError(e);
            return false;
        }
        return true;
    }
    private static void GenerateCommandUpdate()               
    {
        
        command = $"UPDATE {_curTable.TABLE_NAME}";
        command += " SET ";
        foreach (string str in _curTable.COLUMNS)
            command += str + " = @" + str + ", ";
        command = command.Substring(0, command.Length - 2);
        command += " WHERE ";
        for (int i = 0; i < _curTable.P_KEY.Length; i++)        
            command += _curTable.COLUMNS[i] + " = @K" + _curTable.COLUMNS[i] + " AND ";   
        command = command.Substring(0, command.Length - 5);
    }
    public static bool Delete(ITableElement element)
    {
        try
        {
            GenerateCommandDelete();
            NpgsqlCommand cmd = new NpgsqlCommand(command, Database.dbConnection);
            for (int i = 0; i < _curTable.P_KEY.Length; i++)
            {
                cmd.Parameters.AddWithValue(_curTable.P_KEY[i], element.Data[i]);
            }
            cmd.ExecuteNonQuery();
        }
        catch (Exception e) 
        {
            Database.errorDisplayer.DisplayError(e);
            return false;
        }
        return true;
    }
    private static void GenerateCommandDelete() 
    {
        command = $"DELETE FROM {_curTable.TABLE_NAME} WHERE ";
        for (int i = 0; i < _curTable.P_KEY.Length; i++)
            command += _curTable.COLUMNS[i] + " = @K" + _curTable.COLUMNS[i] + " AND ";
        command = command.Substring(0, command.Length - 5);
    }
}
