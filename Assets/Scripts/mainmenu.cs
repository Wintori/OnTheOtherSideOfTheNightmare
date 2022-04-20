using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainmenu : MonoBehaviour
{
    public GameObject settingsmenu;

    public void StartTutorial()
    {
        SceneManager.LoadScene(1);
    }
    public void StartGame()
    {
        SceneManager.LoadScene(2);
    }
    public void OpenOptions()
    {
        settingsmenu.SetActive(true);
    }
    public void CloseOptions()
    {
        settingsmenu.SetActive(false);
    }
    public void ExitGame()
    {
        Debug.Log("Quitting");
        Application.Quit();
    }
}
