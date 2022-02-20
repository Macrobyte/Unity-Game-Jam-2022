using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static PlayerData Instance;

    public string saveName;
    public Data playerData;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            LoadData();

            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    public void SaveData()
    {
        JsonSaveSystem.Save(saveName, playerData);
    }

    public void LoadData()
    {
        Data savedData =  JsonSaveSystem.Load<Data>(saveName);
        if (savedData != null) playerData = savedData;
    }
}


[System.Serializable]
public class Data
{
    public int higestScore;
    public int lastScore;
    public int highestWasteCollected;
}