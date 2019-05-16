using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class is responsible for the pause menu, a player can press escape to bring it up.
public class PauseUI : MonoBehaviour {

    public GameObject Pause;
    private bool paused = false;
	void Start () {
        Pause.SetActive(false);
	}
    //Checks for the pause key which is escape. inspired by GucioDevs.
    void Update () {
        if (Input.GetButtonDown("Pause"))
        {
            paused = !paused;
        }
        if (paused)
        {
            Pause.SetActive(true);
            //Freezes the game if pause bool is active.
            Time.timeScale = 0f;
        }
        if (!paused)
        {
            Pause.SetActive(false);
           Time.timeScale =1f;
        }
    }
    public void Resume()
    {
        paused = false;
    }
    //The reload-game button.
    public void Restart()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
