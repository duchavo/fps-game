using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* Controls the overall game logics. It spawns enemies until it has spawned a total of 10.
 * It will not spawn more than 3 enemies at a time.
 * When an enemy dies, it calls the killEnemy method, decreasing the totalEnemies and enemyCounter
 * counters.
 * When gameOver is called, it shows a panel either saying the player won or lost. Input is also
 * disabled. */


public class SceneControls : MonoBehaviour {

    public float sceneWidth = 50f;
    public float sceneHeight = 50f;

    public GameObject enemy1;
    public GameObject enemy2;

    public int totalEnemies = 10;
    public int maxEnemies = 3;


    float enemyCounter = 0;
    bool enemyBool = true;

    public GameObject gameOverPanel;
    public GameObject winPanel;

    bool inputEnabled = true;


    // Use this for initialization
    void Start () {

        gameOverPanel.SetActive(false);
        winPanel.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {

        if(totalEnemies <= 0)
        {
            gameOver();
        }
        else if(enemyCounter < maxEnemies && totalEnemies >= maxEnemies )
        {
            spawnEnemy();
        }
		
	}

    void spawnEnemy()
    {
        GameObject enemy;
        if (enemyBool)
        {
            enemy = enemy1;
        }
        else
        {
            enemy = enemy2;
        }
        enemyBool = !enemyBool;
        Instantiate(enemy, randomLocation(), Quaternion.identity);
        enemyCounter++;
    }

    public void killEnemy()
    {
        enemyCounter--;
        totalEnemies--;
    }

    Vector3 randomLocation()
    {
        return new Vector3(
            Random.Range(-sceneWidth/2, sceneWidth/2),
            -4.6f,
            Random.Range(-sceneHeight / 2, sceneHeight / 2)
            );
    }

    public void gameOver()
    {
        inputEnabled = false;
        if(totalEnemies > 0)
        {
            gameOverPanel.SetActive(true);
        }
        else
        {
            winPanel.SetActive(true);
        }
    }

    public void reset()
    {
        GameObject[] allObjects = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject obj in allObjects)
        {
            Destroy(obj);
        }

        enemyCounter = 0;
        enemyBool = true;
        inputEnabled = true;
        gameOverPanel.SetActive(false);
        winPanel.SetActive(false);
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.transform.SetPositionAndRotation(new Vector3(0, 1.1f, 0), Quaternion.identity);
        PlayerCharacter pc = ((PlayerCharacter)player.GetComponent<PlayerCharacter>());
        pc.reset();
    }

    public bool inputIsEnabled()
    {
        return inputEnabled;
    }

}
