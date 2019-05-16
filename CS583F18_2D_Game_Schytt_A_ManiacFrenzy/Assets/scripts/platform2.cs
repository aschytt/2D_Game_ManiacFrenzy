using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This class is responsible for moving a platform.
public class platform2 : MonoBehaviour {

    float speed = 1f;
    bool facingUp= true;

    //This code section is insprired by Alexander Zotov with few changes.
    void Update()
    {
        //checks the current position of the platform, if it has reached its limit, then go the other direction.
        if (transform.position.y > 2f)
        {
            facingUp = false;
        }
        if (transform.position.y < -2.7f)
        {
            facingUp = true;
        }
        if (facingUp)
        {
            //Tells the platform to start moving in this direction.
            transform.position = new Vector2(transform.position.x, transform.position.y + speed * Time.deltaTime);
        }
        else
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - speed * Time.deltaTime);
        }
    }
}
