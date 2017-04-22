using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Resource
{
    Food,
    BuildingMaterial,
    Bananas,
    Sugar,
    Wood,
    Stone
}

public enum Building
{
    Tower,
    Mine,
    Sawmill,
    PowerStation
}

[System.Serializable]
public class Resources
{
    public int Food;
    public int BuildingMaterial;
    public int Bananas;
    public int Sugar;
    public int Wood;
    public int Stone;
}




public class Enums : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
