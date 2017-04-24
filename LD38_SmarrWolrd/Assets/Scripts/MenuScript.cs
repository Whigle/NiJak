using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Time.timeScale = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
	}

    public void clickStartButton()
    {
        GameObject.Find("BuildingsUI").GetComponent<Canvas>().enabled=true;
        ResourcesManager.showResources = true;
        CameraScript.allowCameraMovement = true;
        GameObject.Find("StartScreen").gameObject.GetComponent<Canvas>().enabled = false;
        Time.timeScale = 1;
    }
    public void clickQuitButton()
    {
        Application.Quit();
    }
}
