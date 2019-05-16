using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class  solely exists for response time on the sound effect that gives the punch.
//constantly checks for a boolean that is activated during an animation, then plays 
public class sounds : MonoBehaviour {

    public AudioSource punchsound;
    public bool activatePunch;
	void Start () {
       
	}
	
//Checks for the boolean to see if its the right time to activate the sound effect.
	void Update () {
		if (activatePunch)
        {
            punchsound.Play();
        }
	}
}
