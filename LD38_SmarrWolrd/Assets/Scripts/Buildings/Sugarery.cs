using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sugarery : BuildingObject
{

    public int sugarAmount;
    public int energyCost;
    static public int increaseOverTime = 0;
    static public double productionFrequency;

    public Sugarery() : base() {}
    void Start()
    {
        base.Start();
        increaseOverTime += sugarAmount;
        PowerTower.energyConsumptionOverTime += (energyCost / frequency.TotalSeconds);
        productionFrequency = buildingCooldown;
        buildingType = Building.Sugarery;
    }

    protected override void iterateProduction()
    {
        if (ResourcesManager.hasResource(Resource.Energy, energyCost))
        {
            if (ResourcesManager.getResource(Resource.Sugar) < ResourcesManager.resourcesCapacity)
            {
                if (!enabled)
                {
                    PowerTower.energyConsumptionOverTime += (energyCost / frequency.TotalSeconds);
                    increaseOverTime += sugarAmount;
                    enabled = true;
                }
                ResourcesManager.decreaseResource(Resource.Energy, energyCost);
                ResourcesManager.increaseResource(Resource.Sugar, sugarAmount);

            }
            else
            {
                if (enabled)
                {
                    PowerTower.energyConsumptionOverTime -= (energyCost / frequency.TotalSeconds);
                    increaseOverTime -= sugarAmount;
                    enabled = false;
                }
            }
        }
        else
        {
            if (enabled)
            {
                PowerTower.energyConsumptionOverTime -= (energyCost / frequency.TotalSeconds);
                increaseOverTime -= sugarAmount;
                enabled = false;
            }
        }
    }
}
