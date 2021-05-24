using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Utilities
{
    
    public static IEnumerable<double> GenerateX(double minX, double maxX, double step)
    {
        if (minX > maxX)
            return null;

        var result = new List<double>();
        
        for (var i = minX; i <= maxX; i+=step)
        {
            result.Add(i);
        }

        return result;
    }

    // ReSharper disable once InconsistentNaming
    public static List<KeyValuePair<double, double>> GetYPairs(IEnumerable<KeyValuePair<double, double>> pairsXY)
    {
        var yPairs = new List<KeyValuePair<double, double>>();
        var prev = 0.0;
        
        foreach (var now in pairsXY.Select(pair => pair.Value))
        {
            yPairs.Add(new KeyValuePair<double, double>(prev, now));
            prev = now;
        }

        return yPairs;
    }
    
    // ReSharper disable once InconsistentNaming
    public static List<KeyValuePair<double, double>> GetXPairs(IEnumerable<KeyValuePair<double, double>> pairsXY)
    {
        var xPairs = new List<KeyValuePair<double, double>>();
        var prev = 0.0;
        
        foreach (var now in pairsXY.Select(pair => pair.Key))
        {
            xPairs.Add(new KeyValuePair<double, double>(prev, now));
            prev = now;
        }

        return xPairs;
    }
    
    
    public static void DebugCollection<TEnumerable>(TEnumerable enumerable) where TEnumerable:IEnumerable
    {
        foreach (var en in enumerable)
        {
            Debug.Log(en.ToString());
        }
    }
}
