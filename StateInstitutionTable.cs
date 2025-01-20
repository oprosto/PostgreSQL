using Npgsql;
using System.Collections.Generic;

public  class StateInstitutionTable : ITable
{
    private string _tableName = "���. ����������";
    public Dictionary<string, StateInstitution> _citizens = new Dictionary<string, StateInstitution>();
    private string[] _columns = { "����� ���", "�����", "������ ������� ������" };
    private string[] _pKey = { "����� ���" };
    
    public string TABLE_NAME { get { return _tableName; } }
    public string[] COLUMNS { get { return _columns; } }
    public string[] P_KEY { get { return _pKey; } }

    public void CreateFromRead(NpgsqlDataReader reader)
    {
        throw new System.NotImplementedException();
    }
}
