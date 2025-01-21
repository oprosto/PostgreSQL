using Npgsql;
using System.Collections.Generic;
using UnityEngine;

public  class StateInstitutionTable : MonoBehaviour, ITable
{
    private string _tableName = "ֳּ׃";
    public Dictionary<long, StateInstitution> _stateInstitutions = new Dictionary<long, StateInstitution>();
    private string[] _columns = { "ֽמלונ_ֳּ׃", "ְהנוס", "ֱ‏הזוע" };
    private string[] _pKey = { "Kֽמלונ_ֳּ׃" };
    private SITableUI _tableUI;

    private void Awake()
    {
        _tableUI = GetComponent<SITableUI>();
    }
    public string TABLE_NAME { get { return _tableName; } }
    public string[] COLUMNS { get { return _columns; } }
    public string[] P_KEY { get { return _pKey; } }

    public void CreateFromRead(NpgsqlDataReader reader)
    {
        _tableUI.CreateFromRead(reader);
    }
}