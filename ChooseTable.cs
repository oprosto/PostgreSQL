using UnityEngine;
using UnityEngine.Events;

public class ChooseTable : MonoBehaviour
{
    [SerializeField]
    private GameObject _citizenTable, _SITable, _viewPortContent;

    private GameObject _curTable;

    public static UnityEvent<Tables> ChangeTable = new UnityEvent<Tables>();

    private void Start()
    {
        onCitizenChoosed();
    }
    /*private void Clear() 
    {
        while (_viewPortContent.transform.childCount > 0)
            Destroy(_viewPortContent.transform.GetChild(0).gameObject);
    }*/
    public void Clear()
    {
        Debug.Log(_viewPortContent.transform.childCount);
        int i = 0;

        //Array to hold all child obj
        GameObject[] allChildren = new GameObject[_viewPortContent.transform.childCount];

        //Find all child obj and store to that array
        foreach (Transform child in _viewPortContent.transform)
        {
            allChildren[i] = child.gameObject;
            i += 1;
        }

        //Now destroy them
        foreach (GameObject child in allChildren)
        {
            DestroyImmediate(child.gameObject);
        }

        Debug.Log(_viewPortContent.transform.childCount);
    }
    public void onCitizenChoosed() 
    {
        if (_curTable == _citizenTable)
            return;
        if (_viewPortContent.transform.childCount > 0)
        {
            Database.SITable._stateInstitutions.Clear();
            Clear();
        }
        if (_curTable != null)
            _curTable.SetActive(false);
        _citizenTable.SetActive(true);
        _curTable = _citizenTable;
        ChangeTable.Invoke(Tables.Гражданин);
    }
    public void onStateInstitutionChoosed() 
    {        
        if (_curTable == _SITable)
            return;
        if (_viewPortContent.transform.childCount > 0)
        {
            Database.citizenTable._citizens.Clear();
            Clear();
        }
        if (_curTable != null)
            _curTable.SetActive(false);
        _SITable.SetActive(true);
        _curTable = _SITable;
        ChangeTable.Invoke(Tables.ГМУ);
    }
}
