using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private InputActionAsset _inputActions;
    [SerializeField] private GameObject _pauseScreen;
    private InputActionMap _gameActionMap;
    private InputActionMap _uiActionMap;
    private bool _isPaused = false;
    

    private void Start()
    {
        _gameActionMap = _inputActions.FindActionMap("Game");
        _uiActionMap = _inputActions.FindActionMap("UI");

        //start met Game action map enabled en UI action map disabled
        _gameActionMap.Enable();
        _uiActionMap.Disable();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Pausing();
            _isPaused = true;
        }

    }

    private void Pausing()
    {
        Debug.Log("Paused"); 
        Time.timeScale = 0f;
        _pauseScreen.SetActive(true);
        _gameActionMap.Disable();
        _uiActionMap.Enable();
    }

    private void Resume()
    {
        Time.timeScale = 1f;
        _pauseScreen.SetActive(false);
        _gameActionMap.Enable();
        _uiActionMap.Disable();
    }
}
