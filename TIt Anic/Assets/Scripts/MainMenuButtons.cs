using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    public void ExitGame()
    {
        Application.Quit();
    }

    public void SwapScenes()
    {
        SceneManager.LoadScene("Master_Scene");
    }
}
