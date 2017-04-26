using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bananery : BuildingObject
{
    public int bananasAmount;
    public int energyCost;
    static public int increaseOverTime=0;
    static public double productionFrequency;
    static public int enabledStructures = 0;
    static public int totalStructures = 0;
    public Bananery() : base() {}
    void Start()
    {
        base.Start();
        totalStructures++;
        enabledStructures++;
        increaseOverTime += bananasAmount;
        PowerTower.energyConsumptionOverTime += (energyCost / frequency.TotalSeconds);
        productionFrequency = buildingCooldown;
        buildingType = Building.Bananery;
    }

    protected override void iterateProduction()
    {
        if (ResourcesManager.hasResource(Resource.Energy, energyCost))
        {
            if (ResourcesManager.getResource(Resource.Bananas) < ResourcesManager.resourcesCapacity)
            {
                if (!enabled)
                {
                    enabledStructures++;
                    increaseOverTime += bananasAmount;
                    PowerTower.energyConsumptionOverTime += (energyCost/frequency.TotalSeconds);
                    enabled = true;
                }
                ResourcesManager.increaseResource(Resource.Bananas, bananasAmount);
                ResourcesManager.decreaseResource(Resource.Energy, energyCost);
                
            }
            else
            {
                if (enabled)
                {
                    enabledStructures--;
                    increaseOverTime -= bananasAmount;
                    PowerTower.energyConsumptionOverTime -= (energyCost / frequency.TotalSeconds);
                    enabled = false;
                }
            }
        }
        else
        {
            if (enabled)
            {
                enabledStructures--;
                increaseOverTime -= bananasAmount;
                PowerTower.energyConsumptionOverTime -= (energyCost / frequency.TotalSeconds);
                enabled = false;
            }
        }
    }

    ~Bananery()
    {
        if (enabled)
        {
            enabledStructures--;
            increaseOverTime -= bananasAmount;
            PowerTower.energyConsumptionOverTime -= (energyCost / frequency.TotalSeconds);
            enabled = false;
        }
        totalStructures--;
    }
}
