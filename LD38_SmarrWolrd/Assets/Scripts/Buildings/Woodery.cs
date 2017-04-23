using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Woodery : BuildingObject
{
    public int woodAmount = 10;

    public Woodery() : base() { }
    void Start()
    {
        base.Start();
        buildingType = Building.Woodery;
    }

    protected override void iterateProduction()
    {
        if (ResourcesManager.getResource(Resource.Wood) < ResourcesManager.resourcesCapacity)
        {
            ResourcesManager.increaseResource(Resource.Wood, woodAmount);
        }
    }
}
