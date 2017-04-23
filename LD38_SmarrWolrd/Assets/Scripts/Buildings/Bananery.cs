using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bananery : BuildingObject
{

    void Start ()
    {
        buildingCooldown = 10f;
        buildingType = Building.Bananery;
    }

    void Update ()
    {

    }
}
