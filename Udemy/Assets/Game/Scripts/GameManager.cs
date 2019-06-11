using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool gamePaused = false;
    public bool coopMode = false;
    public bool gameOver = true;
    public bool playerOneDead = true;
    public bool playerTwoDead = true;
    public GameObject player;
    public GameObject[] coopPlayers;
    private UIManager _uiManager;
    private SpawnManager _spawnManager;
    private Animator _pauseAnimator;
    
    private void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _pauseAnimator = GameObject.Find("Pause_Menu_Panel").GetComponent<Animator>();
        _pauseAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;
    }

    private void Update()
    {
        //if game over is true
        if (gameOver == true)
        { 
            //if space key pressed
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                if (coopMode == false)
                {
                    //spawn the player
                    Instantiate(player, Vector3.zero, Quaternion.identity);
                }
                else
                {
                    if (playerOneDead == true)
                    {
                        Instantiate(coopPlayers[0], Vector3.zero, Quaternion.identity);
                    }
                    if (playerTwoDead == true)
                    {
                        Instantiate(coopPlayers[1], Vector3.zero, Quaternion.identity);
                    }
                }
                _spawnManager.startSpawning(); 
                //hide title screen
                _uiManager.HideTitleScreen();
                //gameOver is false
                gameOver = false;
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene("Main_Menu");
            }
        }

        //checks if p is pressed
        if (Input.GetKeyDown(KeyCode.P))
        {
            Pause();
        }
    }

    //Functions for the pause Menu
    public void Pause()
    {
        Time.timeScale = 0f;
        _uiManager.pauseMenu.SetActive(true);
        gamePaused = true;
        _pauseAnimator.SetBool("isPaused", true);
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        _uiManager.pauseMenu.SetActive(false);
        gamePaused = false;
    }

    public void BackToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main_Menu");
    } 
}
