using System;
using System.Collections.Generic;

public interface IPolynomialMethod
{
    double GetPolynomialValue(double x);
    string GetPolynomialString();
}

public class NewtonPolynomialMethod: IPolynomialMethod
{

    private List<List<double>> _rankLists;
    private List<double> _xList;

    public NewtonPolynomialMethod(IReadOnlyList<double> xList, IEnumerable<double> yList)
    {
        _xList = new List<double>();
        _xList.AddRange(xList);

        _rankLists = new List<List<double>> {new List<double>()};
        _rankLists[0].AddRange(yList);
        
        for (var rank = 1; rank < xList.Count; rank++)
        {
            _rankLists.Add(new List<double>());
            for (var i = 0; i < xList.Count - rank; i++)
            {
                _rankLists[rank].Add((_rankLists[rank - 1][i + 1] - _rankLists[rank - 1][i]) /
                                    (xList[i + rank] - xList[i]));
            }
        }
    }
    
    public double GetPolynomialValue(double x)
    {
        var p = _rankLists[0][0];
        for (var rank = 1; rank < _xList.Count; rank++)
        {
            var rankValue = _rankLists[rank][0];
            for (var i = 0; i < rank; i++)
            {
                rankValue *= x - _xList[i];
            }

            p += rankValue;
        }

        return p;
    }

    public string GetPolynomialString()
    {
        var polynomial = _rankLists[0][0].ToString("F");
        for (var rank = 1; rank < _xList.Count; rank++)
        {
            polynomial += _rankLists[rank][0] >= 0 ? " + " : " - ";
            polynomial += Math.Abs(_rankLists[rank][0]).ToString("F5");
            for (var i = 0; i < rank; i++)
            {
                polynomial += " * ";
                polynomial += "(x " + (_xList[i] >= 0 ? "- " : "+ ") + Math.Abs(_xList[i]).ToString("F") + ")";
            }
        }
        return polynomial;
    }
}