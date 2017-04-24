using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bananery : BuildingObject
{
    public int bananasAmount;
    public int energyCost;

    public Bananery() : base() { }
    void Start()
    {
        base.Start();
        buildingType = Building.Bananery;
    }

    protected override void iterateProduction()
    {
        if (ResourcesManager.hasResource(Resource.Energy, energyCost))
        {
            if (ResourcesManager.getResource(Resource.Bananas) < ResourcesManager.resourcesCapacity)
            {
                ResourcesManager.increaseResource(Resource.Bananas, bananasAmount);
                ResourcesManager.decreaseResource(Resource.Energy, energyCost);
            }
        }
    }
}
