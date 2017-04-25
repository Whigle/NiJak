using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stonery : BuildingObject
{
    public int stoneAmount;
    public int energyCost;
    static public int increaseOverTime = 0;
    static public double productionFrequency;

    public Stonery() : base() {}
    void Start()
    {
        base.Start();
        productionFrequency = buildingCooldown;
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
                    increaseOverTime += stoneAmount;
                    enabled = true;
                }
                ResourcesManager.decreaseResource(Resource.Energy, energyCost);
                ResourcesManager.increaseResource(Resource.Stone, stoneAmount);
            }
            else
            {
                increaseOverTime -= stoneAmount;
                enabled = false;
            }
        }
        else {
            increaseOverTime -= stoneAmount;
            enabled = false;
        }
    }
}
