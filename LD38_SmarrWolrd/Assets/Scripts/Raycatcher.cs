using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Raycatcher : MonoBehaviour
{

    void Update()
    {
        gameObject.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, HookScript.startPosition.z);
    }
}
