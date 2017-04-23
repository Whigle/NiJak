using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ResourcesManager : MonoBehaviour
{

    public static Dictionary <Resource, int> resources;
    public Texture [] resourcesTextures;
    static public int resourcesCapacity = 1000;

    static ResourcesManager ()
    {
        resources = new Dictionary<Resource, int> ();
        resources.Add (Resource.Food, 100);
        resources.Add (Resource.BuildingMaterial, 100);
        resources.Add (Resource.Bananas, 0);
        resources.Add (Resource.Sugar, 0);
        resources.Add (Resource.Wood, 0);
        resources.Add (Resource.Stone, 0);
    }
    
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            string str = "Resources on connected isles:\n";
            foreach (KeyValuePair<Resource,int> res in getResourcesFromIslands())
            {
                str += res.Key + ": " + res.Value+'\n';
            }
            print(str);
        }
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
            //MonoBehaviour.print(resourceName+" added: "+value+" In stockpile: "+resources[resourceName]);
            return true;
        }
        else
            return false;
    }

    static public bool zeroResource (Resource resourceName)
    {
        if (resources.ContainsKey (resourceName))
        {
            resources [resourceName] = 0;
            return true;
        }
        else
            return false;
    }

    static public bool clearStockpile ()
    {
        zeroResource (Resource.Bananas);
        zeroResource (Resource.BuildingMaterial);
        zeroResource (Resource.Food);
        zeroResource (Resource.Stone);
        zeroResource (Resource.Sugar);
        zeroResource (Resource.Wood);

        return true;
    }

    static public List<KeyValuePair<Resource,int>> getResourcesFromIslands()
    {
        List<KeyValuePair<Resource, int>> ret = new List<KeyValuePair<Resource, int>>();
        foreach (GameObject island in getIslandsConnected())
        {
            if (island.GetComponent<floatingIslandScript>()!=null)
                ret.Add(new KeyValuePair<Resource, int> (island.GetComponent<floatingIslandScript>().resource, island.GetComponent<floatingIslandScript>().resourceCount));
        }
        return ret;
    }

    static public List<GameObject> getIslandsConnected(){
        List<GameObject> ret = new List<GameObject>();
        foreach (GameObject island in GameObject.FindGameObjectsWithTag("Island"))
        {
            if (island.GetComponent<floatingIslandScript>() != null)
            {
                if (island.GetComponent<floatingIslandScript>().partOfIsland) ret.Add(island);
            }
        }
        return ret;
    }
    static public List<GameObject> getIslandsOfTypeConnected(Resource resourceType)
    {
        List<GameObject> ret = new List<GameObject>();
        foreach (GameObject island in GameObject.FindGameObjectsWithTag("Island"))
        {
            if (island.GetComponent<floatingIslandScript>() != null)
            {
                if (island.GetComponent<floatingIslandScript>().partOfIsland)
                {
                    if (island.GetComponent<floatingIslandScript>().resource==resourceType) ret.Add(island);
                }
            }
        }
        return ret;
    }

    void OnGUI()
    {
        int i = 0, width=75, height=40, spacing=5;
        foreach (KeyValuePair<Resource, int> pair in resources)
        {
            GUIContent content;
            if (i < resourcesTextures.Length)
            {
                content = new GUIContent(pair.Value.ToString(), resourcesTextures[i]);
            }
            else
            {
                content = new GUIContent(pair.Key.ToString()+": "+pair.Value.ToString());
            }
            GUI.Box(new Rect(width * i + spacing, 0, width, height), content);
            i++;
        }
    }

}
