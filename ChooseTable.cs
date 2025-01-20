using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class ChooseTable : MonoBehaviour
{
    [SerializeField]
    private GameObject _citizenTable;
    [SerializeField]
    private ScrollView _scrollView;

    private GameObject _curTable;

    public static UnityEvent<Tables> ChangeTable = new UnityEvent<Tables>();
    private void Start()
    {
        onCitizenChoosed();
    }
    public void onCitizenChoosed() 
    {

        //_scrollView.contentContainer.Clear();
        if (_curTable != null)
            _curTable.SetActive(false);
        _citizenTable.SetActive(true);
        _curTable = _citizenTable;
        ChangeTable.Invoke(Tables.Гражданин);
    }
}
