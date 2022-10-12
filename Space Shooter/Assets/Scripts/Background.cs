using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4f;
    private Vector3 _startPosition;
    private float _repeatWidth;
    void Start()
    {
        _startPosition = transform.position;
        _repeatWidth = GetComponent<BoxCollider2D>().size.y / 2;
    }

    void Update()
    {
        transform.Translate(translation:Vector3.down * _speed * Time.deltaTime);
        if(transform.position.y < _startPosition.x - _repeatWidth)
        {
            transform.position = _startPosition;
        }
    }
}
