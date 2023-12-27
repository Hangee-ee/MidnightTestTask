using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class YouWon : MonoBehaviour
{
    public PauseManager pauseManager;
    private Canvas YouWonCanvas;

    void Start()
    {
    pauseManager = FindObjectOfType<PauseManager>();
    YouWonCanvas = GetComponent<Canvas>();
    }

    void Update()
    {
        if (YouWonCanvas.enabled)
        {
            pauseManager.PauseGame();
        }
    }
    
    public void OnTryAgainButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnQuitButton()
    {
        Application.Quit();
    }
}
