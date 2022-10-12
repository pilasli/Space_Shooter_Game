using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private float _speed = 8;
    private int _topLimit = 8;
    private bool _isEnemyLaser = false;
    private bool _isPlayer1Laser = false;
    private bool _isPlayer2Laser = false;

    private GameManager _gameManager;
    private UpgradeManager _upgradeManager;
    private SpawnManager _spawnManager;
    private Enemy _enemy;
    private Player _player1;
    private Player _player2;

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        _upgradeManager = GameObject.Find("Upgrade_Manager").GetComponent<UpgradeManager>();
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();

        if(_gameManager == null)
        {
            Debug.LogError("The Game Manager on Laser is NULL.");
        }

        if(_upgradeManager == null)
        {
            Debug.LogError("The Upgrade Manager on Player is NULL.");
        }

        if(_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager on Player is NULL.");
        }        
    }

    // Update is called once per frame
    void Update()
    {
        if(_isEnemyLaser == false)
        {
            MoveUp();
        }
        else
        {
            MoveDown();
        }
    }

    void MoveUp()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        if(transform.position.y > _topLimit)
        {
            if(transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
    }
    
    void MoveDown()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if(transform.position.y < -_topLimit)
        {
            if(transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }        
    }

    public void AssignEnemyLaser()
    {
        _isEnemyLaser = true;
    }
    public void AssignPlayer1Laser()
    {
        _isPlayer1Laser = true;
    }
    public void AssignPlayer2Laser()
    {
        _isPlayer2Laser = true;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(_gameManager._isCoOpMode == false && _spawnManager._stopSpawning == false)
        {
            _player1 = GameObject.Find("Player_1").GetComponent<Player>();
            if(_player1 == null)
            {
                Debug.LogError("Player1 on Laser is NULL!");
            }
        }
        else if(_gameManager._isCoOpMode == true && _spawnManager._stopSpawning == false)
        {
            _player1 = GameObject.Find("Player_1").GetComponent<Player>();
            if(_player1 == null)
            {
                Debug.LogError("Player1 on Laser is NULL!");
            }
            _player2 = GameObject.Find("Player_2").GetComponent<Player>();
            if(_player2 == null)
            {
                Debug.LogError("Player2 on Laser is NULL!");
            }
        }

        if(other.tag == "Player1")
        {
            if(_player1 != null && _isEnemyLaser == true)
            {
                _player1.Damage();               
                Destroy(this.gameObject);
            }
        }
        if(other.tag == "Player2")
        {
            if(_player2 != null && _isEnemyLaser == true)
            {
                _player2.Damage();
                Destroy(this.gameObject);
            }
        }
        if(other.tag == "Enemy")
        {
            Enemy _enemy = other.transform.GetComponent<Enemy>();
            if(_isPlayer1Laser == true && _isEnemyLaser == false)
            {
                _player1.AddScoreP1(10);
                _enemy.DamageEnemy(_player1._playerLaserShotDamage);
                Destroy(this.gameObject);
            }
            else if(_isPlayer2Laser == true && _isEnemyLaser == false)
            {
                _player2.AddScoreP2(10);
                _enemy.DamageEnemy(_player2._playerLaserShotDamage);
                Destroy(this.gameObject);
            }
        }
    }
}
