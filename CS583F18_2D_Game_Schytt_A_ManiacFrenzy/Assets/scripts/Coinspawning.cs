using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The class responsible for spawning coins at random locations (or at least random among given 17 locations).
public class Coinspawning : MonoBehaviour {
    public Transform[] spawnPoints;
    public GameObject coin;
    int randomSpawnPoint;
    public static bool spawnAllowed;
	void Start () {
        spawnAllowed = true;
        InvokeRepeating("SpawnCoin", 0f, 7f);
	}
	
    //This piece of code were found on Alexander Zotovs tutorial, see sources cited - spawning objects.
    //It picks a transform at random, and assigns a coin to its location while the boolean spawnAllowed is true. This
    //variable is turned off after a player has won the game.
	void SpawnCoin()
    {
        if (spawnAllowed)
        {
            randomSpawnPoint = Random.Range(0, spawnPoints.Length);
            Instantiate(coin, spawnPoints[randomSpawnPoint].position, Quaternion.identity);
        }
    }
    public void GameStopped()
    {
        spawnAllowed = false;
    }
}
