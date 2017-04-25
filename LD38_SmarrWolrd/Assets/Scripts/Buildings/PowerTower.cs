using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerTower : BuildingObject
{
    public int smogAmount;
    public int energyAmount;
    static public int increaseOverTime = 0;
    static public int pollutionOverTime = 0;
    static public double productionFrequency;
    public PowerTower() : base() {}
    void Start ()
    {
        base.Start();
        increaseOverTime += energyAmount;
        pollutionOverTime += smogAmount;
        productionFrequency = buildingCooldown;
        buildingType = Building.PowerTower;
    }

    protected override void iterateProduction()
    {
        if (ResourcesManager.getResource(Resource.Energy) < ResourcesManager.resourcesCapacity)
        {
            if (!enabled)
            {
                increaseOverTime += energyAmount;
                pollutionOverTime += smogAmount;
                enabled = true;
            }
            ResourcesManager.increaseResource(Resource.Energy, energyAmount);
            ResourcesManager.increaseResource(Resource.Smog, smogAmount);
        }
        else {
            increaseOverTime -= energyAmount;
            pollutionOverTime -= smogAmount;
            enabled = false;
        }
        print(increaseOverTime);
    }
}
