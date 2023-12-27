using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        Debug.Log("PlayGame clicked");
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Debug.Log("QuitGame clicked");
        Application.Quit();
    }
}
