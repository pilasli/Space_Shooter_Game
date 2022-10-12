using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool _isGamePaused = false;
    public bool _isShopOn = false;
    public bool _isCoOpMode;
    public bool _isGameOver = false;

    private SavePrefs _savePrefs; 
    private SpawnManager _spawnManager;
    private UIManager _uiManager;
    private GameMenu _gameMenu;

    [SerializeField] private GameObject _pauseMenuPanel;
    [SerializeField] private GameObject _shopPanel;
    [SerializeField] private GameObject _singlePlayerPrefab;
    [SerializeField] private GameObject _coopPlayersPrefab;

    void Awake()
    {
        _savePrefs = GameObject.Find("Save_Game").GetComponent<SavePrefs>();
        if(_savePrefs == null)
        {
            Debug.LogError("SavePrefs on Player is <null>");
        }
        _savePrefs.LoadData();
        if(_savePrefs.coopModeToSave == "CoopFalse")
        {
            _isCoOpMode = false;
            Instantiate(_singlePlayerPrefab, Vector3.zero, Quaternion.identity);

        }
        if(_savePrefs.coopModeToSave == "CoopTrue")
        {
            _isCoOpMode = true;
            Instantiate(_coopPlayersPrefab, Vector3.zero, Quaternion.identity);
        }

        /*if(_isCoOpMode == false)
        {
            Instantiate(_singlePlayerPrefab, Vector3.zero, Quaternion.identity);
        }
        else if (_isCoOpMode == true)
        {
            Instantiate(_coopPlayersPrefab, Vector3.zero, Quaternion.identity);
        }*/

    }

    // Start is called before the first frame update
    void Start()
    {
        //_pauseAnimator = GameObject.Find("Pause_Menu_Panel").GetComponent<Animator>();
        //_pauseAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if(_uiManager == null)
        {
            Debug.LogError("The UI Manager on GameManager is <null>");
        }
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        if(_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager on GameManager is <null>");
        }
        _gameMenu = GameObject.Find("Game_Menu").GetComponent<GameMenu>();
        if(_gameMenu == null)
        {
            Debug.LogError("The GameMenu on GameManager is <null>");
        }

        AudioManager.instance.Play("Background_Music");
        _gameMenu.CursorLockOn();
    }

    // Update is called once per frame
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R) && _isGameOver == true)
        {
            SceneManager.LoadScene("Level_1");
            /*if(_isCoOpMode == false)
            {
                SceneManager.LoadScene("Level_1"); //Current Game Scene
            }
            else if (_isCoOpMode == true)
            {
                SceneManager.LoadScene("Level_1"); //Current Game Scene
            }*/
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(!_isShopOn)
            {
                if(_isGamePaused)
                {
                    ResumeGame();
                }
                else
                {
                    PauseGame();
                }
            }
        }

        if(Input.GetKeyDown(KeyCode.M))
        {
            if(!_isGamePaused)
            {
                if(_isShopOn)
                {
                    CloseShop();
                }
                else
                {
                    OpenShop();
                }
            }

        }
    }

    public void GameOver()
    {
        Debug.Log("GameManager::GameOver() Called");
        _isGameOver = true;
        //_spawnManager.StartSpawning();
    }

    public void PauseGame()
    {
        _uiManager.HideInPause();
        _pauseMenuPanel.SetActive(true);
        _gameMenu.CursorLockOff();
        //_pauseAnimator.SetBool("isPaused", true);
        _isGamePaused = true;
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        if(_pauseMenuPanel.activeSelf)
        {
            _gameMenu.CursorLockOn();
            _pauseMenuPanel.SetActive(false);
            _uiManager.ShowOutPause();
            //_pauseAnimator.SetBool("isPaused", false);
            _isGamePaused = false;
            Time.timeScale = 1.0f;
        }
    }

    public void OpenShop()
    {
        _uiManager.HideInPause();
        _shopPanel.SetActive(true);
        _gameMenu.CursorLockOff();
        _isShopOn =  true;
        Time.timeScale = 0f;
    }

    public void CloseShop()
    {
        _gameMenu.CursorLockOn();
        _shopPanel.SetActive(false);
        _uiManager.ShowOutPause();
        _isShopOn = false;
        Time.timeScale = 1.0f;
    }

    public void GoMainMenu()
    {
        AudioManager.instance.Stop("Background_Music");
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Main_Menu");
    }
}
