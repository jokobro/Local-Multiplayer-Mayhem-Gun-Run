using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private InputActionAsset _inputActions;
    [SerializeField] private GameObject _pauseScreen;
    private InputActionMap _gameActionMap;
    private InputActionMap _uiActionMap;


    private void Start()
    {
        _gameActionMap = _inputActions.FindActionMap("Game");
        _uiActionMap = _inputActions.FindActionMap("UI");

        //start met Game action map enabled en UI action map disabled
        _gameActionMap.Enable();
        _uiActionMap.Disable();
    }

    public void Pausing(InputAction.CallbackContext Context)
    {
        if (Context.performed)
        {
            Debug.Log("Paused");
            Time.timeScale = 0f;
            _pauseScreen.SetActive(true);
            _gameActionMap.Disable();
            _uiActionMap.Enable();
        }
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        _pauseScreen.SetActive(false);
        _gameActionMap.Enable();
        _uiActionMap.Disable();
    }
}
