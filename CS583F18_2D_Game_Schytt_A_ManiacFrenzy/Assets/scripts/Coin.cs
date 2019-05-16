using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The coin class that will be used to intantiate each coin object.
public class Coin : MonoBehaviour {

    public AudioSource coinSound;
	// Use this for initialization
	void Start () {
	}
	  
	// Update is called once per frame
	void Update () {
        //rb.velocity = new Vector2(speed * transform.localScale.x, 0);
	}
    void OnTriggerEnter2D(Collider2D col)
    {
        //Simple check on collider to register which player came in comntact. Then register that player to the Game Manager.
        if (col.gameObject.name.Equals("fighter1"))
        {
            coinSound.Play();
            FindObjectOfType<GameManager>().p1Gain();
            Destroy(gameObject);
        }
        else if (col.gameObject.name.Equals("fighter2"))
        {
            coinSound.Play();
            FindObjectOfType<GameManager>().p2Gain();
            Destroy(gameObject);
        }

        
    }
}
