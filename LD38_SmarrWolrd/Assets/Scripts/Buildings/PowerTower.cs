using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerTower : BuildingObject
{

    void Start ()
    {
        buildingCooldown = 5f;
        buildingType = Building.PowerTower;
    }

    void Update ()
    {

    }
}
