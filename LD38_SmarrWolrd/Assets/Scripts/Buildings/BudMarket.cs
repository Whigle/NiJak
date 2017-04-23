using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BudMarket : BuildingObject
{

    void Start ()
    {
        buildingCooldown = 10f;
        buildingType = Building.BudMarket;
    }

    void Update ()
    {

    }
}
