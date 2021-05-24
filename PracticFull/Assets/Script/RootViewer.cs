using System.Globalization;
using TMPro;
using UnityEngine;

public class RootViewer : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI _textHolder;

    public void ViewRoot(double root)
    {
        _textHolder.text = root.ToString("F5", CultureInfo.InvariantCulture);
    }
}