using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Howtoplay : MonoBehaviour {

    bool play = false;
    bool paused = false;
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
        if (startTime-time > 1.35f && !paused)
        {
           buttonsound.Pause();
            paused = true;
        }
        //Space, enter, or return may be pressed to transision.
        if (Input.GetButtonDown("Submit") || Input.GetButtonDown("Space") || play)
        {
            paused = true;
            buttonsound.UnPause();
            starter.GetComponent<Animator>().SetBool("play", true);
            
            play = false;
            StartCoroutine(Loadnextscene());
            
        }
    }
    //Loads the next scene, this piece of code was inspired by theblackthornprod.
    IEnumerator Loadnextscene()
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("howtoplaycontinued");
    }
    public void Playgame()
    {
        play = true;
    }
    

}
