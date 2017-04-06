using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; // needed for reading and writing .csv
using System.Text; // for csv 
using System; // needed for Array.Sort

public class ChangeColorInterface : MonoBehaviour {

    public Transform character;
    private ChangeColor changecolor;
    float color;
    float[] ecg; // original data, which is pretended to be streamed live at 60Hz

    float ecg_current; // simply the voltage as it is streamed "live"
    float ecg_lastframe; // stored to compare difference
    float ecg_diff;
    float threshold; // cutoff to detect R curve
    float[] ecg_diff_firstseconds; // records difference of some seconds to find reasonable cutoff for R curve
    int counter = 0;

	// Use this for initialization
	void Start () {
        changecolor = character.GetComponent<ChangeColor>();
        LoadFile("C:/Users/mar39lw/Documents/Projects/2017_Genf/ECG data/ecg9.csv");

        ecg_diff_firstseconds = new float[300]; // 5 seconds
    }
	
	// Update is called once per frame
	void Update () {
        
        ecg_current = ecg[counter];
        if (counter == 0) { ecg_lastframe = ecg_current; } // only on start of streaming, set last to current
        ecg_diff = Mathf.Abs(ecg_current - ecg_lastframe);
        if (counter < 300) { ecg_diff_firstseconds[counter] = ecg_diff; } // store differences 

        if (counter == 300)
        {
            Array.Sort(ecg_diff_firstseconds);
            threshold = ecg_diff_firstseconds[Mathf.CeilToInt(ecg_diff_firstseconds.Length * 0.95f)]; // make 95% percentile threshold -> seems good compromise for z curve detection
        }

        if (counter > 300)
        {
            if (ecg_diff > threshold) { color = 5; } // 4 is highest. set to 5 as it will be faded in next step
        }

        if (color > 0) { color -= .1f; } // always fade

        changecolor.SetColor(Mathf.CeilToInt(color));

        ecg_lastframe = ecg_current;
        counter++;
    }

    // load csv file with only one line and put it into ecg as floats
    public void LoadFile(string path)
    {
        if (File.Exists(path))
        {
            StreamReader sr = new StreamReader(path);
            string[] Line = sr.ReadLine().Split(';'); // only one line, so no need for "while (!sr.EndOfStream)"

            ecg = new float[Line.Length];
            for (int i = 0; i < Line.Length; i++)
            {
                ecg[i] = float.Parse(Line[i]);
            }
        }
        else { Debug.Log("ecg file not found"); }
    }
}
