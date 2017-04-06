using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor; // needed for TextureImporter
//using System.IO; // to automatically detect Directory

// this script simply changes color of entire mesh renderer. To limit this to certain areas (and not the clothes, for example), I see two solutions:
// 1. asset "Colorify" (25$)
// 2. asset "Texture Mixer" (free), manipulate character's mesh in paint and mix the two

public class ChangeColor : MonoBehaviour {

    [SerializeField]
    GameObject skin;
    // public int color; // 
    Texture2D skinNormal;
    
    Texture2D skinBlushed;
    
    Color[] skinNormalCol;
    Color[] resultingTextureCol;
    Color[] skinBlushedCol;
    Texture2D[] ShadesOfBlushing;
    Color[] ShadesOfBlushingCol;

    // Use this for initialization
    void Start () {
        skinNormal = (Texture2D)Resources.Load("Frau11_normal");
        skinBlushed = (Texture2D)Resources.Load("Frau11_blushed");

        ShadesOfBlushing = new Texture2D[10];

        // create 10 intensities
        for (int t = 0; t < 5; t++)
        {
                skinNormalCol = skinNormal.GetPixels(0); // only work at mip 0
                skinBlushedCol = skinBlushed.GetPixels(0); // only work at mip 0
                ShadesOfBlushingCol = skinNormal.GetPixels();

                ShadesOfBlushing[t] = new Texture2D(2048, 2048, TextureFormat.ARGB32, false);

                for (int i = 0; i < skinNormalCol.Length; i++) // 
                {
                    ShadesOfBlushingCol[i] = skinNormalCol[i] * (1 - t * 0.2f) + skinBlushedCol[i] * t * 0.2f; // adjust to 1/tmax
                }

                ShadesOfBlushing[t].SetPixels(ShadesOfBlushingCol, 0); // only work at mip 0

            ShadesOfBlushing[t].Apply(false); // apply, but don't recalculate mip
        }
        
    }

    public void SetColor(int color)
    {
        if (color >= 0 && color < 5)
        {
            skin.GetComponent<Renderer>().material.SetTexture("_MainTex", ShadesOfBlushing[color]);
        }
    }
	
	// Update is called once per frame
	void Update () {
        //character.GetComponent<Renderer>().material.color = Color.Lerp(Color.white, Color.red, color*0.1f); // color entire mesh
    }
}
