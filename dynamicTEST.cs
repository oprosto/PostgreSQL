using Npgsql;
using UnityEngine;

public class dynamicTEST : MonoBehaviour
{
    const string TABLE_NAME = "Гражданин";
    public void StartButton() 
    {
        ADDMode();
    }
    string cmdText = "";
    private void ADDMode() 
    {
        cmdText = $"INSERT INTO {TABLE_NAME} ";

    }

    private void DeleteMode() 
    {
        cmdText = $"DELETE FROM {TABLE_NAME} WHERE ID=(@p)";
    }
    private void UpdateMode()
    {
        
    }
    public void Add(Citizen citizen)
    {
        string cmdText = $"INSERT INTO {TABLE_NAME} (Серия, Номер, ФИО, Регистрация) VALUES (@series, @number, @fullName, @placeOfResidence)";
        NpgsqlCommand cmd = new NpgsqlCommand(cmdText, Database.dbConnection);
        cmd.Parameters.AddWithValue("series", citizen.Series);
        cmd.Parameters.AddWithValue("number", citizen.Number);
        cmd.Parameters.AddWithValue("fullName", citizen.FullName);
        cmd.Parameters.AddWithValue("placeOfResidence", citizen.PlaceOfResidence);
        cmd.ExecuteNonQuery();        
    }
}
