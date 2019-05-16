using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//The simpleton class that keeps track of scores between the players.
public class GameManager : MonoBehaviour {
    public GameObject fighter1;
    public GameObject fighter2;
    public GameObject p1wins;
    public GameObject p2wins;
    public GameObject[] p1coins;
    public GameObject[] p2coins;
    public GameObject gameSound;
    public GameObject spawnMachine;
    public AudioSource applause;
    public AudioSource game;
    public bool gameover = false;
    public int p1Life;
    public int p2Life;

    
  //Sets both players coins to 0 at start of the game.
    void Start () {
        p1Life = 0;
        p2Life = 0;
	}
	

	void Update () {
        //If any of the players reaches 5 coins, they win.
        //Stop the game, apply sounds,and set the other player to be inactive.
        if (p1Life >=5)
        {
            if (!gameover)
            {
                game.Play();
                applause.Play();
            }
            gameover = true;
            p1wins.SetActive(true);
            fighter2.SetActive(false);
            gameSound.GetComponent<AudioSource>().Pause();
            spawnMachine.GetComponent<Coinspawning>().GameStopped();
        }
        if (p2Life >= 5)
        {
            if (!gameover)
            {
                game.Play();
                applause.Play();
            }
            gameover = true;
            p2wins.SetActive(true);
            fighter1.SetActive(false);
            gameSound.GetComponent<AudioSource>().Pause();
            spawnMachine.GetComponent<Coinspawning>().GameStopped();
        }
    }
    //This method is called from external when player 1 is supposed to recieve an increase in score.
    //That is, he collected a coin.
    public void p1Gain()
    {
        p1Life += 1;
        //For loop that takes the array of coins under "player" sprite, and sets active or inactive depending on score set.
        //credits to gamesplusjames.
        for (int i = 0; i < p1coins.Length; i++)
        {
            if (p1Life <= i)
            {
                p1coins[i].SetActive(false);
            }
            else
            {
                p1coins[i].SetActive(true);
            }
        }
    }
    //Same as above but for player 2.
    public void p2Gain()
    {
        p2Life += 1;
        for (int i = 0; i < p2coins.Length; i++)
        {
            if (p2Life <= i)
            {
                p2coins[i].SetActive(false);
            }
            else
            {
                p2coins[i].SetActive(true);
            }
        }
    }
    //Similiarly P1Gain, but decreases the score.
    //This function is called externally when player 1 is being hit by player 2. Credits to gamesplusjames.
    public void p1hurt()
    {
        if (p1Life > 0)
        {
            p1Life -= 1;
            for (int i = 0; i < p1coins.Length; i++)
            {
                if (p1Life > i)
                {
                    p1coins[i].SetActive(true);
                }
                else
                {
                    p1coins[i].SetActive(false);
                }
            }
        }
    }
    //Same as method above, but for player 2.
    public void p2hurt()
    {
        if (p2Life > 0)
        {
            p2Life -= 1;
            for (int i = 0; i < p2coins.Length; i++)
            {
                if (p2Life > i)
                {
                    p2coins[i].SetActive(true);
                }
                else
                {
                    p2coins[i].SetActive(false);
                }
            }
        }
    }
}
