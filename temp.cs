using TMPro;
using UnityEngine;

public class temp : MonoBehaviour
{
    [SerializeField] TMP_InputField _ser;
    [SerializeField] TMP_InputField _num;
    [SerializeField] TMP_InputField _fulNa;
    [SerializeField] TMP_InputField _Place;
    
    [SerializeField]CRUD crud;

    [SerializeField]
    private GameObject prefab;
    [SerializeField]
    private Transform content;

    private Color bright = new Color(0f, 0f, 0f, 0.6f);
    private Color dim = new Color(0f, 0f, 0f, 0.25f);

    public void getInfo() 
    {
        Debug.Log(_Place.text);
        //crud.Add(new Citizen());
        Citizen cit = new Citizen(int.Parse(_ser.text), int.Parse(_num.text), _fulNa.text, _Place.text);

        //Database.citizenTable._citizens.Add(cit.SeriesAndNumber, cit);
        GameObject line = Instantiate(prefab, content);
        line.GetComponent<ItemCivilUI>().Init(cit, bright);
        CRUD.Add(cit);
    }
}
