using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingObject : MonoBehaviour
{
    public string name = "";
    public string description = "";
    public Texture buildingImage;
    public float buildingCooldown;
    public Building buildingType;

    void Start ()
    {

    }

    void Update ()
    {

    }


    void OnTriggerEnter (Collider col)
    {
        if (col.gameObject.CompareTag ("ChunkIsland"))
        {
            Destroy (gameObject);
        }
    }
}
