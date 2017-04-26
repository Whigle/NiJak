﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerTower : BuildingObject
{
    public int smogAmount;
    public int energyAmount;
    static public int increaseOverTime = 0;
    static public double energyConsumptionOverTime = 0;
    static public int pollutionOverTime = 0;
    static public double productionFrequency;
    static public int enabledStructures = 0;
    static public int totalStructures = 0;
    public PowerTower() : base() {}
    
    void Start ()
    {
        base.Start();
        enabledStructures++;
        totalStructures++;
        increaseOverTime += energyAmount;
        pollutionOverTime += smogAmount;
        productionFrequency = buildingCooldown;
        buildingType = Building.PowerTower;
    }

    protected override void iterateProduction()
    {
        if (ResourcesManager.getResource(Resource.Energy) < ResourcesManager.resourcesCapacity)
        {
            if (!enabled)
            {
                enabledStructures++;
                increaseOverTime += energyAmount;
                pollutionOverTime += smogAmount;
                enabled = true;
            }
            ResourcesManager.increaseResource(Resource.Energy, energyAmount);
            ResourcesManager.increaseResource(Resource.Smog, smogAmount);
        }
        else {
            if (enabled)
            {
                enabledStructures--;
                increaseOverTime -= energyAmount;
                pollutionOverTime -= smogAmount;
                enabled = false;
            }
        }
    }

    ~PowerTower()
    {
        if (enabled)
        {
            enabledStructures--;
            increaseOverTime -= energyAmount;
            pollutionOverTime -= smogAmount;
            enabled = false;
        }
        totalStructures--;
    }
}
