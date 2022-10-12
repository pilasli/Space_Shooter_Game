using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private SavePrefs _savePrefs;

    void Start()
    {
        AudioManager.instance.Play("Main_Menu_Music");
        _savePrefs = GameObject.Find("Save_Game").GetComponent<SavePrefs>();
        {
            if(_savePrefs == null)
            {
                Debug.LogError("SavePrefs on MainManu is <null>");
            }
        }
    }

    /*public void LoadSinglePlayer()
    {
        AudioManager.instance.Stop("Main_Menu_Music");
        Debug.Log("Single Player Game is Loading...");
        SceneManager.LoadScene("Single_Player");
    }

    public void LoadCoOpMode()
    {
        AudioManager.instance.Stop("Main_Menu_Music");
        Debug.Log("Co-Op Mode is Loading...");
        SceneManager.LoadScene("Co-Op_Mode");
    }*/

    public void LoadGame(string mode)
    {
        _savePrefs.coopModeToSave = mode;
        _savePrefs.SaveData();
        AudioManager.instance.Stop("Main_Menu_Music");
        Debug.Log("Single Player Game is Loading...");
        SceneManager.LoadScene("Level_1");        
    }
}
