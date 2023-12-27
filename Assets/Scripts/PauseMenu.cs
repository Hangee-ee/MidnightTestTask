using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    private Canvas pauseCanvas;
    public PauseManager pauseManager;
    public WinManager winManager;
    public GameManager gameManager;

    public TMP_Text winsText;
    public TMP_Text lossesText;

    void Start()
    {
        pauseCanvas = GetComponent<Canvas>();
        pauseManager = FindObjectOfType<PauseManager>();
        pauseCanvas.enabled = false;
        winManager = FindObjectOfType<WinManager>();
        gameManager = GameManager.Instance;

        UpdateUI();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseCanvas.enabled = !pauseCanvas.enabled;

            if (pauseCanvas.enabled)
            {
                pauseManager.PauseGame();
            }
            else
            {
                pauseManager.ResumeGame();
            }
        }
    }

    void UpdateUI()
    {
        winsText.text = "Wins: " + GameManager.Instance.GetWins();
        lossesText.text = "Losses: " + GameManager.Instance.GetLosses();
    }
}
