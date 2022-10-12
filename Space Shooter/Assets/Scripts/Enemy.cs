using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _laserPrefab;
    [SerializeField] private GameObject _coinPrefab;
    [SerializeField] private float _speed = 1.5f;   
    [SerializeField] private SimpleFlash _flashEffect;
    private float _spawnPosY = 8;
    private float _fireRate = 3.0f;
    private float _canFire = -1;
    private float _enemyHealth = 30;
    private int _coinDropChance = 40;

    private Player _player1;
    private Player _player2;
    private Animator _anim;
    private GameObject _laser;
    private GameManager _gameManager;
    private UpgradeManager _upgradeManager;
    private SpawnManager _spawnManager;
 
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        if(_gameManager == null)
        {
            Debug.LogError("GameManager on Enemy is <null>");
        }
        _upgradeManager = GameObject.Find("Upgrade_Manager").GetComponent<UpgradeManager>();
        if(_upgradeManager == null)
        {
            Debug.LogError("UpgradeManager on Enemy is <null>");
        }  

        _anim = GetComponent<Animator>();      
        if(_anim == null)
        {
            Debug.LogError("Animator on Enemy is <null>");
        }
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        if(_spawnManager == null)
        {
            Debug.LogError("SpawnManager on Enemy is <null>");
        }

        if(_gameManager._isCoOpMode == false)
        {
            _player1 = GameObject.Find("Player_1").GetComponent<Player>();
            if(_player1 == null)
            {
            Debug.LogError("The Player1 on Enemy is <null>");
            }
        }
        else if(_gameManager._isCoOpMode == true)
        {
            _player1 = GameObject.Find("Player_1").GetComponent<Player>();
            _player2 = GameObject.Find("Player_2").GetComponent<Player>(); 
            if(_player1 == null)
            {
                Debug.LogError("The Player1 on Enemy is <null>");
            }
            if(_player2 == null)
            {
                Debug.LogError("The Player2 on Enemy is <null>");
            }          
        }
        //_audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        if(Time.time > _canFire && _speed != 0)
        {
            _fireRate = Random.Range(3f, 7f);
            _canFire = Time.time + _fireRate;
            GameObject enemyLaser = Instantiate(_laserPrefab, transform.position, Quaternion.identity);
            Laser[] lasers = enemyLaser.GetComponentsInChildren<Laser>();
            for(int i=0; i<lasers.Length; i++)
            {
                lasers[i].AssignEnemyLaser();
            }
        }
    }

    void CalculateMovement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if(transform.position.y < -6f)
        {
            float _spawnPosX = Random.Range(-8f,8f);
            transform.position = new Vector3(_spawnPosX, _spawnPosY, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player1")
        {           
            if(_player1 != null && _speed !=0)
            {
               _player1.Damage();
            }
            DestroyEnemy();
        }
        if(other.tag == "Player2")
        {  
            if(_player2 != null && _speed !=0)
            {
                _player2.Damage();
            }
            DestroyEnemy();
        }
    }

    public void DamageEnemy(float playerDamage)
    {
        _flashEffect.Flash(Color.white);
        _enemyHealth -= playerDamage;
        AudioManager.instance.Play("Player_Hit_Sound");
        if(_enemyHealth <= 0)
        {
            DestroyEnemy();
        }      
    }

    public void DestroyEnemy()
    {
        _anim.SetTrigger("OnEnemyDeath");
        _speed = 0;
        AudioManager.instance.Play("Explosion_Sound");
        _spawnManager.enemyCounter--;
        int _coinDropNum = Random.Range(1, 101);
        //_coinDropNum = Mathf.RoundToInt(_coinDropNum);
        if(_coinDropNum <= _coinDropChance)
        {
            StartCoroutine(CoinCreateRoutine());
        }
        Destroy(GetComponent<Collider2D>());
        Destroy(this.gameObject, 2.38f);        
    }

    IEnumerator CoinCreateRoutine()
    {
        yield return new WaitForSeconds(1f);
        Instantiate(_coinPrefab, transform.position, Quaternion.identity);
    }
}
