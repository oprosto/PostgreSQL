using System;
using System.Collections;
public class Citizen : ITableElement
{
    private int _series;
    private int _number;
    private string _fullName;
    private string _placeOfResidence;

    private string _seriesAndNumber;
    private ArrayList data = new ArrayList(4);
    public Citizen()
    {
        _series = -1;
        _number = -1;
        _fullName = null;
        _placeOfResidence = null;
    }
    public Citizen(int series, int number, string fullName, string placeOfResidence)
    {
        _series = series;
        _number = number;
        _fullName = fullName;
        _placeOfResidence = placeOfResidence;

        _seriesAndNumber = series.ToString() + " " + _number.ToString();
        data.Add(_series);
        data.Add(_number);
        data.Add(_fullName);
        data.Add(_placeOfResidence);
    }

    public int Series
    {
        get { return _series; }
        set
        {
            if (value < 1000 || value > 9999)
                throw new Exception($"Неправильно введена серия паспорта: {value}. Она должна состоять из четырех цифр");
            _series = value;
            _seriesAndNumber = _series.ToString() + " " + _number.ToString();
            data.Insert(0, value);
        }
    }
    public int Number
    {
        get { return _number; }
        set
        {
            if (value < 100000 || value > 999999)
                throw new Exception($"Неправильно введен номер паспорта: {value}. Он должен состоять из шести цифр");
            _number = value;
            _seriesAndNumber = _series.ToString() + " " + _number.ToString();
            data.Insert(1, value);
        }
    }
    public string FullName
    {
        get { return _fullName; }
        set
        {
            if (value.Split().Length < 3)
                throw new Exception($"Неправильно введено ФИО: {value}. Оно должно состоять из трех слов");
            _fullName = value;
            data.Insert(2, value);
        }
    }
    public string PlaceOfResidence { get { return _placeOfResidence; } set { _placeOfResidence = value; data.Insert(3, value); } }
    public string SeriesAndNumber { get { return _seriesAndNumber; } }

    public ArrayList Data { get => data; }
}
