using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class initialIslandScript : MonoBehaviour {

    #region zmienne

    static public MainGame mainGame;

    #endregion

    #region główne funkcje

    // Use this for initialization
    void Start () {
        if (mainGame == null) mainGame = GameObject.Find("MainGame").GetComponent<MainGame>();
    }
    
    // Update is called once per frame
    void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.GetComponent<floatingIslandScript>().speed = 0f;
        collision.gameObject.GetComponent<floatingIslandScript>().direction = new Vector3(0, 0, 0);
        collision.gameObject.GetComponent<floatingIslandScript>().partOfIsland = true;
        Vector3 temp = collision.gameObject.transform.position;
        temp.x = System.Convert.ToInt32(temp.x);
        temp.y = System.Convert.ToInt32(temp.y);
        collision.gameObject.transform.position = temp;
        mainGame.removeIslandfromList(collision.gameObject);
        collision.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        float x = transform.localScale.x;
        float y = transform.localScale.y;
        float transx = 1 / x;
        float transy = 1 / y;
        for(int i=0;i<x;i++)
        {
            for(int j=0;j<y;j++)
            {
                Vector3 temp = transform.position;
                temp.x += i - (int)x / 2;
                temp.y += j - (int)y / 2;

                Gizmos.DrawCube(temp, new Vector3(0.9f, 0.9f, 1.1f));
            }
        }
    }

    #endregion

    #region dodatkowe funkcje

    #endregion

}
