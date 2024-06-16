using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class ButtonClick : MonoBehaviour
{

    public void OnPlayButtonClicked()
    {
        // load the game scene
        SceneManager.LoadScene("YouWonScreen");
    }

    public void OnGameInfoButtonClicked()
    {
        SceneManager.LoadScene("GameInfoMenu");
    }

    public void OnQuitButtonClicked()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }

    public void OnMenuButtonClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
