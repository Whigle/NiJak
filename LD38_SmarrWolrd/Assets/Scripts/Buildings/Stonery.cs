using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stonery : BuildingObject
{

    void Start ()
    {
        buildingCooldown = 10f;
        buildingType = Building.Stonery;
    }

    void Update ()
    {

    }
}
