using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stonery : BuildingObject
{
    public int stoneAmount = 10;

    public Stonery() : base() { }
    void Start()
    {
        base.Start();
        buildingType = Building.Stonery;
    }

    protected override void iterateProduction()
    {
        if (ResourcesManager.getResource(Resource.Stone) < ResourcesManager.resourcesCapacity)
        {
            ResourcesManager.increaseResource(Resource.Stone, stoneAmount);
        }
    }
}
