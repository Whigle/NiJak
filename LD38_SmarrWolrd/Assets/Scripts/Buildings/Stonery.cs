using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stonery : BuildingObject
{
    public int stoneAmount;
    public int energyCost;

    public Stonery() : base() { }
    void Start()
    {
        base.Start();
        buildingType = Building.Stonery;
    }

    protected override void iterateProduction()
    {
        if (ResourcesManager.hasResource(Resource.Energy, energyCost))
        {
            if (ResourcesManager.getResource(Resource.Stone) < ResourcesManager.resourcesCapacity)
            {
                ResourcesManager.decreaseResource(Resource.Energy, energyCost);
                ResourcesManager.increaseResource(Resource.Stone, stoneAmount);
            }
        }
    }
}
