using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    private float _canDamage = 0.0f;
    private float _laserBeamRate = 0.1f;
    private int _playerLaserBeamDPS = 10;
    [SerializeField] private bool _isPlayer1LaserBeam;
    [SerializeField] private bool _isPlayer2LaserBeam;

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {
            Enemy _enemy = other.transform.GetComponent<Enemy>();
            if(_isPlayer1LaserBeam == true && Time.time > _canDamage)
            {
                _canDamage = Time.time + _laserBeamRate;
                _enemy.DamageEnemy(_playerLaserBeamDPS);
            }

        }
    }
}
