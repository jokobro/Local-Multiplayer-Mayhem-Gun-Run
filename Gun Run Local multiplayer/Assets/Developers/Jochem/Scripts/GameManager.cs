using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _checkPoint;
    public List<GameObject> players = new List<GameObject>();
    private bool gameStarted = false;

    private void Start()
    {
        // Optioneel: houd UI verborgen totdat er genoeg spelers zijn.
    }

    public void OnPlayerJoined(PlayerInput playerInput)
    {
        players.Add(playerInput.gameObject);

        if (players.Count >= 2 && !gameStarted)
        {
            StartGame();
        }
    }

    public void OnPlayerLeft(PlayerInput playerInput)
    {
        players.Remove(playerInput.gameObject);

        if (gameStarted && players.Count < 2)
        {
            EndGame(); // Eindig het spel als er niet genoeg spelers zijn.
        }
    }

    private void StartGame()
    {
        gameStarted = true;

        // Kies een willekeurige gunner
        int gunnerIndex = Random.Range(0, players.Count);
        players[gunnerIndex].GetComponent<PlayerController>().AssignGunner();

        // Begin de spelmechanieken
        Debug.Log("Game started!");
    }

    private void EndGame()
    {
        gameStarted = false;
        Debug.Log("Game ended due to insufficient players.");
    }

    private void checkPlayerPassedCheckpoint()
    {

    }
}
