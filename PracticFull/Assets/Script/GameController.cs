using System.Collections.Generic;
using System.Linq;
using AwesomeCharts;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private ApproximateRootViewer _approximateRootViewerObject;
    [SerializeField] private Transform _approximateRootViewerParentTransform;
    
    [SerializeField] private RootViewer _rootViewerObject;
    [SerializeField] private Transform _rootViewerParentTransform;

    [SerializeField] private TextMeshProUGUI textPolynomial;
    [SerializeField] private LineChart chart;

    [SerializeField] private TextMeshProUGUI textIntegral;

    
    private readonly List<double> _xList = new List<double>
    {
        -5, -2, 1, 3, 6, 8
    };
    private readonly List<double> _yList = new List<double>
    {
        2.3, -3, -2.32, -0.34, 1.5, 2.1
    };
    
    private List<KeyValuePair<double, double>> _selectedRoots;
    private List<double> _qualifiedRoots;

    private RootSelector _rootSelector;
    private IFunction _taskOneFunction;
    private IQualifierMethod _qualifierMethod;
    private IPolynomialMethod _polynomialMethod;
    private IFunction _taskFourFunction;

    public void FindApproximateRoots()
    {
        var xCollection = Utilities.GenerateX(-5, 8, 0.01);
        _taskOneFunction = new TaskOneFunction();
        _rootSelector = new RootSelector(_taskOneFunction);
        _selectedRoots = _rootSelector.GetApproximateRoots(xCollection).ToList();
        ViewApproximateRoots();
    }

    private void ViewApproximateRoots()
    {
        for (var i = 0; i < _approximateRootViewerParentTransform.childCount; i++)
        {
            Destroy(_approximateRootViewerParentTransform.GetChild(i).gameObject);
        }
        foreach (var root in _selectedRoots)
        {
            var rv = Instantiate(_approximateRootViewerObject, _approximateRootViewerParentTransform);
            rv.ViewRoot(root);
        }
    }

    public void QualifyRoots()
    {
        _qualifierMethod = new ChordMethod(_taskOneFunction);
        _qualifiedRoots = _selectedRoots.Select(xPair => _qualifierMethod.ClarifyApproximatedRoot(xPair.Key, xPair.Value, 0.001)).ToList();
        ViewQualifiedRoots();
    }

    private void ViewQualifiedRoots()
    {
        for (var i = 0; i < _rootViewerParentTransform.childCount; i++)
        {
            Destroy(_rootViewerParentTransform.GetChild(i).gameObject);
        }

        foreach (var root in _qualifiedRoots)
        {
            var rv = Instantiate(_rootViewerObject, _rootViewerParentTransform);
            rv.ViewRoot(root);
        }
    }

    public void ConstructPolynomial()
    {
        _polynomialMethod = new NewtonPolynomialMethod(_xList, _yList);
        
        ViewPolynomial();
        ViewGraph();
    }

    private void ViewGraph()
    {
        var xCollection = Utilities.GenerateX(-5, 8, 0.1);
        var set1 = new LineDataSet
        {
            LineColor = new Color32(54, 105, 126, 255), 
            FillColor = new Color32(54, 105, 126, 110)
        };

        foreach (var x in xCollection)
        {
            set1.AddEntry(new LineEntry((float)x,(float)_polynomialMethod.GetPolynomialValue(x)));
        }

        chart.GetChartData().DataSets.Clear();
        chart.GetChartData().DataSets.Add(set1);
        chart.SetDirty();
    }

    private void ViewPolynomial()
    {
        textPolynomial.text = _polynomialMethod.GetPolynomialString();
    }

    public void CalculateIntegral()
    {
        _taskFourFunction = new TaskFourFunction(new BaseFunction(), _polynomialMethod);
        
        var a = _qualifiedRoots[1];
        var b = _qualifiedRoots[2];
        var n = 100;
        var h = (b - a) / n;

        var integral = 0.0;
        for (var i = 1; i < n; i++)
            integral += _taskFourFunction.ProceedFunction(a + i * h,Derivative.None);
        integral += (_taskFourFunction.ProceedFunction(a, Derivative.None) +
                     _taskFourFunction.ProceedFunction(b, Derivative.None)) / 2;
        integral *= h;
        
        textIntegral.text = integral.ToString("F5");

    }
    
}