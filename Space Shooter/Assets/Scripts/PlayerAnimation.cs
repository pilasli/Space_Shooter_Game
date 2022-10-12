using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _animator;
    private Player _player;
    
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _player = GetComponent<Player>();

        if(_animator == null)
        {
            Debug.LogError("The Animator on PlayerAnimation is NULL.");
        }
        if(_player == null)
        {
            Debug.LogError("The Player on PlayerAnimation is NULL.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(_player.isPlayerOne == true)
        {
            if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                //_animator.SetBool("isTurnRight", false);
                _animator.SetBool("isTurnLeft", true);
            }
            else if(Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
            {
                //_animator.SetBool("isTurnRight", false);
                _animator.SetBool("isTurnLeft", false);
            }

            if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                //_animator.SetBool("isTurnLeft", false);
                _animator.SetBool("isTurnRight", true);
            }
            else if(Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
            {
                //_animator.SetBool("isTurnLeft", false);
                _animator.SetBool("isTurnRight", false);
            }      
        }
        else if(_player.isPlayerTwo == true)
        {
            if(Input.GetKeyDown(KeyCode.Keypad4))
            {
                //_animator.SetBool("isTurnRight", false);
                _animator.SetBool("isTurnLeft", true);
            }
            else if(Input.GetKeyUp(KeyCode.Keypad4))
            {
                //_animator.SetBool("isTurnRight", false);
                _animator.SetBool("isTurnLeft", false);
            }

            if(Input.GetKeyDown(KeyCode.Keypad6))
            {
                //_animator.SetBool("isTurnLeft", false);
                _animator.SetBool("isTurnRight", true);
            }
            else if(Input.GetKeyUp(KeyCode.Keypad6))
            {
                //_animator.SetBool("isTurnLeft", false);
                _animator.SetBool("isTurnRight", false);
            }      
        }
    }
}
