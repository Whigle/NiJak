using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JamMaker : BuildingObject
{

    void Start ()
    {
        buildingCooldown = 10f;
        buildingType = Building.JamMaker;
    }

    void Update ()
    {

    }
}
