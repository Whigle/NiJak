using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Woodery : BuildingObject
{
    public int woodAmount;
    public int energyCost;
    static public int increaseOverTime = 0;
    static public double productionFrequency;

    public Woodery() : base() {}
    void Start()
    {
        base.Start();
        increaseOverTime += woodAmount;
        PowerTower.energyConsumptionOverTime += (energyCost / frequency.TotalSeconds);
        productionFrequency = buildingCooldown;
        buildingType = Building.Woodery;
    }

    protected override void iterateProduction()
    {
        if (ResourcesManager.hasResource(Resource.Energy))
        {
            if (ResourcesManager.getResource(Resource.Wood) < ResourcesManager.resourcesCapacity)
            {
                if (!enabled)
                {
                    PowerTower.energyConsumptionOverTime += (energyCost / frequency.TotalSeconds);
                    increaseOverTime += woodAmount;
                    enabled = true;
                }
                ResourcesManager.decreaseResource(Resource.Energy, energyCost);
                ResourcesManager.increaseResource(Resource.Wood, woodAmount);

            }
            else
            {
                if (enabled)
                {
                    PowerTower.energyConsumptionOverTime -= (energyCost / frequency.TotalSeconds);
                    increaseOverTime -= woodAmount;
                    enabled = false;
                }
            }
        }
        else
        {
            if (enabled)
            {
                PowerTower.energyConsumptionOverTime -= (energyCost / frequency.TotalSeconds);
                increaseOverTime -= woodAmount;
                enabled = false;
            }
        }
    }
}
