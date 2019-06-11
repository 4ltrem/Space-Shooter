using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyShipPrefab;
    [SerializeField]
    private GameObject[] powerups;
    private GameManager _gameManager;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void startSpawning()
    {
        _gameManager.gameOver = false;
        StartCoroutine(spawnEnemyRoutine());
        StartCoroutine(PowerupSpawnRoutine());
    }

    //create a coroutine to spawn the enemy every 5 seconds
    IEnumerator spawnEnemyRoutine()
    {
        while (_gameManager.gameOver == false)
        {
            Instantiate(enemyShipPrefab, new Vector3(Random.Range(-8.0f, 8.0f), 6.3f, 0), Quaternion.identity);
            yield return new WaitForSeconds(5.0f);
        } 
    }

    IEnumerator PowerupSpawnRoutine()
    {
        while (_gameManager.gameOver == false)
        {
            int randomPowerup = Random.Range(0, 3 );
            Instantiate(powerups[randomPowerup], new Vector3(Random.Range(-8.0f, 8.0f), 6.3f, 0), Quaternion.identity);
            yield return new WaitForSeconds(5.0f);
        }
    }
}
