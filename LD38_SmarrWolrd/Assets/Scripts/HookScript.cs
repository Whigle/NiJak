using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookScript : MonoBehaviour
{

    Vector3 startPosition;
    Vector3 targetPosition;
    LineRenderer lineRenderer;
    public float speed = 1f;
    bool hookShooting = false;
    bool hookReturning = false;
    bool hookEnabled = true;
    bool trigger = false;
    bool hookReturned = false;
    bool hookGotIsland = false;
    float lineLength = 10f;
    GridPoint gridPosition;
    MainGame mainGame;

    // Use this for initialization
    void Start ()
    {

        lineRenderer = gameObject.GetComponent<LineRenderer> ();
        lineRenderer.numPositions = 2;
        lineRenderer.enabled = false;
        startPosition = transform.position;
        gridPosition = new GridPoint (100, 100);
        mainGame = FindObjectOfType<MainGame> ();
        //hookEnabled = true;
    }

    // Update is called once per frame
    void Update ()
    {
        if (hookEnabled)
        {
            if (hookReturned && hookGotIsland)
                Destroy (gameObject);
            //Cursor.visible=false;
            if (!hookShooting && !hookReturning)
            {
                //GetInput ();
                RaycastHit myRay = new RaycastHit ();
                Physics.Raycast (Camera.allCameras [1].ScreenPointToRay (Input.mousePosition), out myRay);
                if (myRay.transform != null)
                {
                    if (myRay.transform.gameObject.tag == "RayCatcher")
                    {
                        Vector3 rayCatcherPosition = myRay.transform.position;
                        //float dist = Vector3.Distance(new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0f), new Vector3(Camera.allCameras[1].transform.position.x, Camera.allCameras[1].transform.position.y, 0f));
                        /*targetPosition = myRay.point;
                        targetPosition.x -= rayCatcherPosition.x;
                        targetPosition.y -= rayCatcherPosition.y;
                        targetPosition.x -= startPosition.x;
                        targetPosition.y -= startPosition.y;
                        targetPosition.y += 1f;
                        targetPosition *= 1000;
                        targetPosition.z = startPosition.z;*/
                        Vector3 p = new Vector3();
                        Camera c = Camera.main;
                        Event e = Event.current;
                        Vector2 mousePos = new Vector2();

                        // Get the mouse position from Event.
                        // Note that the y position from Event is inverted.
                        mousePos.x = Input.mousePosition.x;
                        mousePos.y = Input.mousePosition.y;

                        targetPosition = c.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, -c.transform.position.z));
                        targetPosition.z = startPosition.z;
                        transform.LookAt (targetPosition);
                    }
                }
                if (Input.GetMouseButton (0))
                {
                    hookShooting = false;
                    ShootHook (startPosition + transform.forward * 20);
                }
            }
        }
        if (hookShooting)
        {
            lineRenderer.SetPosition (1, gameObject.transform.position);
            transform.Translate (new Vector3 (0f, 0f, 1f * speed * Time.deltaTime));
            if (Vector3.Distance (startPosition, transform.position) > lineLength)
            {
                hookShooting = false;
                hookReturning = true;
            }
        }
        if (hookReturning)
        {
            lineRenderer.SetPosition (1, gameObject.transform.position);
            transform.Translate (new Vector3 (0f, 0f, -1f * speed * Time.deltaTime));
            if (Vector3.Distance (startPosition, transform.position) <= 0.5f)
            {
                transform.position = startPosition;
                hookReturning = false;
                lineRenderer.enabled = false;
                trigger = false;
                hookReturned = true;
            }
        }
    }

    void ShootHook (Vector3 targetPos)
    {
        transform.position = startPosition;
        targetPosition = targetPos;
        lineRenderer.SetPosition (0, startPosition);
        lineRenderer.enabled = true;
        hookShooting = true;
        hookReturned = false;
    }

    void returnHook ()
    {
        hookShooting = false;
        hookReturning = true;
        hookGotIsland = true;
    }
    void OnTriggerEnter (Collider col)
    {
        if (!trigger)
        {
            if (col.GetComponent<floatingIslandScript> () != null)
            {
                if (!col.GetComponent<floatingIslandScript> ().partOfIsland)
                {
                    col.GetComponent<floatingIslandScript> ().direction = startPosition - transform.position;
                    col.GetComponent<floatingIslandScript>().direction.z = 0;
                    col.GetComponent<floatingIslandScript> ().direction.Normalize ();
                    col.GetComponent<floatingIslandScript> ().speed = speed;
                    col.GetComponent<floatingIslandScript>().hooked = true;
                    returnHook ();
                    trigger = true;
                }
            }
        }
    }

    private void GetInput ()
    {
        if (Input.GetKeyDown (KeyCode.W))
        {
            if (mainGame.grid [gridPosition.x] [gridPosition.y + 1] == 1)
            {
                transform.position += Vector3.up;
                startPosition = transform.position;
                gridPosition.y += 1;
            }
        }
        if (Input.GetKeyDown (KeyCode.S))
        {
            if (mainGame.grid [gridPosition.x] [gridPosition.y - 1] == 1)
            {
                transform.position += Vector3.down;
                startPosition = transform.position;
                gridPosition.y -= 1;
            }
        }
        if (Input.GetKeyDown (KeyCode.A))
        {
            if (mainGame.grid [gridPosition.x - 1] [gridPosition.y] == 1)
            {
                transform.position += Vector3.left;
                startPosition = transform.position;
                gridPosition.x -= 1;
            }
        }
        if (Input.GetKeyDown (KeyCode.D))
        {
            if (mainGame.grid [gridPosition.x + 1] [gridPosition.y] == 1)
            {
                transform.position += Vector3.right;
                startPosition = transform.position;
                gridPosition.x += 1;
            }
        }
    }

}
