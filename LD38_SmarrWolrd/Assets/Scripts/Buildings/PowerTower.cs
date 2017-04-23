using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerTower : BuildingObject
{
    public int energyAmount;
    public PowerTower() : base() { }
    void Start ()
    {
        base.Start();
        buildingType = Building.PowerTower;
    }

    protected override void iterateProduction()
    {
        if (ResourcesManager.getResource(Resource.Energy) < ResourcesManager.resourcesCapacity)
        {
            ResourcesManager.increaseResource(Resource.Energy, energyAmount);
        }
    }
}
