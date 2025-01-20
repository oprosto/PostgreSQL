using Npgsql;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CitizenTableUI))]
public class CitizenTable : MonoBehaviour, ITable
{
    private string _tableName = "���������";
    public Dictionary<string, Citizen> _citizens = new Dictionary<string, Citizen>();
    private string[] _columns = { "�����", "�����", "���", "�����������" };
    private string[] _pKey = { "�����", "�����" };
    private CitizenTableUI _tableUI;

    private void Awake()
    {
        _tableUI = GetComponent<CitizenTableUI>();
    }

    public string TABLE_NAME {  get { return _tableName; } }
    public string[] COLUMNS { get { return _columns; } }
    public string[] P_KEY { get { return _pKey; } }

    public void CreateFromRead(NpgsqlDataReader reader)
    {
        _tableUI.CreateFromRead(reader);
    }

}


