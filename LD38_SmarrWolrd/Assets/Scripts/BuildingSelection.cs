using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingSelection : MonoBehaviour
{
    public List<Button> buildButtons;


    void Start ()
    {

    }

    void Update ()
    {

    }

    public void Selected (Button button)
    {
        print (button.GetComponentInChildren<Text>().text);
    }



}
