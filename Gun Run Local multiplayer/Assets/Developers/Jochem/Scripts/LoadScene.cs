using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadScene : MonoBehaviour
{    [SerializeField] private GameObject _creditsPanel;
    public bool StartButtonPressed;

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("");
        StartButtonPressed = true;
    }
    
    public void ReloadScene()
    {
       string currentSceneName = SceneManager.GetActiveScene().name;
       SceneManager.LoadScene(currentSceneName);
       Debug.Log("reload");
    }

    public void LoadLobby()
    {
      SceneManager.LoadScene("");
      Debug.Log("Load lobby");
    }

    public void LoadCredits()
    {
     _creditsPanel.SetActive(true);
    }

    public void returnMainMenuCredits()
    {
      _creditsPanel.SetActive(false);
    }
     
    public void Quit()
    {
      Debug.Log("Has Quit");
      Application.Quit();
    }


}
