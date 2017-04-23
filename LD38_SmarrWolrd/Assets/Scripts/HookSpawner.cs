using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookSpawner : MonoBehaviour
{
    public GameObject hook;
    MainGame main;
    GameObject reference;

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
            if(reference==null)
            reference = Instantiate (hook,transform.position + Vector3.back, new Quaternion());
        }
    }
}
