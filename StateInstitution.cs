using System;
using System.Collections;

public enum Budget 
{
    Null = -1,
    Государственный = 0,
    Областной = 1,
    Частный = 2
}
public class StateInstitution : ITableElement
{
    private long _numberSMI;
    private string _address;
    private Budget _budgetFrom;

    private ArrayList data = new ArrayList(3);

    public StateInstitution()
    {        
        _numberSMI = -1;
        _address = null;
        _budgetFrom = Budget.Null;
    }
    public StateInstitution(long numberSMI, string address, Budget budgetFrom)
    {
        _numberSMI = numberSMI;
        _address = address;
        _budgetFrom = budgetFrom;

        data.Add(_numberSMI);
        data.Add(_address);
        data.Add(_budgetFrom.ToString());
    }

    public long NumberSMI
    {
        get { return _numberSMI; }
        set
        {
            if (value < 1000000 || value > 9999999)
                throw new Exception($"Неправильно введен номер ГМУ: {value}. Должно быть семь цифр");
            _numberSMI = value;
            data.Insert(0, value);
        }
    }
    public string Address
    {
        get { return _address; }
        set { _address = value; data.Insert(1, value); }
    }
    public Budget BudgetFrom
    {   get { return _budgetFrom; }
        set
        {   if ((int)value > 2 || (int)value < 0) 
                _budgetFrom = Budget.Null;
            _budgetFrom = value;
            data.Insert(2, value.ToString());
        }
    }

    public ArrayList Data { get => data; }
}
