using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ApproximateRootViewer : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI _textHolder;

    public void ViewRoot(KeyValuePair<double, double> root)
    {
        _textHolder.text = $"[ {root.Key:F} ; {root.Value:F} ]";
    }
}