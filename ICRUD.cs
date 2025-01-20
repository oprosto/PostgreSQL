using UnityEngine;

public interface ICRUD
{
    void Add(object element);
    T Read<T>(int id);
    void Updater<T>(int id, T element);
    void Delete(int id);
    protected void CreateTableIfNotExist();

}
