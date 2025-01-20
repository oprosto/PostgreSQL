using System;

public enum Budget 
{
    Null = -1,
    Governament = 0,
    Region = 1,
    Private = 2
}
public class StateInstitution
{
    private long _numberSMI;
    private string _address;
    private Budget _budgetFrom;

    private object[] data = new object[3];

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

        data[0] = _numberSMI.ToString();
        data[1] = _address;
        data[2] = _budgetFrom.ToString();
    }

    public long NumberSMI
    {
        get { return NumberSMI; }
        set
        {
            if (value < 1000000 || value > 9999999)
                throw new Exception($"Неправильно введен номер ГМУ: {value}");
            NumberSMI = value;
        }
    }
    public string Address
    {
        get { return Address; }
        set { Address = value; }
    }
    public Budget BudgetFrom
    {   get { return _budgetFrom; }
        set
        {   if ((int)value > 2 || (int)value < 0) 
                _budgetFrom = Budget.Null;
            _budgetFrom = value;
        }
    }

    public object[] Data => throw new NotImplementedException();
}
