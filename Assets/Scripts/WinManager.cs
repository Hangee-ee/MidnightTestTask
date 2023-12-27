using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WinManager : MonoBehaviour
{
    public Canvas youWonCanvas;
    public TextMeshProUGUI zombiesCountText;

    private int totalZombies;
    private int zombiesRemaining;
    public bool GameWon = false;

    void Start()
    {
        totalZombies = GameObject.FindGameObjectsWithTag("Zombie").Length;
        zombiesRemaining = totalZombies;
        youWonCanvas.enabled = false;

        UpdateZombiesCountText();
    }

    public void ZombieDied()
    {
        zombiesRemaining--;

        UpdateZombiesCountText();

        if (zombiesRemaining <= 0)
        {
            GameWin();
        }
    }

    private void UpdateZombiesCountText()
    {
        if (zombiesCountText != null)
        {
            zombiesCountText.text = $"{zombiesRemaining:D3}/{totalZombies:D3}";
        }
    }

    public void GameWin()
    {
        GameWon = true;
        Time.timeScale = 0;
        youWonCanvas.enabled = true;
        GameManager.Instance.IncrementWins();
    }
}