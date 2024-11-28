using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJoinHandler : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    public void OnPlayerJoined(PlayerInput playerInput)
    {
        PlayerController playerController = playerInput.GetComponent<PlayerController>();
        if (playerController != null)
        {
            gameManager.AddPlayer(playerController);
        }
        else
        {
            Debug.LogError("PlayerInput heeft geen PlayerController!");
        }
    }
}
