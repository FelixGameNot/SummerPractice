using System;
using UnityEngine;

public interface IQualifierMethod
{
    double ClarifyApproximatedRoot(double minX, double maxX, double accuracy);
}

public class ChordMethod : IQualifierMethod
{
    private readonly IFunction _function;
    
    public ChordMethod(IFunction function)
    {
        _function = function;
    }

    public double ClarifyApproximatedRoot(double minX, double maxX, double accuracy)
    {
        double prevC;
        double a;
        if (_function.ProceedFunction(minX, Derivative.First) * _function.ProceedFunction(minX, Derivative.Second) > 0)
        {
            prevC = minX;
            a = maxX;
        }
        else
        {
            prevC = maxX;
            a = minX;
        }

        double delta;
        do
        {
            var c = prevC - _function.ProceedFunction(prevC, Derivative.None) * (a - prevC) /
                (_function.ProceedFunction(a, Derivative.None) - _function.ProceedFunction(prevC, Derivative.None));
            delta = Math.Abs(prevC - c);
            Debug.Log($"{prevC} - {c} = {delta}");
            prevC = c;
        } while (delta > accuracy);

        return prevC;
    }
    
}