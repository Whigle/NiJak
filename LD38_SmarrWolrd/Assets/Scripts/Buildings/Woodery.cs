using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Woodery : BuildingObject
{
    public int woodAmount;
    public int energyCost;

    public Woodery() : base() { }
    void Start()
    {
        base.Start();
        buildingType = Building.Woodery;
    }

    protected override void iterateProduction()
    {
        if (ResourcesManager.hasResource(Resource.Energy))
        {
            if (ResourcesManager.getResource(Resource.Wood) < ResourcesManager.resourcesCapacity)
            {
                ResourcesManager.decreaseResource(Resource.Energy, energyCost);
                ResourcesManager.increaseResource(Resource.Wood, woodAmount);
            }
        }
    }
}
