using Npgsql;
using System;
using UnityEngine;

public class Database : MonoBehaviour
{
    public static NpgsqlConnection dbConnection;
    public static NpgsqlCommand _dbcmd;
    public static NpgsqlDataReader _dbReader;

    public static CitizenTable citizenTable;// = new CitizenTable();
    public static StateInstitutionTable SITable;
    //public static UnityEvent<ITableElement> ItemChanged = new UnityEvent<ITableElement>();

    public static ErrorDisplayer errorDisplayer;
    private void Awake()
    {
        Init();
        try
        {
            ConnectDataBase();
            _dbcmd = dbConnection.CreateCommand();
        }
        catch (Exception e)
        {
            errorDisplayer.DisplayError(e);
        }
        
        
    }
    
    private void Init()
    {
        citizenTable = GetComponentInChildren<CitizenTable>();
        SITable = GetComponentInChildren<StateInstitutionTable>();
        errorDisplayer = GetComponent<ErrorDisplayer>();
    }


    private void ConnectDataBase()
    {
        string connectionString =
            "Port = 5432;" +
            "Server=localhost;" +
            "Database=postgres;" +
            "User ID=postgres;" +
            "Password=postgres;";
        dbConnection = new NpgsqlConnection(connectionString);
        dbConnection.Open();
    }
}
