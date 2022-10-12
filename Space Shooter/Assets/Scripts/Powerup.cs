using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField] private float _speed = 3.0f;
    //ID for powerups, 0=Tripple Laser, 1=Speed, 2=Shield, 3=Heart, 4=Coin
    [SerializeField] private int _powerupID;

    //[SerializeField] private AudioClip _clip;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if(transform.position.y < -5f)
        {
            Destroy(this.gameObject);
        }
    }

   private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player1")
        {
            Player player1 = other.transform.GetComponent<Player>();
            //AudioSource.PlayClipAtPoint(_clip, transform.position);
            if(player1 != null)
            {
                switch(_powerupID)
                {
                    case 0:
                        player1.TripleLaserActive();
                        AudioManager.instance.Play("Power_Up_Sound");
                        break;
                    case 1:
                        player1.SpeedActive();
                        AudioManager.instance.Play("Power_Up_Sound");                        
                        break;
                    case 2:
                        player1.ShieldActive();
                        AudioManager.instance.Play("Power_Up_Sound");
                        break;
                    case 3:
                        //player1.AddLifeP1(1);
                        player1.AddLife(1);
                        AudioManager.instance.Play("Heart_Collect_Sound");                       
                        break;
                    case 4:
                        //player1.AddCoinP1(10);
                        player1.AddCoin(10);
                        AudioManager.instance.Play("Coin_Collect_Sound");                       
                        break;
                    default:
                        Debug.Log("Default Value");
                        break;
                }
            }
            Destroy(this.gameObject);
            
        }

        if (other.tag == "Player2")
        {
            Player player2 = other.transform.GetComponent<Player>();
            //AudioSource.PlayClipAtPoint(_clip, transform.position);
            if(player2 != null)
            {
                switch(_powerupID)
                {
                    case 0:
                        player2.TripleLaserActive();
                        AudioManager.instance.Play("Power_Up_Sound");
                        break;
                    case 1:
                        player2.SpeedActive();
                        AudioManager.instance.Play("Power_Up_Sound");                        
                        break;
                    case 2:
                        player2.ShieldActive();
                        AudioManager.instance.Play("Power_Up_Sound");                        
                        break;
                    case 3:
                        //player2.AddLifeP2(1);
                        player2.AddLife(1);
                        AudioManager.instance.Play("Heart_Collect_Sound");               
                        break;                        
                    case 4:
                        //player2.AddCoinP2(10);
                        player2.AddCoin(10);
                        AudioManager.instance.Play("Coin_Collect_Sound");
                        break;
                    default:
                        Debug.Log("Default Value");
                        break;
                }
            }
            Destroy(this.gameObject);
        }      
    }
}
