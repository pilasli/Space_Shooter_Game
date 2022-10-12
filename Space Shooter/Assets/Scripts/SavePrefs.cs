using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePrefs : MonoBehaviour
{
    public string coopModeToSave = "Default";
    // Start is called before the first frame update
    public void SaveData()
    {
        PlayerPrefs.SetString("SavedCoopMode", coopModeToSave);
        PlayerPrefs.Save();
        Debug.Log("Game data saved");
    }

    public void LoadData()
    {
        if(PlayerPrefs.HasKey("SavedCoopMode"))
        {
            coopModeToSave = PlayerPrefs.GetString("SavedCoopMode");
            Debug.Log("Game data loaded!");
        }
        else
        {
            Debug.LogError("There is no save data");
        }        
    }

    public void ResetData()
    {
        PlayerPrefs.DeleteAll();
        coopModeToSave = "Default";
    }
}
