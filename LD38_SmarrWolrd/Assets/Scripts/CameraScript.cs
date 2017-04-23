using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    public float cameraSpeed = 0.25f;
    public float zoomSpeed = 0.4f;
    public float maxZoom = 2f;
    public float minZoom = 30f;
    public float edgeMargin = 20f;
    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update() {
        Vector3 mousePos = Input.mousePosition;
        if (mousePos.x > 0f && mousePos.x < Screen.width && mousePos.y > 0f && mousePos.y < Screen.height)
        {
            if (mousePos.x <= edgeMargin)
            {
                transform.Translate(new Vector3(-1f, 0f, 0f) * cameraSpeed);
            }
            if (mousePos.y <= edgeMargin)
            {
                transform.Translate(new Vector3(0f, -1f, 0f) * cameraSpeed);
            }
            if (mousePos.x >= Screen.width - edgeMargin)
            {
                transform.Translate(new Vector3(1f, 0f, 0f) * cameraSpeed);
            }
            if (mousePos.y >= Screen.height - edgeMargin)
            {
                transform.Translate(new Vector3(0f, 1f, 0f) * cameraSpeed);
            }
            if (Input.mouseScrollDelta.y > 0)
            {
                if (Mathf.Abs(transform.position.z) > maxZoom)
                {
                    transform.Translate(new Vector3(0f, 0f, 1f) * zoomSpeed);
                }
            }
            if (Input.mouseScrollDelta.y < 0)
            {
                if (Mathf.Abs(transform.position.z) < minZoom)
                {
                    transform.Translate(new Vector3(0f, 0f, -1f) * zoomSpeed);
                }
            }
        }
    }

    
}
