using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This class is responsible for moving a platform.
public class platform7 : MonoBehaviour {

    float speed=2f;
    bool facingRight = true;
 //This code section is insprired by Alexander Zotov with few changes.
	void Update () {
        //checks the current position of the platform, if it has reached its limit, then go the other direction.
		if(transform.position.x > -0.5f)
        {
            facingRight = false;
        }
        if (transform.position.x < -3.5f)
        {
            facingRight = true;
        }
        if (facingRight)
        {
            //Tells the platform to start moving in this direction.
            transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
        }
        else
        {
            transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
        }
    }
}
