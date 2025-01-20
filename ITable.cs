using Npgsql;

public interface ITable
{
    public string TABLE_NAME { get; }
    public string[] COLUMNS { get; }
    public string[] P_KEY { get; }

    public void CreateFromRead(NpgsqlDataReader reader);
}
