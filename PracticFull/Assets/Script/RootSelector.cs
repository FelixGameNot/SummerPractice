using System.Collections.Generic;
using System.Linq;

public class RootSelector
{
    
    private readonly IFunction _functionHandler;
    
    public RootSelector(IFunction functionHandler)
    {
        _functionHandler = functionHandler;
    }
    
    public IEnumerable<KeyValuePair<double, double>> GetApproximateRoots(IEnumerable<double> xList)
    {
        var pairs = GetAllPairs(xList);
        var yPairs = Utilities.GetYPairs(pairs);
        var xPairs = Utilities.GetXPairs(pairs);

        var approximateRoots = new List<KeyValuePair<double, double>>();
        for (var i = 1; i < pairs.Count; i++)
        {
            if (yPairs[i].Key * yPairs[i].Value > 0) continue;

            if (_functionHandler.ProceedFunction(xPairs[i].Key, Derivative.First) *
                _functionHandler.ProceedFunction(xPairs[i].Value, Derivative.First) < 0) continue;

            if (_functionHandler.ProceedFunction(xPairs[i].Key, Derivative.Second) *
                _functionHandler.ProceedFunction(xPairs[i].Value, Derivative.Second) < 0) continue;
            
            approximateRoots.Add(xPairs[i]);
        }

        return approximateRoots;
    }
    
    private List<KeyValuePair<double,double>> GetAllPairs(IEnumerable<double> xList)
    {
        return (from t in xList 
            let y = _functionHandler.ProceedFunction(t, Derivative.None) 
            select new KeyValuePair<double, double>(t, y)).ToList();
    }

}
