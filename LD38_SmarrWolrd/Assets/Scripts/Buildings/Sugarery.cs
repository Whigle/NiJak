﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sugarery : BuildingObject
{

    void Start ()
    {
        buildingCooldown = 10f;
        buildingType = Building.Sugarery;
    }

    void Update ()
    {

    }
}
