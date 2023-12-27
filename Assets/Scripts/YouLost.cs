using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class YouLost : MonoBehaviour
{
    public PauseManager pauseManager;
    private Canvas YouLostCanvas;

    void Start()
    {
    pauseManager = FindObjectOfType<PauseManager>();
    YouLostCanvas = GetComponent<Canvas>();
    }

    void Update()
    {
        if (YouLostCanvas.enabled)
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