using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{ 
    private Vector3 _laserOffset = new Vector3(0, 1.05f , 0);
    [SerializeField] private GameObject _shieldVisualizer;
    [SerializeField] private GameObject[] _engineFireVisualizers;
    [SerializeField] private GameObject _laserPrefab;
    [SerializeField] private GameObject _tripleLaserPrefab;
    [SerializeField] private GameObject _laserBeamPrefab;
    [SerializeField] private int _score = 0;
    [SerializeField] private float _speed = 5.0f;
    public int _gold = 0;
    private float _speedMultiplier = 2.0f;
    private float _horizontalBound = 11.3f;
    private float _topBound = 5;
    private float _downBound = -3;
    private float _canFire1 = 0.0f;
    private float _canFire2 = 0.0f;
    public int _lives = 3;
    public int _numOfLives = 3;
    public int _shields = 0;
    public int _numOfShields = 1;
    public float _laserFireRate = 0.15f;
    public float _powerupTime = 5.0f;
    public float _playerLaserShotDamage = 10;
    public bool isPlayerOne;
    public bool isPlayerTwo;
    private bool _isTripleLaserActive = false;
    private bool _isSpeedActive = false;
    private bool _isInvincible = false;
    [SerializeField]
    private bool isLaserBeamActive = false;

    private GameManager _gameManager;
    private SpawnManager _spawnManager;
    private UIManager _uiManager;
    private UpgradeManager _upgradeManager;
    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        if(_gameManager == null)
        {
            Debug.LogError("The Game Manager on Player is <null>");
        }
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        if(_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager on Player is <null>");
        }
        _uiManager =  GameObject.Find("Canvas").GetComponentInParent<UIManager>();
        if(_uiManager == null)
        {
            Debug.LogError("The UI Manager on Player is <null>");
        }
        _upgradeManager = GameObject.Find("Upgrade_Manager").GetComponent<UpgradeManager>();
        if(_upgradeManager == null)
        {
            Debug.LogError("The Upgrade Manager on Player is <null>");
        }
        _animator = GetComponent<Animator>();
        if(_animator == null)
        {
            Debug.LogError("The Animator on Player is <null>");
        } 
    }

    // Update is called once per frame
    void Update()
    {
        if(isPlayerOne == true)
        {
            MovementPlayerOne();
            if((Input.GetKeyDown(KeyCode.Space) || Input.GetKey(KeyCode.Space)) && Time.time > _canFire1)
            {
                ShootPlayerOne();
            }
            if(Input.GetKeyDown(KeyCode.E) && !isLaserBeamActive)
            {
                _laserBeamPrefab.SetActive(true);
                isLaserBeamActive = true;                
            }
            else if(Input.GetKeyDown(KeyCode.E) && isLaserBeamActive)
            {
                _laserBeamPrefab.SetActive(false);
                isLaserBeamActive = false;
            }
        }
        if(isPlayerTwo == true)
        {
            MovementPlayerTwo();
            if((Input.GetKeyDown(KeyCode.RightControl) || Input.GetKey(KeyCode.RightControl)) && Time.time > _canFire2)
            {
                ShootPlayerTwo();
            }     
        }         
    }

    void MovementPlayerOne()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(direction * _speed * Time.deltaTime);

        if(transform.position.x <= -_horizontalBound)
        {
            transform.position = new Vector3(-_horizontalBound, transform.position.y, 0);
        }
        else if(transform.position.x >= _horizontalBound)
        {
            transform.position = new Vector3(_horizontalBound, transform.position.y, 0);
        }  
        if (transform.position.y <= _downBound)
        {
            transform.position = new Vector3(transform.position.x, _downBound, 0);
        }
        else if(transform.position.y >= _topBound)
        {
            transform.position = new Vector3(transform.position.x, _topBound, 0);
        }
    }

    void MovementPlayerTwo()
    {
        if(Input.GetKey(KeyCode.Keypad8))
        {
            transform.Translate(Vector3.up * _speed * Time.deltaTime);
        }
        if(Input.GetKey(KeyCode.Keypad5))
        {
            transform.Translate(Vector3.down * _speed * Time.deltaTime);
        }
        if(Input.GetKey(KeyCode.Keypad6))
        {
            transform.Translate(Vector3.right * _speed * Time.deltaTime);
        }
        if(Input.GetKey(KeyCode.Keypad4))
        {
            transform.Translate(Vector3.left * _speed * Time.deltaTime);
        }
    
        if(transform.position.x <= -_horizontalBound)
        {
            transform.position = new Vector3(-_horizontalBound, transform.position.y, 0);
        }
        else if(transform.position.x >= _horizontalBound)
        {
            transform.position = new Vector3(_horizontalBound, transform.position.y, 0);
        }  
        if (transform.position.y <= _downBound)
        {
            transform.position = new Vector3(transform.position.x, _downBound, 0);
        }
        else if(transform.position.y >= _topBound)
        {
            transform.position = new Vector3(transform.position.x, _topBound, 0);
        }
    }
    
        void ShootPlayerOne()
        {
            _canFire1 = Time.time + _laserFireRate;
            if(_isTripleLaserActive == false)
            {
                GameObject player1Laser = Instantiate(_laserPrefab, transform.position, Quaternion.identity);
                Laser[] lasers1 = player1Laser.GetComponentsInChildren<Laser>();
                for(int i=0; i<lasers1.Length; i++)
                {
                    lasers1[i].AssignPlayer1Laser();
                }
                AudioManager.instance.Play("Laser_Shot_Sound");

            }
            else if(_isTripleLaserActive == true)
            {
                GameObject player1Laser = Instantiate(_tripleLaserPrefab, transform.position, Quaternion.identity);
                Laser[] lasers1 = player1Laser.GetComponentsInChildren<Laser>();
                for(int i=0; i<lasers1.Length; i++)
                {
                    lasers1[i].AssignPlayer1Laser();
                }
                AudioManager.instance.Play("Laser_Shot_Sound");            
            }
        }

        void ShootPlayerTwo()
        {
            _canFire2 = Time.time + _laserFireRate;
            if(_isTripleLaserActive == false)
            {
                GameObject player2Laser = Instantiate(_laserPrefab, transform.position, Quaternion.identity);
                Laser[] lasers2 = player2Laser.GetComponentsInChildren<Laser>();
                for(int i=0; i<lasers2.Length; i++)
                {
                    lasers2[i].AssignPlayer2Laser();
                }
                AudioManager.instance.Play("Laser_Shot_Sound");
            }
                else if(_isTripleLaserActive == true)
            {
                GameObject player2Laser = Instantiate(_tripleLaserPrefab, transform.position, Quaternion.identity);
                Laser[] lasers2 = player2Laser.GetComponentsInChildren<Laser>();
                for(int i=0; i<lasers2.Length; i++)
                {
                    lasers2[i].AssignPlayer2Laser();
                }
                AudioManager.instance.Play("Laser_Shot_Sound");
            }
        }

    public void Damage()
    {
        /*if(_isShieldActive == true)
        {
            _isShieldActive = false;
            _shieldVisualizer.SetActive(false);
            Invincible();
            return;
        }*/

        Shake.instance.Shaking();

        if(_isInvincible == false)
        {
            if(_shields == 1)
            {
                _shields--;
                _shieldVisualizer.SetActive(false);
                AudioManager.instance.Play("Shield_Hit_Sound");
                Invincible();
                return;
            }
            else if(_shields > 1)
            {
                _shields--;
                AudioManager.instance.Play("Shield_Hit_Sound");
                Invincible();
                return;
            }
        }

        if(_isInvincible == false)
        {
            _lives--;
            AudioManager.instance.Play("Player_Hit_Sound");
            Invincible();
        }
        ChangeVisualiser();

        /*if(_lives < 3)
        {
            _engineFireVisualizers[0].SetActive(true);
        }
        else if(_lives < 2)
        {
            _engineFireVisualizers[1].SetActive(true);
        }*/

        if(isPlayerOne == true)
        {
            _uiManager.UpdateLivesP1(_lives, _numOfLives);
            if(_lives < 1)
            {
                _spawnManager.OnPlayerDead();
                Destroy(this.gameObject);
            }
        }
        else if(isPlayerTwo == true)
        {
            _uiManager.UpdateLivesP2(_lives, _numOfLives);
            if(_lives < 1)
            {
                _spawnManager.OnPlayerDead();
                Destroy(this.gameObject);
            }
        }
    }

    public void Invincible()
    {
        _isInvincible = true;
        StartCoroutine(VulnurableDownRoutine());
    }
    IEnumerator VulnurableDownRoutine()
    {
        yield return new WaitForSeconds(0.2f);
        _isInvincible = false;
    }


    public void TripleLaserActive()
    {
        _isTripleLaserActive = true;
        StartCoroutine(TripleLaserPowerDownRoutine());
    }
    IEnumerator TripleLaserPowerDownRoutine()
    {
        yield return new WaitForSeconds(_powerupTime);
        _isTripleLaserActive = false;
    }

    public void SpeedActive()
    {
        _isSpeedActive = true;
        if(_speed <=5 )
        {
        _speed *= _speedMultiplier;
        }
        StartCoroutine(SpeedPowerDownRoutine());
    }
    IEnumerator SpeedPowerDownRoutine()
    {
        yield return new WaitForSeconds(_powerupTime);
        _isSpeedActive = false;
        if(_speed >= 10)
        {
        _speed /= _speedMultiplier;
        }
    }

    public void ShieldActive()
    {

        _shields = _numOfShields;
        _shieldVisualizer.SetActive(true);
    }

    public void AddScoreP1(int points)
    {
        _score += points;
        _uiManager.UpdateScoreP1(_score);
    }
    public void AddScoreP2(int points)
    {
        _score += points;
        _uiManager.UpdateScoreP2(_score);
    }

    /*public void AddCoinP1(int coins)
    {
        _gold += coins;
        _uiManager.UpdateGoldP1(_gold);
    }
    public void AddCoinP2(int coins)
    {
        _gold += coins;
        _uiManager.UpdateGoldP2(_gold);
    }*/

    public void AddCoin(int coin)
    {
        _gold += coin;
        if(isPlayerOne == true)
        {
            _uiManager.UpdateGoldP1(_gold);
        }
        if(isPlayerTwo == true)
        {
            _uiManager.UpdateGoldP2(_gold);
        }
    }

    /*public void AddLifeP1(int life)
    {
        if(_lives < _numOfLives)
        {
           _lives += life;
           _uiManager.UpdateLivesP1(_lives, _numOfLives);
            if(_lives > 1)
            {
                _engineFireVisualizers[1].SetActive(false);
            }
            if(_lives > 2)
            {
                _engineFireVisualizers[0].SetActive(false);
            }           
        }
    }
    public void AddLifeP2(int life)
    {
        if(_lives < _numOfLives)
        {
           _lives += life;
           _uiManager.UpdateLivesP2(_lives, _numOfLives);
        }
    }*/

    public void AddLife(int life)
    {
        if(_lives < _numOfLives)
        {
            _lives += life;
            if(isPlayerOne == true)
            {
                _uiManager.UpdateLivesP1(_lives, _numOfLives);
            }
            if(isPlayerTwo == true)
            {
                _uiManager.UpdateLivesP2(_lives, _numOfLives);
            }
            ChangeVisualiser();
            /*if(_lives > 1)
            {
                _engineFireVisualizers[1].SetActive(false);
            }
            if(_lives > 2)
            {
                _engineFireVisualizers[0].SetActive(false);
            }*/
        }
    }

    public void ChangeVisualiser()
    {
        if(_lives == 1)
        {
            _engineFireVisualizers[0].SetActive(true);
            _engineFireVisualizers[1].SetActive(true);
        }
        else if(_lives == 2)
        {
            _engineFireVisualizers[0].SetActive(false);
            _engineFireVisualizers[1].SetActive(true);
        }
        else
        {
            _engineFireVisualizers[0].SetActive(false);
            _engineFireVisualizers[1].SetActive(false);
        }
    }
}