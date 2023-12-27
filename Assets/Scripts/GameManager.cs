using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private const string WinsKey = "Wins";
    private const string LossesKey = "Losses";

    private int wins;
    private int losses;

    public static GameManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        LoadPlayerData();
    }

    public void IncrementWins()
    {
        wins++;
        SavePlayerData();
    }

    public void IncrementLosses()
    {
        losses++;
        SavePlayerData();
    }

    private void LoadPlayerData()
    {
        wins = PlayerPrefs.GetInt(WinsKey, 0);
        losses = PlayerPrefs.GetInt(LossesKey, 0);
    }

    private void SavePlayerData()
    {
        PlayerPrefs.SetInt(WinsKey, wins);
        PlayerPrefs.SetInt(LossesKey, losses);
        PlayerPrefs.Save();
    }

    public int GetWins()
    {
        return wins;
    }

    public int GetLosses()
    {
        return losses;
    }
}
