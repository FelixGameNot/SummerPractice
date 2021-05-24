using System;

public interface IFunction
{
    double ProceedFunction(double x, Derivative derivative);
}

public class BaseFunction : IFunction
{
    
    //здесь f(x)=0
    public double ProceedFunction(double x, Derivative derivative)
    {
        return 4 * Math.Sin(3 * x) + 0.05 * Math.Pow(x, 2) + 1;
    }

}

public class TaskOneFunction : IFunction
{
    
    //здесь обрабатывается не f(x)=0 а f"(x)+f(x)=0
    public double ProceedFunction(double x, Derivative derivative)
    {
        switch (derivative)
        {
            case Derivative.None:
                return -32f * Math.Sin(3 * x) + 1.1 + 0.05f * Math.Pow(x,2);
            case Derivative.First:
                return -96 * Math.Cos(3 * x) + 0.1 * x;
            case Derivative.Second:
                return 288 * Math.Sin(3 * x) + .10;
            default:
                throw new ArgumentOutOfRangeException(nameof(derivative), derivative, null);
        }
    }

}

public class TaskFourFunction : IFunction
{
    private IFunction _baseFunction;
    private IPolynomialMethod _polynomialMethod;
    
    public TaskFourFunction(IFunction baseFunction, IPolynomialMethod polynomialMethod)
    {
        _baseFunction = baseFunction;
        _polynomialMethod = polynomialMethod;
    }
    
    //здесь f(x)=0
    public double ProceedFunction(double x, Derivative derivative)
    {
        //return Math.Pow(2.30 - 1.76667 * (x + 5) + (.332222 * (x + 5)) * (x + 2) - (0.2244e-1 * (x + 5)) * (x + 2) * (x - 1) -(0.55e-3 * (x + 5)) * (x + 2) * (x - 1) * (x - 3) +(0.275e-3 * (x + 5)) * (x + 2) * (x - 1) * (x - 3) * (x - 6) - _baseFunction.ProceedFunction(x,Derivative.None), 2) ;
        return Math.Pow(_polynomialMethod.GetPolynomialValue(x) - _baseFunction.ProceedFunction(x, Derivative.None), 2);
    }

}

public enum Derivative
{
    None,
    First,
    Second
}