using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apperance : MonoBehaviour {

    Renderer[] rens = new Renderer[] { };
    int count = 0;
    int lastIndex = 0;
    float deltaTime = 0;
    public float intervalTime = 0.5f;
    public Color color=new Color(1,0,0);
	// Use this for initialization
	void Start () {
        rens=this.GetComponentsInChildren<Renderer>();
        count = rens.Length;
	}
	
	// Update is called once per frame
	void Update () {
        deltaTime -= Time.deltaTime;
        if (deltaTime <= 0)
        {
            rens[lastIndex].material.SetColor("_Color", Color.white);
            lastIndex++;
            if (lastIndex >= count) lastIndex = 0;
            rens[lastIndex].material.SetColor("_Color", color);
            deltaTime = intervalTime;
        }
	}
}
