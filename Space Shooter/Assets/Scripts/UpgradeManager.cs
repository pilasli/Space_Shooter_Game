using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    private UIManager _uiManager;
    private GameManager _gameManager;
    private Player _player1;
    private Player _player2;
    [SerializeField] private Button _extraArmorButtonP1;
    [SerializeField] private Button _empoweredLaserButtonP1;
    [SerializeField] private Button _ironWillButtonP1;
    [SerializeField] private Button _concentrationButtonP1;
    [SerializeField] private Button _machineGunButtonP1;
    [SerializeField] private Button _ultimatePowerButtonP1;
    [SerializeField] private Button _extraArmorButtonP2;
    [SerializeField] private Button _empoweredLaserButtonP2;
    [SerializeField] private Button _ironWillButtonP2;
    [SerializeField] private Button _concentrationButtonP2;
    [SerializeField] private Button _machineGunButtonP2;
    [SerializeField] private Button _ultimatePowerButtonP2;
    [SerializeField] private Image _extraArmorImageP1;
    [SerializeField] private Image _empoweredLaserImageP1;
    [SerializeField] private Image _ironWillImageP1;
    [SerializeField] private Image _concentrationImageP1;
    [SerializeField] private Image _machineGunImageP1;
    [SerializeField] private Image _ultimatePowerImageP1;
    [SerializeField] private Image _extraArmorImageP2;
    [SerializeField] private Image _empoweredLaserImageP2;
    [SerializeField] private Image _ironWillImageP2;
    [SerializeField] private Image _concentrationImageP2;
    [SerializeField] private Image _machineGunImageP2;
    [SerializeField] private Image _ultimatePowerImageP2;
    private int extraArmorUpgradeCost = 200;
    private int empoweredLaserUpgradeCost = 250;
    private int ironWillUpgradeCost = 200;
    private int concentrationUpgardeCost = 100;
    private int machineGunUpgradeCost = 250;
    private int ultimatePowerUpgradeCost = 250;
    
    [SerializeField] private GameObject _player2Header;
    [SerializeField] private GameObject[] _player2Upgrades;

    void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if(_uiManager == null)
        {
            Debug.LogError("The UI Manager on Upgrade Manager is NULL!");
        }
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        if(_gameManager == null)
        {
            Debug.LogError("The Game Manager on Enemy is <null>");
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
        SetStart();
        SetButtons();
    }

    private void SetStart()
    {
        if(_gameManager._isCoOpMode == true)
        {
            _player2Header.SetActive(true);
            for(int i=0; i<_player2Upgrades.Length; i++)
            {
                _player2Upgrades[i].SetActive(true);
            }
        }
        else
        {
            _player2Header.SetActive(false);
            for(int i=0; i<_player2Upgrades.Length; i++)
            {
                _player2Upgrades[i].SetActive(false);
            }           
        }
    }

    private void SetButtons()
    {
        Text textArmor1 = _extraArmorButtonP1.GetComponentInChildren<Text>();
        Text textArmor2 = _extraArmorButtonP2.GetComponentInChildren<Text>();
        textArmor1.text = extraArmorUpgradeCost + " Gold";
        textArmor2.text = extraArmorUpgradeCost + " Gold";
        Text textLaserShot1 = _empoweredLaserButtonP1.GetComponentInChildren<Text>();
        Text textLaserShot2 = _empoweredLaserButtonP2.GetComponentInChildren<Text>();
        textLaserShot1.text = empoweredLaserUpgradeCost + " Gold";
        textLaserShot2.text = empoweredLaserUpgradeCost + " Gold";
        Text textIronWill1 = _ironWillButtonP1.GetComponentInChildren<Text>();
        Text textIronWill2 = _ironWillButtonP2.GetComponentInChildren<Text>();
        textIronWill1.text = ironWillUpgradeCost + " Gold";
        textIronWill2.text = ironWillUpgradeCost + " Gold";
        Text textConcentration1 = _concentrationButtonP1.GetComponentInChildren<Text>();
        Text textConcentration2 = _concentrationButtonP2.GetComponentInChildren<Text>();
        textConcentration1.text = concentrationUpgardeCost + " Gold";
        textConcentration2.text = concentrationUpgardeCost + " Gold";
        Text textMachineGun1 = _machineGunButtonP1.GetComponentInChildren<Text>();
        Text textMachineGun2 = _machineGunButtonP2.GetComponentInChildren<Text>();
        textMachineGun1.text = machineGunUpgradeCost + " Gold";
        textMachineGun2.text = machineGunUpgradeCost + " Gold";
        Text textUltimate1 = _ultimatePowerButtonP1.GetComponentInChildren<Text>();
        Text textUltimate2 = _ultimatePowerButtonP2.GetComponentInChildren<Text>();
        textUltimate1.text = ultimatePowerUpgradeCost + " Gold";
        textUltimate2.text = ultimatePowerUpgradeCost + " Gold";
    }

    public void UpgradeShieldP1()
    {
        if(_player1._gold >= extraArmorUpgradeCost)
        {
            _player1._gold -= extraArmorUpgradeCost;
            _uiManager.UpdateGoldP1(_player1._gold);            
            _extraArmorButtonP1.interactable = false;
            _extraArmorImageP1.color = Color.grey; 
            _player1._numOfShields++;
            AudioManager.instance.Play("Buy_Sound");
        }
        else
        {
            AudioManager.instance.Play("Cant_Buy_Sound");
        }
    }
    public void UpgradeShieldP2()
    {
        if(_player2._gold >= extraArmorUpgradeCost)
        {
            _player2._gold -= extraArmorUpgradeCost;
            _uiManager.UpdateGoldP2(_player2._gold);            
            _extraArmorButtonP2.interactable = false;
            _extraArmorImageP2.color = Color.grey; 
            _player2._numOfShields++;
            AudioManager.instance.Play("Buy_Sound");            
        }
        else
        {
            AudioManager.instance.Play("Cant_Buy_Sound");
        }        
    }

    public void UpgradeLaserShotDamageP1()
    {
        if(_player1._gold >= empoweredLaserUpgradeCost)
        {
            _player1._gold -= empoweredLaserUpgradeCost;
            _uiManager.UpdateGoldP1(_player1._gold);
            _empoweredLaserButtonP1.interactable = false;
            _empoweredLaserImageP1.color = Color.grey;
            _player1._playerLaserShotDamage *=  1.5f;
            AudioManager.instance.Play("Buy_Sound");            
        }
        else
        {
            AudioManager.instance.Play("Cant_Buy_Sound");
        }
    }
    public void UpgradeLaserShotDamageP2()
    {
        if(_player2._gold >= empoweredLaserUpgradeCost)
        {
            _player2._gold -= empoweredLaserUpgradeCost;
            _uiManager.UpdateGoldP2(_player2._gold);            
            _empoweredLaserButtonP2.interactable = false;
            _empoweredLaserImageP2.color = Color.grey;
            _player2._playerLaserShotDamage *= 1.5f;
            AudioManager.instance.Play("Buy_Sound");                       
        }
        else
        {
            AudioManager.instance.Play("Cant_Buy_Sound");
        }        
    }

    public void UpgradeLiveP1()
    {
        if(_player1._gold >= ironWillUpgradeCost)
        {
            _player1._gold -= ironWillUpgradeCost;
            _uiManager.UpdateGoldP1(_player1._gold);
            if(_player1._numOfLives < _uiManager._livesImgP1.Length)
            {
                _player1._numOfLives++;
                _player1._lives = _player1._numOfLives;
                _uiManager.UpdateNumOfLivesP1(_player1._numOfLives);
                _player1.ChangeVisualiser();
                AudioManager.instance.Play("Buy_Sound");                
                if(_player1._numOfLives == _uiManager._livesImgP1.Length)
                {
                    _ironWillButtonP1.interactable = false;
                    _ironWillImageP1.color = Color.grey;
                }
            }
        }
        else
        {
            AudioManager.instance.Play("Cant_Buy_Sound");
        }        
    }
    public void UpgradeLiveP2()
    {
        if(_player2._gold >= ironWillUpgradeCost)
        {
            _player2._gold -= ironWillUpgradeCost;
            _uiManager.UpdateGoldP2(_player2._gold);            
            if(_player2._numOfLives < _uiManager._livesImgP2.Length)
            {
                _player2._numOfLives++;
                _player2._lives = _player1._numOfLives;
                _uiManager.UpdateNumOfLivesP2(_player1._numOfLives);
                _player2.ChangeVisualiser();
                AudioManager.instance.Play("Buy_Sound");                
                if(_player2._numOfLives == _uiManager._livesImgP1.Length)
                {
                    _ironWillButtonP2.interactable = false;
                    _ironWillImageP2.color = Color.grey;
                }
            }
        }
        else
        {
            AudioManager.instance.Play("Cant_Buy_Sound");
        }        
    }
    public void UpgradePowerupTimeP1()
    {
        if(_player1._gold >= concentrationUpgardeCost)
        {
            _player1._gold -= concentrationUpgardeCost;
            _uiManager.UpdateGoldP1(_player1._gold);
            _concentrationButtonP1.interactable = false;
            _concentrationImageP1.color = Color.grey;
            _player1._powerupTime *=  1.5f;
            AudioManager.instance.Play("Buy_Sound");                
        }
        else
        {
            AudioManager.instance.Play("Cant_Buy_Sound");
        }        
    }
    public void UpgradePowerupTimeP2()
    {
        if(_player2._gold >= concentrationUpgardeCost)
        {
            _player2._gold -= concentrationUpgardeCost;
            _uiManager.UpdateGoldP2(_player2._gold);            
            _concentrationButtonP2.interactable = false;
            _concentrationImageP2.color = Color.grey;
            _player2._powerupTime *= 1.5f;
            AudioManager.instance.Play("Buy_Sound");                       
        }
        else
        {
            AudioManager.instance.Play("Cant_Buy_Sound");
        }               
    }   
    public void UpgradeFireRateP1()
    {
        if(_player1._gold >= machineGunUpgradeCost)
        {
            _player1._gold -= machineGunUpgradeCost;
            _uiManager.UpdateGoldP1(_player1._gold);            
            _machineGunButtonP1.interactable = false;
            _machineGunImageP1.color = Color.grey;
            _player1._laserFireRate *= 0.8f;
            AudioManager.instance.Play("Buy_Sound");                        
        }        
        else
        {
            AudioManager.instance.Play("Cant_Buy_Sound");
        }
    }
    public void UpgradeFireRateP2()
    {
        if(_player2._gold >= machineGunUpgradeCost)
        {
            _player2._gold -= machineGunUpgradeCost;
            _uiManager.UpdateGoldP2(_player2._gold);            
            _machineGunButtonP2.interactable = false;
            _machineGunImageP2.color = Color.grey;
            _player2._laserFireRate *= 0.8f;
            AudioManager.instance.Play("Buy_Sound");                       
        }
        else
        {
            AudioManager.instance.Play("Cant_Buy_Sound");
        }        
    }    
    public void UpgradeUltimateP1()
    {
        if(_player1._gold >= ultimatePowerUpgradeCost)
        {
            _player1._gold -= ultimatePowerUpgradeCost;
            _uiManager.UpdateGoldP1(_player1._gold);            
            _ultimatePowerButtonP1.interactable = false;
            _ultimatePowerImageP1.color = Color.grey;
            AudioManager.instance.Play("Buy_Sound");            
            Debug.Log("Ultimate power P1 has upgraded");            
        }
        else
        {
            AudioManager.instance.Play("Cant_Buy_Sound");
        }        
    }
    public void UpgradeUltimateP2()
    {
        if(_player2._gold >= ultimatePowerUpgradeCost)
        {
            _player2._gold -= ultimatePowerUpgradeCost;
            _uiManager.UpdateGoldP2(_player2._gold);            
            _ultimatePowerButtonP2.interactable = false;
            _ultimatePowerImageP2.color = Color.grey;
            AudioManager.instance.Play("Buy_Sound");            
            Debug.Log("Ultimate power P2 has upgraded");        
        }
        else
        {
            AudioManager.instance.Play("Cant_Buy_Sound");
        }        
    }   
}
