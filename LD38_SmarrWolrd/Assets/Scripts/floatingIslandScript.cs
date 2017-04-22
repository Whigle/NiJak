using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floatingIslandScript : MonoBehaviour {

    #region zmienne

    static public MainGame mainGame; 
    public Vector3 direction;
    public float speed;
    public bool partOfIsland;

    #endregion

    #region główne funckje

    // Use this for initialization
    void Awake () {
        int scalex = (int)Random.Range(1f, 3f);
        if (scalex % 2 != 0) scalex++;
        int scaley = (int)Random.Range(1f, 3f);
        if (scaley % 2 != 0) scaley++;
        transform.localScale += new Vector3(scalex, scaley);
        direction = Vector3.zero - transform.position;
        direction.Normalize();
        speed = Random.Range(1f, 2f);
        partOfIsland = false;
        if(mainGame==null) mainGame = GameObject.Find("MainGame").GetComponent<MainGame>();
    }
	
	// Update is called once per frame
	void Update () {
        transform.Translate(direction * speed * Time.deltaTime);
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (!partOfIsland) direction = (transform.position - collision.transform.position).normalized;
        else if (collision.gameObject.GetComponent<floatingIslandScript>() != null && !collision.gameObject.GetComponent<floatingIslandScript>().partOfIsland)
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
    }

    public float x;
    public float y;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        x = transform.localScale.x;
        y = transform.localScale.y;
        float transx = 1 / x;
        float transy = 1 / y;
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                Vector3 temp = transform.position;
                temp.x += i - (int)x / 2;
                if ((int)x % 2 == 0) temp.x += 0.5f;
                temp.y += j-(int)y/2;
                if ((int)y % 2 == 0) temp.y += 0.5f;
                Gizmos.DrawCube(temp, new Vector3(0.9f, 0.9f, 1.1f));
            }
        }
    }

    #endregion

    #region dodatkowe funkcje

    #endregion
}
