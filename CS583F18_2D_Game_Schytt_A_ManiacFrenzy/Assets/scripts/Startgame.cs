using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Startgame : MonoBehaviour {

    bool play = false;
    bool paused = false;
    bool credits = false;
    private Animator anim;
    public GameObject starter;
    private AudioSource buttonsound;
    float time = 1.5f;
    float startTime=1.5f;
    void Start()
    {
        buttonsound = GetComponent<AudioSource>();
        buttonsound.Play();
    }

    void Update()
    {
        //This counter allows for the button sound to show up correctly and not too late.
        time -= Time.deltaTime;
        if (startTime-time > 1.25f && !paused)
        {
           buttonsound.Pause();
            paused = true;
        }
        //C Key is credits key.
        if (Input.GetButtonDown("Credits"))
            credits = true;
        //Space, enter, or return may be pressed to transision.
        if (Input.GetButtonDown("Submit") || Input.GetButtonDown("Space"))
            play = true;
        if (play || credits)
        {
            paused = true;
            buttonsound.UnPause();
            if (credits)
                starter.GetComponent<Animator>().SetBool("toCred", true);
            else
                starter.GetComponent<Animator>().SetBool("play", true);
            
                
            
            play = false;
            StartCoroutine(Loadnextscene(credits));
            
        }
    }
    //Loads the next scene, this piece of code was inspired by theblackthornprod.
    IEnumerator Loadnextscene(bool cred)
    {
        yield return new WaitForSeconds(1.0f);
        if(!cred)
            SceneManager.LoadScene("howtoplay");
        else
            SceneManager.LoadScene("credits");
    }
    public void Playgame()
    {
        play = true;
    }
    //sets the credits bool to true and signals whichs scene to go to.
    public void gotoCredits()
    {
        credits = true;
        play = true;
    }
    

}
