using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    public bool canTripleShot = false;
    public bool SpeedBoostActive = false;
    public bool shieldActive = false;
    public int lives = 3;
    public bool isPlayerOne = false;
    public bool isPlayerTwo = false;

    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private GameObject _explosionPrefab;
    [SerializeField]
    private GameObject _shieldsGameObject;
    [SerializeField]
    private GameObject[] _engines;

    [SerializeField]
    private float _fireRate = 0.25f;
   
    private float _canFire = 0.0f;

    [SerializeField]
    private float _speed = 5.0f;

    private UIManager _uiManager;
    private GameManager _gameManager;

    private AudioSource _audiosource;

    private int hitCount = 0;

    void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        if (_gameManager.coopMode == false)
        {
            //current pos = new position
            transform.position = new Vector3(0, 0, 0);
        }

        if (_uiManager != null)
        {
            _uiManager.UpdateLives(lives);
        }

        _audiosource = GetComponent<AudioSource>();

        hitCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //if player 1
        if (isPlayerOne == true)
        {
            Movement();

            #if UNITY_ANDROID
            if ((Input.GetKeyDown(KeyCode.Space) || CrossPlatformInputManager.GetButtonDown("Fire")) && isPlayerOne == true)
            {
                if (_gameManager.gamePaused == false)
                {
                    Shoot();
                }
            }
            #else
            if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && isPlayerOne == true)
            {
                if (_gameManager.gamePaused == false)
                {
                    Shoot();
                }
            }
            #endif
        }

        //if player 2
        if (isPlayerTwo == true)
        {
            Movement2();
            if (Input.GetKeyDown(KeyCode.KeypadEnter) && isPlayerTwo == true)
            {
                Shoot();
            }
        }
    }

    private void Shoot()
    {
        if (Time.time > _canFire)
        {
            _audiosource.Play();
            if (canTripleShot == true)
            {
                Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
            }
            else
            {
                //spawn my laser
                Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.88f, 0), Quaternion.identity);
            }
            _canFire = Time.time + _fireRate;
        }
    }

    private void Movement()
    {
        float horizontalInput = CrossPlatformInputManager.GetAxis("Horizontal"); //Input.GetAxis("Horizontal");
        float verticalInput = CrossPlatformInputManager.GetAxis("Vertical"); //Input.GetAxis("Vertical");

        if (SpeedBoostActive == true)
        { 
            transform.Translate(Vector3.right * _speed * 1.5f * horizontalInput * Time.deltaTime);
            transform.Translate(Vector3.up * _speed * verticalInput * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.right * _speed * horizontalInput * Time.deltaTime);
            transform.Translate(Vector3.up * _speed * verticalInput * Time.deltaTime);
        }

        //if player on the y is greater than 0
        //set player position on the y to 0

        if (transform.position.y > 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y < -4)
        {
            transform.position = new Vector3(transform.position.x, -4, 0);
        }

        //if the player on the y is greater than  9
        //set player position to -9 and vice versa

        if (transform.position.x > 9)
        {
            transform.position = new Vector3(-9, transform.position.y, 0);
        }
        else if (transform.position.x < -9)
        {
            transform.position = new Vector3(9, transform.position.y, 0);
        }
    }

    private void Movement2()
    {
        float horizontalInput2 = Input.GetAxis("Horizontal2");
        float verticalInput2 = Input.GetAxis("Vertical2"); 

        if (SpeedBoostActive == true)
        {
            transform.Translate(Vector3.right * _speed * 1.5f * horizontalInput2 * Time.deltaTime);
            transform.Translate(Vector3.up * _speed * verticalInput2 * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.right * _speed * horizontalInput2 * Time.deltaTime);
            transform.Translate(Vector3.up * _speed * verticalInput2 * Time.deltaTime);
        }

        //if player on the y is greater than 0
        //set player position on the y to 0

        if (transform.position.y > 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y < -4)
        {
            transform.position = new Vector3(transform.position.x, -4, 0);
        }

        //if the player on the y is greater than  9
        //set player position to -9 and vice versa

        if (transform.position.x > 9)
        {
            transform.position = new Vector3(-9, transform.position.y, 0);
        }
        else if (transform.position.x < -9)
        {
            transform.position = new Vector3(9, transform.position.y, 0);
        }
    }

    public void Damage()
    {
        //subtract 1 life from the player 
        
        if (shieldActive == true)
        {
            shieldActive = false;
            _shieldsGameObject.SetActive(false);
            return;
        }

        int engineChoice = Random.Range(0, 2);

        hitCount++;

        if (hitCount == 1)
        {
            //turn on engine of the array w/ number generated 
            _engines[engineChoice].SetActive(true);

        }
        else if (hitCount == 2)
        {
            if (engineChoice == 1)
            {
                engineChoice = 0;
            }
            else if (engineChoice == 0)
            {
                engineChoice = 1;
            }
            //turn on opposite engine
            _engines[engineChoice].SetActive(true);
        }
        lives--;
        _uiManager.UpdateLives(lives);
        //if lives < 1 (meaning 0)
        if (lives < 1)
        {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            _gameManager.gameOver = true;
            if (isPlayerOne == true)
            {
                _gameManager.playerOneDead = true;
            }
            else if (isPlayerTwo == true)
            {
                _gameManager.playerTwoDead = true;
            }
            _uiManager.ShowTitleScreen();
            Destroy(this.gameObject);
        }  
    }

    //Enables Triple Shot Powerup
    public void TripleShotPowerupOn()
    {
        canTripleShot = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    //Enables Speed Powerup
    public void SpeedPowerupOn()
    {
        SpeedBoostActive = true;
        StartCoroutine(SpeedPowerDownRoutine());
    }

    //Enables Shield Powerup
    public void ShieldPowerupOn()
    {
        shieldActive = true;
        _shieldsGameObject.SetActive(true);
    }

    //=== Powerdown Routines for disabling Powerups ===//

    public IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        canTripleShot = false;
    }

    public IEnumerator SpeedPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        SpeedBoostActive = false;
    }
}


