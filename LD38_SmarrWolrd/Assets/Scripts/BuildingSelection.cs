using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingSelection : MonoBehaviour
{
    //public List<Button> buildButtons;
    //public List<Image> buildImages;
    static public List<GameObject> lastIslands =  new List<GameObject>();
    static List<Material> lastMaterials = new List<Material>();
    public Material green;
    public Material red;
    public Material active;
    public GameObject buildingPrefab;
    static public GameObject selected;
    static GameObject lastselected;
    public Image buildingImage;
    static GUIStyle style;
    bool showTip = false;
    float tooltipWidth = 200f, tooltipHeight = 75f;
    string costs = "Costs:\n";
    string tooltipText = "";

    void Start ()
    {
        if (GetComponent<BuildingRequirements>() != null)
        {
            if (buildingPrefab.GetComponent<BuildingObject>() != null || buildingPrefab.GetComponent<HookScript>() != null)
            {
                costs = "Costs:\n";
                Resources r = GetComponent<BuildingRequirements>().requiredResources;
                if (r.Bananas > 0)
                {
                    if (ResourcesManager.hasResource(Resource.Bananas, r.Bananas)) costs += "<color=lime>";
                    else costs += "<color=red>";
                    costs += "Bananas: " + r.Bananas + "</color>\n";
                }
                if (r.BuildingMaterial > 0)
                {
                    if (ResourcesManager.hasResource(Resource.BuildingMaterial, r.BuildingMaterial)) costs += "<color=lime>";
                    else costs += "<color=red>";
                    costs += "BuildingMaterial: " + r.BuildingMaterial + "</color>\n";
                }
                if (r.Energy > 0)
                {
                    if (ResourcesManager.hasResource(Resource.Energy, r.Energy)) costs += "<color=lime>";
                    else costs += "<color=red>";
                    costs += "Energy: " + r.Energy + "</color>\n";
                }
                if (r.Food > 0)
                {
                    if (ResourcesManager.hasResource(Resource.Food, r.Food)) costs += "<color=lime>";
                    else costs += "<color=red>";
                    costs += "Food: " + r.Food + "</color>\n";
                }
                if (r.Stone > 0)
                {
                    if (ResourcesManager.hasResource(Resource.Stone, r.Stone)) costs += "<color=lime>";
                    else costs += "<color=red>";
                    costs += "Stone: " + r.Stone + "</color>\n";
                }
                if (r.Sugar > 0)
                {
                    if (ResourcesManager.hasResource(Resource.Sugar, r.Sugar)) costs += "<color=lime>";
                    else costs += "<color=red>";
                    costs += "Sugar: " + r.Sugar + "</color>\n";
                }
                if (r.Wood > 0)
                {
                    if (ResourcesManager.hasResource(Resource.Wood, r.Wood)) costs += "<color=lime>";
                    else costs += "<color=red>";
                    costs += "Wood: " + r.Wood + "</color>\n";
                }

                if (buildingPrefab.GetComponent<BuildingObject>() != null)
                {
                    tooltipText = buildingPrefab.GetComponent<BuildingObject>().name + "\n" + buildingPrefab.GetComponent<BuildingObject>().description + "\n" + costs;
                }
                else
                {
                    tooltipText = "Hook\nPlace Hook on empty field to grab floating island.\n" + costs;
                }
                for (int i = 0; i < tooltipText.Length; i++)
                {
                    if (tooltipText[i] == '\n')
                        tooltipHeight += 12;
                }
            }
        }
        else costs = "";
    }

    void Update ()
    {
        if (GetComponent<BuildingRequirements>() != null)
        {
            if (buildingPrefab.GetComponent<BuildingObject>() != null || buildingPrefab.GetComponent<HookScript>() != null)
            {
                costs = "Costs:\n";
                Resources r = GetComponent<BuildingRequirements>().requiredResources;
                if (r.Bananas > 0)
                {
                    if (ResourcesManager.hasResource(Resource.Bananas, r.Bananas)) costs += "<color=lime>";
                    else costs += "<color=red>";
                    costs += "Bananas: " + r.Bananas + "</color>\n";
                }
                if (r.BuildingMaterial > 0)
                {
                    if (ResourcesManager.hasResource(Resource.BuildingMaterial, r.BuildingMaterial)) costs += "<color=lime>";
                    else costs += "<color=red>";
                    costs += "BuildingMaterial: " + r.BuildingMaterial + "</color>\n";
                }
                if (r.Energy > 0)
                {
                    if (ResourcesManager.hasResource(Resource.Energy, r.Energy)) costs += "<color=lime>";
                    else costs += "<color=red>";
                    costs += "Energy: " + r.Energy + "</color>\n";
                }
                if (r.Food > 0)
                {
                    if (ResourcesManager.hasResource(Resource.Food, r.Food)) costs += "<color=lime>";
                    else costs += "<color=red>";
                    costs += "Food: " + r.Food + "</color>\n";
                }
                if (r.Stone > 0)
                {
                    if (ResourcesManager.hasResource(Resource.Stone, r.Stone)) costs += "<color=lime>";
                    else costs += "<color=red>";
                    costs += "Stone: " + r.Stone + "</color>\n";
                }
                if (r.Sugar > 0)
                {
                    if (ResourcesManager.hasResource(Resource.Sugar, r.Sugar)) costs += "<color=lime>";
                    else costs += "<color=red>";
                    costs += "Sugar: " + r.Sugar + "</color>\n";
                }
                if (r.Wood > 0)
                {
                    if (ResourcesManager.hasResource(Resource.Wood, r.Wood)) costs += "<color=lime>";
                    else costs += "<color=red>";
                    costs += "Wood: " + r.Wood + "</color>\n";
                }

                if (buildingPrefab.GetComponent<BuildingObject>() != null)
                {
                    tooltipText = buildingPrefab.GetComponent<BuildingObject>().name + "\n" + buildingPrefab.GetComponent<BuildingObject>().description + "\n" + costs;
                }
                else
                {
                    tooltipText = "Hook\nPlace Hook on empty field to grab floating island.\n" + costs;
                }
            }
        }
        else costs = "";
        /*if (Input.GetKeyDown(KeyCode.Escape))
        {
            selected = null;
            HighlightIslands();
        }*/
    }

    void LateUpdate ()
    {
        HighlightButtons();
    }

    public void Selected (Button button)
    {
        /*if (button.gameObject != selected)
        {*/
            lastselected = selected;
            selected = button.gameObject;
            HighlightIslands();
            BuildingManager.reqResources = GetComponent<BuildingRequirements>().requiredResources;
        //}
    }

    private void HighlightButtons ()
    {
        foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("BuildButton"))
        {
            Button button = gameObject.GetComponent<Button>();
            if (button.GetComponent<BuildingRequirements> ().CanBuild ())
            {
                button.GetComponent<Image> ().material = green;
                button.enabled = true;
            }
            else
            {
                button.enabled = false;
                button.GetComponent<Image> ().material = red;
            }
        }
    }

    static public void UnHighlightIslands()
    {
        if (lastIslands.Count > 0)
        {
            for (int i = 0; i < lastIslands.Count; i++)
            {
                if (lastIslands[i] != null) lastIslands[i].GetComponent<MeshRenderer>().material = new Material(lastMaterials[i]);
            }
            lastIslands.Clear();
            lastMaterials.Clear();
        }
    }
    private void HighlightIslands()
    {
        UnHighlightIslands();
        if (selected != null)
        {
            foreach (GameObject island in BuildingManager.getIslandsOfTypeConnected(selected.GetComponent<BuildingRequirements>().islandTypeNeeded))
            {
                if (!lastIslands.Contains(island))
                {
                    lastIslands.Add(island);
                    lastMaterials.Add(new Material(island.GetComponent<MeshRenderer>().material));
                    island.GetComponent<MeshRenderer>().material = active;
                }
            }
        }
    }

    public void OnGUI()
    {
        if (showTip)
        {
            style = new GUIStyle(GUI.skin.textArea);
            style.richText = true;
            style.clipping = TextClipping.Overflow;
            if (buildingPrefab != null)
            {
                if (buildingPrefab.GetComponent<BuildingObject>() != null || buildingPrefab.GetComponent<HookScript>() != null)
                {
                    GUI.TextArea(new Rect(Input.mousePosition.x-tooltipWidth, Screen.height - Input.mousePosition.y - tooltipHeight/2, tooltipWidth, tooltipHeight), tooltipText,style);

                }
            }
        }
    }

    public void OnMouseOver()
    {
        showTip = true;
    }
    public void OnMouseExit()
    {
        showTip = false;
    }
}
