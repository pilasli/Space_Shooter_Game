using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text _scoreTextP1;
    [SerializeField] private Text _scoreTextP2;
    [SerializeField] private Text _coinTextP1;
    [SerializeField] private Text _coinTextP2;
    [SerializeField] private Text _gameOverText;
    [SerializeField] private Text _restartText;
    [SerializeField] private Sprite _fullLive;
    [SerializeField] private Sprite _emptyLive;
    [SerializeField] private GameObject _pauseMenuPanel;
    [SerializeField] private GameObject _livesP1Panel;
    [SerializeField] private GameObject _livesP2Panel;
    public Image[] _livesImgP1;
    public Image[] _livesImgP2;

    private GameManager _gameManager;
    
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        if(_gameManager == null)
        {
            Debug.LogError("The Game Manager on UI Manager is NULL.");
        }
        if(_gameManager._isCoOpMode == false)
        {
            _scoreTextP1.text = "Score  : " + 0;
            _scoreTextP2.enabled = false;
            _coinTextP1.text = "Gold    : " + 0;
            _coinTextP2.enabled = false;
            _livesP2Panel.gameObject.SetActive(false);
        }
        else if(_gameManager._isCoOpMode == true)
        {
            _scoreTextP1.text = "Score  : " + 0;
            _scoreTextP2.text = "Score  : " + 0;
            _coinTextP1.text = "Gold    : " + 0;
            _coinTextP2.text = "Gold    : " + 0;
        }
        _gameOverText.gameObject.SetActive(false);
        _restartText.gameObject.SetActive(false);
    }

    public void UpdateScoreP1(int score)
    {
        _scoreTextP1.text = "Score  : " + score.ToString();
    }
    public void UpdateScoreP2(int score)
    {
        _scoreTextP2.text = "Score  : " + score.ToString();
    }

    public void UpdateGoldP1(int gold)
    {
        _coinTextP1.text = "Gold    : " + gold.ToString();
    }
    public void UpdateGoldP2(int gold)
    {
        _coinTextP2.text = "Gold    : " + gold.ToString();
    }
    
    public void UpdateNumOfLivesP1(int numOfLives)
    {
        for(int i= 0; i < numOfLives; i++)
        {
            _livesImgP1[i].gameObject.SetActive(true);
            _livesImgP1[i].sprite = _fullLive;
        }
    }
    public void UpdateNumOfLivesP2(int numOfLives)
    {
        for(int i= 0; i < numOfLives; i++)
        {
            _livesImgP2[i].gameObject.SetActive(true);
            _livesImgP2[i].sprite = _fullLive;
        }                
    }

    public void UpdateLivesP1(int lives, int numOfLives)
    {
        for(int i = 0 ; i < lives; i++)
        {
            _livesImgP1[i].sprite = _fullLive;
        }
        for(int i = lives ; i < numOfLives; i++)
        {
            _livesImgP1[i].sprite = _emptyLive;
        }
        if(lives == 0)
        {
            GameOverSequence();
        }
    }
    public void UpdateLivesP2(int lives, int numOfLives)
    {
        for(int i = 0 ; i < lives; i++)
        {
            _livesImgP2[i].sprite = _fullLive;
        }
        for(int i = lives ; i < numOfLives; i++)
        {
            _livesImgP2[i].sprite = _emptyLive;
        }
        if(lives == 0)
        {
            GameOverSequence();
        }
    }

    void GameOverSequence()
    {
        _gameManager.GameOver();
        _gameOverText.gameObject.SetActive(true);
        _restartText.gameObject.SetActive(true);
        StartCoroutine(GameOverFlickerRoutine());
    }

    IEnumerator GameOverFlickerRoutine()
    {
        while(true)
        {
            _gameOverText.text = "GAME OVER";
            yield return new WaitForSeconds(0.5f);
            _gameOverText.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void HideInPause()
    {
        if(!_gameManager._isCoOpMode)
        {
            _livesP1Panel.gameObject.SetActive(false);
            _coinTextP1.enabled = false;
            _scoreTextP1.enabled = false;
        }
        else
        {
        _livesP1Panel.gameObject.SetActive(false);
        _livesP2Panel.gameObject.SetActive(false);   
        _coinTextP1.enabled = false;
        _coinTextP2.enabled = false;       
        _scoreTextP1.enabled = false;
        _scoreTextP2.enabled = false;
        }

    }
    public void ShowOutPause()
    {
        if(!_gameManager._isCoOpMode)
        {
            _livesP1Panel.gameObject.SetActive(true);
            _coinTextP1.enabled = true;
            _scoreTextP1.enabled = true;                
        }
        else
        {
            _livesP1Panel.gameObject.SetActive(true);
            _livesP2Panel.gameObject.SetActive(true);   
            _coinTextP1.enabled = true;
            _coinTextP2.enabled = true;       
            _scoreTextP1.enabled = true;
            _scoreTextP2.enabled = true;
        }
    }
}
