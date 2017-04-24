using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bananery : BuildingObject
{
    public int bananasAmount = 10;

    public Bananery() : base() { }
    void Start()
    {
        base.Start();
        buildingType = Building.Bananery;
    }

    protected override void iterateProduction()
    {
        if (ResourcesManager.getResource(Resource.Bananas) < ResourcesManager.resourcesCapacity)
        {
            ResourcesManager.increaseResource(Resource.Bananas, bananasAmount);
        }
    }
}
