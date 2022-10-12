using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private float _rotateSpeed = 15.0f;
    [SerializeField] private GameObject _explosionPrefab;
    private SpawnManager _spawnManager;
    private bool _getHit = false;
    // Start is called before the first frame update
    void Start()
    {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        if(_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager on Asteroid is NULL.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * _rotateSpeed * Time.deltaTime);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Laser" && _getHit == false)
        {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            AudioManager.instance.Play("Explosion_Sound");
            Destroy(other.gameObject);
            _getHit = true;
            _spawnManager.StartSpawning();
            Destroy(this.gameObject, 0.25f);
        }
    }

}
