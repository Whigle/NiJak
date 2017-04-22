using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookSpawner : MonoBehaviour
{
    public GameObject hook;
    MainGame main;

    void Start ()
    {
        main = FindObjectOfType<MainGame> ();
    }

    void Update ()
    {
        GetInput ();
    }

    private void GetInput ()
    {
        if (Input.GetKeyDown (KeyCode.E))
        {
            Instantiate (hook);
        }
    }
}
