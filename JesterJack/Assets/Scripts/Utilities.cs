using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Utilities : MonoBehaviour
{
    public void GoToGame()
    {
        SceneManager.LoadScene("NatashaScene");
    }

    public void QuitGame()
    { 
        Application.Quit();
    }

   /* public void Pauze()
    {
        Time.timeScale = 0;
    } */
    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }
}
