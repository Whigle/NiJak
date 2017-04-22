using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingSelection : MonoBehaviour
{
    public List<Button> buildButtons;
    public List<Image> buildImages;
    public Material green;
    public Material red;
    public Material grey;

    void Start ()
    {

    }

    void Update ()
    {

    }

    void LateUpdate ()
    {
        HighLightButtons ();
    }

    public void Selected (Button button)
    {

    }

    private void HighLightButtons ()
    {
        foreach (Button button in buildButtons)
        {
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

}
