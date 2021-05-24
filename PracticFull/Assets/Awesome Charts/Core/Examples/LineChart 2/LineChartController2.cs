using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace AwesomeCharts {
    public class LineChartController2 : MonoBehaviour
    {

        public TMP_InputField xInput;
        public TMP_InputField yInput;
        public TextMeshProUGUI text;
        public LineChart chart;


        [ShowInInspector] private List<double> xList;
        [ShowInInspector] private List<List<double>> rankLists;
        
        public void Interpolate()
        {
            xList = new List<double>();
            rankLists = new List<List<double>>();
            
            var xString = xInput.text.Split(new char[] { ';' });
            var yString = yInput.text.Split(new char[] { ';' });

            foreach (var x in xString)
            {
                double.TryParse(x, out var dX);
                xList.Add(dX);
            }
            rankLists.Add(new List<double>());
            foreach (var y in yString)
            {
                double.TryParse(y, out var dY);
                rankLists[0].Add(dY);
            }

            for (var rank = 1; rank < xList.Count; rank++)
            {
                rankLists.Add(new List<double>());
                for (var i = 0; i < xList.Count - rank; i++)
                {
                    rankLists[rank].Add((rankLists[rank - 1][i + 1] - rankLists[rank - 1][i]) /
                                        (xList[i + rank] - xList[i]));
                }
            }

            
            var polynomial = rankLists[0][0].ToString("F");
            for (var rank = 1; rank < xList.Count; rank++)
            {
                polynomial += rankLists[rank][0] >= 0 ? " + " : " - ";
                polynomial += Math.Abs(rankLists[rank][0]).ToString("F5");
                for (var i = 0; i < rank; i++)
                {
                    polynomial += " * ";
                    polynomial += "(x " + (xList[i] >= 0 ? "- " : "+ ") + Math.Abs(xList[i]).ToString("F") + ")";
                }
            }
            text.text = polynomial;

            var set1 = new LineDataSet
            {
                LineColor = new Color32(54, 105, 126, 255), 
                FillColor = new Color32(54, 105, 126, 110)
            };
            for (var x = xList[0]; x < xList[xList.Count - 1]; x += 0.1f)
            {
                var p = rankLists[0][0];
                for (var rank = 1; rank < xList.Count; rank++)
                {
                    var rankValue = rankLists[rank][0];
                    for (var i = 0; i < rank; i++)
                    {
                        rankValue *= x - xList[i];
                    }

                    p += rankValue;
                }
                
                set1.AddEntry(new LineEntry((float) x, (float) p));
            }

            chart.GetChartData().DataSets.Clear();
            chart.GetChartData().DataSets.Add(set1);
            chart.SetDirty();


        }
        
        

        private void AddChartData() {
            LineDataSet set1 = new LineDataSet();
            set1.AddEntry(new LineEntry(0, 110));
            set1.AddEntry(new LineEntry(20, 50));
            set1.AddEntry(new LineEntry(40, 70));
            set1.AddEntry(new LineEntry(60, 130));
            set1.AddEntry(new LineEntry(80, 150));

            LineDataSet set2 = new LineDataSet();
            set2.AddEntry(new LineEntry(0, 80));
            set2.AddEntry(new LineEntry(20, 110));
            set2.AddEntry(new LineEntry(40, 95));
            set2.AddEntry(new LineEntry(60, 90));
            set2.AddEntry(new LineEntry(80, 120));

            set2.LineColor = new Color32(9, 107, 67, 255);
            set2.FillColor = new Color32(9, 107, 67, 110);

            chart.GetChartData().DataSets.Add(set1);
            chart.GetChartData().DataSets.Add(set2);

            chart.SetDirty();
        }
    }
}
