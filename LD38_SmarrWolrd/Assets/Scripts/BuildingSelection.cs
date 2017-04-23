using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingSelection : MonoBehaviour
{
    //public List<Button> buildButtons;
    //public List<Image> buildImages;
    static List<GameObject> lastIslands =  new List<GameObject>();
    static List<Material> lastMaterials = new List<Material>();
    public Material green;
    public Material red;
    public Material grey;
    public GameObject buildingPrefab;
    static public GameObject selected;
    static GameObject lastselected;
    public Image buildingImage;

    void Start ()
    {
    }

    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) selected = null;
        if (selected!=lastselected) HighlightIslands();
    }

    void LateUpdate ()
    {
        HighlightButtons();
    }

    public void Selected (Button button)
    {
        if (button.gameObject != selected)
        {
            lastselected = selected;
            selected = button.gameObject;
            HighlightIslands();
        }
    }

    private void HighlightButtons ()
    {
        foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("BuildButton"))
        {
            Button button = gameObject.GetComponent<Button>();
            if (button.GetComponent<BuildingRequirements> ().CanBuild ())
            {
                button.GetComponent<Image> ().material = green;
            }
            else
            {
                button.GetComponent<Image> ().material = red;
            }
        }
    }
    
    private void HighlightIslands()
    {
        if (lastIslands.Count > 0)
        {
            for (int i = 0; i < lastIslands.Count; i++)
            {
                lastIslands[i].GetComponent<MeshRenderer>().material = new Material(lastMaterials[i]);
            }
            lastIslands.Clear();
            lastMaterials.Clear();
        }
        foreach (GameObject island in ResourcesManager.getIslandsOfTypeConnected(selected.GetComponent<BuildingRequirements>().islandTypeNeeded))
        {
            if (!lastIslands.Contains(island))
            {
                lastIslands.Add(island);
                lastMaterials.Add(new Material(island.GetComponent<MeshRenderer>().material));
                island.GetComponent<MeshRenderer>().material = red;
            }
        }
    }
}
