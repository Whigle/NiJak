using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Resource
{
    None,
    Food,
    BuildingMaterial,
    Bananas,
    Sugar,
    Wood,
    Stone,
    Energy
}

public enum Building
{
    Tower,
    Stonery,
    Woodery,
    PowerStation,
    Bananery,
    Sugarery,
    JamMaker,
    BudMarket,
    None,
    Unavailable
}

public struct GridPoint
{
    public int x;
    public int y;
    public GridPoint (int x, int y)
    {
        this.x = x;
        this.y = y;
    }
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
    public int Energy;
}




public class Enums : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
