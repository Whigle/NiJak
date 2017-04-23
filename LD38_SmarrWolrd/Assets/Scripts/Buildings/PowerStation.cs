using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerStation : BuildingObject
{

    void Start ()
    {
        buildingCooldown = 5f;
        buildingType = Building.PowerStation;
    }

    void Update ()
    {

    }
}
