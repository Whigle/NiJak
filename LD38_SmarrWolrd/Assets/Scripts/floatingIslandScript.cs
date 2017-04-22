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
        direction = new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), 0f);
        direction.Normalize();
        speed = Random.Range(1f, 9f);
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

    #endregion

    #region dodatkowe funkcje

    #endregion
}
