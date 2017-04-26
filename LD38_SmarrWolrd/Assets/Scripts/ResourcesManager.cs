using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ResourcesManager : MonoBehaviour
{

    public static Dictionary <Resource, int> resources;
    public Texture [] resourcesTextures;
    public Texture populationTexture;
    public Texture2D tooltipBackground;
    public string [] resourcesTooltips;
    string[] resourcesTooltipsExtended = new string[8];
    static public int resourcesCapacity = 1000;
    static DateTime time;
    static public bool showResources = false;
    static public int smogReduction=0;
    public TimeSpan span=TimeSpan.FromSeconds(10);
    int screenHeightGUIModifier = 1;
    int screenWidthGUIModifier = 1;
    static GUIStyle style;
    static GUIStyleState styleState = new GUIStyleState();

    static ResourcesManager ()
    {
        resources = new Dictionary<Resource, int> ();
        resources.Add (Resource.Food, 300);
        resources.Add (Resource.BuildingMaterial, 150);
        resources.Add (Resource.Bananas, 0);
        resources.Add (Resource.Sugar, 0);
        resources.Add (Resource.Wood, 0);
        resources.Add (Resource.Stone, 0);
        resources.Add (Resource.Energy, 100);
        resources.Add (Resource.Smog, 0);
        time = DateTime.Now;
    }
    public void Start()
    {
        //style.font = new Font("Arial");
        //styleState.background=tooltipBackground;
        //styleState.textColor = Color.white;
        //style.wordWrap = true;
        //style.border = new RectOffset();
        //style.normal = styleState;
    }
    public void Update()
    {
        if (DateTime.Now - time > span)
        {
            smogReduction = BuildingManager.getIslandsOfTypeConnected(Resource.Wood).Count;
            decreaseResource(Resource.Smog, smogReduction);
            time = DateTime.Now;
        }
        resourcesTooltipsExtended[0] = resourcesTooltips[0] + "\nProduction: " + JamMaker.increaseOverTime + " / " + JamMaker.productionFrequency + " sec.";
        resourcesTooltipsExtended[0] += "\nWorking structures: " + JamMaker.enabledStructures + " / " + JamMaker.totalStructures;
        if (JamMaker.totalStructures > 0)
        {
            if (Sugarery.enabledStructures == 0 || (BuildingObject.sugarGlobalCost > (Sugarery.increaseOverTime / Sugarery.productionFrequency)))
            {
                resourcesTooltipsExtended[0] += "\n<color=red>Low on sugar! Build more Sugarery.</color> ";
            }
            if (Bananery.enabledStructures == 0 || (BuildingObject.bananasGlobalCost > (Bananery.increaseOverTime / Bananery.productionFrequency)))
            {
                resourcesTooltipsExtended[0] += "\n<color=red>Low on bananas! Build more Bananery.</color>";
            }
        }
        resourcesTooltipsExtended[1] = resourcesTooltips[1] + "\nProduction: " + BudMarket.increaseOverTime + " / " + BudMarket.productionFrequency + " sec.";
        resourcesTooltipsExtended[1] += "\nWorking structures: " + BudMarket.enabledStructures + " / " + BudMarket.totalStructures;
        if (Stonery.productionFrequency!=0 && (BuildingObject.stoneGlobalCost > (Stonery.increaseOverTime / Stonery.productionFrequency)))
        {
            resourcesTooltipsExtended[1] += "\n<color=red>Low on stone! Build more Stonery.</color>";
        }
        if (Woodery.productionFrequency!=0 && (BuildingObject.woodGlobalCost > (Woodery.increaseOverTime / Woodery.productionFrequency)))
        {
            resourcesTooltipsExtended[1] += "\n<color=red>Low on wood! Build more Woodery.</color>";
        }
        resourcesTooltipsExtended[2] = resourcesTooltips[2] + "\nProduction: " + Bananery.increaseOverTime + " / " + Bananery.productionFrequency + " sec.";
        resourcesTooltipsExtended[2] += "\nWorking structures: " + Bananery.enabledStructures + " / " + Bananery.totalStructures;
        resourcesTooltipsExtended[3] = resourcesTooltips[3] + "\nProduction: " + Sugarery.increaseOverTime + " / " + Sugarery.productionFrequency + " sec.";
        resourcesTooltipsExtended[3] += "\nWorking structures: " + Sugarery.enabledStructures + " / " + Sugarery.totalStructures;
        resourcesTooltipsExtended[4] = resourcesTooltips[4] + "\nProduction: " + Woodery.increaseOverTime + " / " + Woodery.productionFrequency + " sec.";
        resourcesTooltipsExtended[4] += "\nWorking structures: " + Woodery.enabledStructures + " / " + Woodery.totalStructures;
        resourcesTooltipsExtended[5] = resourcesTooltips[5] + "\nProduction: " + Stonery.increaseOverTime + " / " + Stonery.productionFrequency + " sec.";
        resourcesTooltipsExtended[5] += "\nWorking structures: " + Stonery.enabledStructures + " / " + Stonery.totalStructures;
        resourcesTooltipsExtended[6] = resourcesTooltips[6] + "\nProduction: " + PowerTower.increaseOverTime + " / " + PowerTower.productionFrequency + " sec."
                                                            + "\nConsumption: " + PowerTower.energyConsumptionOverTime * PowerTower.productionFrequency + " / " + PowerTower.productionFrequency + " sec.";
        int energyConsumingStructures = Bananery.totalStructures + Sugarery.totalStructures + Woodery.totalStructures + Stonery.totalStructures + BudMarket.totalStructures + JamMaker.totalStructures;
        int enabledStructures = Bananery.enabledStructures + BudMarket.enabledStructures + JamMaker.enabledStructures + Stonery.enabledStructures + Sugarery.enabledStructures + Woodery.enabledStructures;
        int totalStructures = Bananery.totalStructures + BudMarket.totalStructures + JamMaker.totalStructures + Stonery.totalStructures + Sugarery.totalStructures + Woodery.totalStructures;
        resourcesTooltipsExtended[6] += "\nPowered structures: " + enabledStructures + " / " + totalStructures;
        if (PowerTower.energyConsumptionOverTime== 0 && energyConsumingStructures==0)
        {
            resourcesTooltipsExtended[6] += "\n<color=white>No need for energy.</color>";
        }
        else if((PowerTower.increaseOverTime / PowerTower.productionFrequency) >= (PowerTower.energyConsumptionOverTime))
        {
            resourcesTooltipsExtended[6] += "\n<color=lime>Structures powered.</color>";
        }
        else
        {
            resourcesTooltipsExtended[6] += "\n<color=red>Low on energy! You need more Power Towers.</color>";
        }
        resourcesTooltipsExtended[7] = resourcesTooltips[7] + "\nProduction: " + PowerTower.pollutionOverTime + " / " + PowerTower.productionFrequency + " sec."
                                                            + "\nReduction: " + smogReduction + " / " + span.TotalSeconds + " sec.";
        
        if (((PowerTower.pollutionOverTime / PowerTower.productionFrequency) == (smogReduction / span.TotalSeconds)) || PowerTower.productionFrequency==0)
        {
            resourcesTooltipsExtended[7] += "\n<color=white>Level of smog is stable.</color>";
        }
        else if ((PowerTower.pollutionOverTime / PowerTower.productionFrequency) < (smogReduction / span.TotalSeconds))
        {
            resourcesTooltipsExtended[7] += "\n<color=lime>Smog is decreasing.</color>";
        }
        else {
            resourcesTooltipsExtended[7] += "\n<color=red>Smog is increasing! You need more trees.</color>";
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

    static public void useResources(Resources r)
    {
        decreaseResource(Resource.Bananas, r.Bananas);
        decreaseResource(Resource.BuildingMaterial, r.BuildingMaterial);
        decreaseResource(Resource.Food, r.Food);
        decreaseResource(Resource.Stone, r.Stone);
        decreaseResource(Resource.Sugar, r.Sugar);
        decreaseResource(Resource.Wood, r.Wood);
        decreaseResource(Resource.Energy, r.Energy);

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

    public static Building ResourceToBuilding (Resource r)
    {
        switch (r)
        {
            case Resource.Bananas:
                return Building.Bananery;
            case Resource.Stone:
                return Building.Stonery;
            case Resource.Sugar:
                return Building.Sugarery;
            case Resource.Wood:
                return Building.Woodery;
            default:
                return Building.None;
        }
    }

    void OnGUI()
    {
        if (showResources)
        {
            style = new GUIStyle(GUI.skin.textArea);
            style.richText = true;
            style.clipping = TextClipping.Overflow;
            screenHeightGUIModifier = Screen.height / 20;
            screenWidthGUIModifier = Screen.width / 20;
            int i = 0, spacing = 20;
            float width = 1.5f, height = 1.25f; 
            width *= screenWidthGUIModifier;
            height *= screenHeightGUIModifier;
            GUIContent content;
            string populationInfo = "";
            if (minionScript.die) populationInfo = "\n<color=red>Your population is dying out!</color>";
            else populationInfo = "\n<color=lime>Your population is growing.</color>";
            content = new GUIContent(minionScript.population + "/" + minionScript.maxPop, populationTexture, "Island population."+populationInfo);
            GUI.Box(new Rect(width * i + spacing, spacing, width, height), content);
            GUI.Label(new Rect(0 + spacing, height, width * i, height * 2 + (populationInfo.Length-populationInfo.Trim('\n').Length)*10), GUI.tooltip);
            i++;
            foreach (KeyValuePair<Resource, int> pair in resources)
            {
                if (i - 1 < resourcesTextures.Length)
                {
                    if (i -1 < resourcesTooltips.Length)
                        content = new GUIContent(pair.Value.ToString(), resourcesTextures[i - 1], resourcesTooltipsExtended[i - 1]);
                    else
                        content = new GUIContent(pair.Value.ToString(), resourcesTextures[i - 1]);
                }
                else
                {
                    if (i - 1 < resourcesTooltips.Length)
                        content = new GUIContent(pair.Key.ToString() + ": " + pair.Value.ToString(), resourcesTooltipsExtended[i - 1]);
                    else
                        content = new GUIContent(pair.Key.ToString() + ": " + pair.Value.ToString());
                }
                GUI.Box(new Rect(width * i + spacing, spacing, width, height), content);
                i++;
            }
            if(GUI.tooltip!="") GUI.TextArea(new Rect(Input.mousePosition.x+15, Screen.height-Input.mousePosition.y+20, width*3, height*2 +(populationInfo.Length - populationInfo.Trim('\n').Length) * 10), GUI.tooltip,style);
        }
    }

}
