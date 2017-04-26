using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stonery : BuildingObject
{
    public int stoneAmount;
    public int energyCost;
    static public int increaseOverTime = 0;
    static public double productionFrequency;
    static public int enabledStructures = 0;
    static public int totalStructures = 0;

    public Stonery() : base() {}
    void Start()
    {
        base.Start();
        totalStructures++;
        enabledStructures++;
        productionFrequency = buildingCooldown;
        PowerTower.energyConsumptionOverTime += (energyCost / frequency.TotalSeconds);
        increaseOverTime += stoneAmount;
        buildingType = Building.Stonery;
    }

    protected override void iterateProduction()
    {
        if (ResourcesManager.hasResource(Resource.Energy, energyCost))
        {
            if (ResourcesManager.getResource(Resource.Stone) < ResourcesManager.resourcesCapacity)
            {
                if (!enabled)
                {
                    enabledStructures++;
                    PowerTower.energyConsumptionOverTime += (energyCost / frequency.TotalSeconds);
                    increaseOverTime += stoneAmount;
                    enabled = true;
                }
                ResourcesManager.decreaseResource(Resource.Energy, energyCost);
                ResourcesManager.increaseResource(Resource.Stone, stoneAmount);
            }
            else
            {
                if (enabled)
                {
                    enabledStructures--;
                    PowerTower.energyConsumptionOverTime -= (energyCost / frequency.TotalSeconds);
                    increaseOverTime -= stoneAmount;
                    enabled = false;
                }
            }
        }
        else
        {
            if (enabled)
            {
                enabledStructures--;
                PowerTower.energyConsumptionOverTime -= (energyCost / frequency.TotalSeconds);
                increaseOverTime -= stoneAmount;
                enabled = false;
            }
        }
    }

    ~Stonery()
    {
        if (enabled)
        {
            enabledStructures--;
            PowerTower.energyConsumptionOverTime -= (energyCost / frequency.TotalSeconds);
            increaseOverTime -= stoneAmount;
            enabled = false;
        }
        totalStructures--;
    }
}
