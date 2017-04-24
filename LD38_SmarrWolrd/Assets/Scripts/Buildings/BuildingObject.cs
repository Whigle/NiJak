using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingObject : MonoBehaviour
{
    public string name = "";
    public string description = "";
    public Texture buildingImage;
    public double buildingCooldown;
    public Building buildingType;
    public bool enabled = true;
    protected DateTime time;
    public TimeSpan frequency;

    public BuildingObject()
    {
        time = DateTime.Now;
    }

    protected void Start ()
    {
        frequency = TimeSpan.FromSeconds(buildingCooldown);
    }

    void Update ()
    {
        if (enabled)
        {
            
            if ((DateTime.Now - time) >= frequency)
            {
                iterateProduction();
                time = DateTime.Now;
            }
        }
    }

    virtual protected void iterateProduction() { }


    void OnTriggerEnter (Collider col)
    {
        if (col.gameObject.CompareTag ("ChunkIsland") || col.gameObject.CompareTag ("Smog"))
        {
            Destroy (gameObject);
        }
    }
}
