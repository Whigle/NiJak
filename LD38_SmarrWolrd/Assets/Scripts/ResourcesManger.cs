using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Resource
{
    Food,
    BuildingMaterial,
    Bananas,
    Sugar,
    Wood,
    Stone
}

static public class ResourcesManger
{

    static Dictionary<Resource,int> resources = new Dictionary<Resource, int> ();
    static public int resourcesCapacity=1000;

    static void Start ()
    {
        resources.Add (Resource.Food, 100);
        resources.Add (Resource.BuildingMaterial, 100);
        resources.Add (Resource.Bananas, 0);
        resources.Add (Resource.Sugar, 0);
        resources.Add (Resource.Wood, 0);
        resources.Add (Resource.Stone, 0);
    }

    static void Update ()
    {

    }

    static public bool hasResource (Resource resourceName, int value = 1)
    {
        if (resources.ContainsKey (resourceName))
        {
            if (resources [resourceName] >= value)
                return true;
            return false;
        }
        else
            return false;
    }

    static public int getResource (Resource resourceName)
    {
        if (resources.ContainsKey (resourceName))
        {
            return resources [resourceName];
        }
        else
            return 0;
    }

    static public bool decreaseResource (Resource resourceName, int value)
    {
        if (resources.ContainsKey (resourceName) && (resources [resourceName] - value >= 0))
        {
            resources [resourceName] -= value;
            return true;
        }
        else
            return false;
    }

    static public bool increaseResource (Resource resourceName, int value)
    {
        if (resources.ContainsKey (resourceName))
        {
            if (resources [resourceName] + value >= resourcesCapacity)
            {
                resources [resourceName] = resourcesCapacity;
            }
            else
            {
                resources [resourceName] += value;
            }
            return true;
        }
        else
            return false;
    }

    static public bool clearStockpile ()
    {
        foreach (KeyValuePair<Resource, int> resource in resources)
        {
            resources [resource.Key] = 0;
        }
        return true;
    }

}
