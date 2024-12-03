using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public PlayerController[] Players;
    public List<PlayerController> Runners = new List<PlayerController>(); // Renners (geen gunner)
    private void Start()
    {
        AssignRandomGunner();
        UpdateRunnersList(); // Update de lijst van renners
    }

    private void AssignRandomGunner()
    {
        if (Players.Length == 0)
        {
            Debug.LogError("No players found!");
            return;
        }

        int randomIndex = Random.Range(0, Players.Length); // Kies een willekeurige speler
        Players[randomIndex].AssignGunner();

        Debug.Log($"Player {Players[randomIndex].PlayerNumber} is assigned as the gunner!");
    }

    private void UpdateRunnersList()
    {
        Runners.Clear(); // Zorg ervoor dat de lijst leeg is voordat je begint
        foreach (var player in Players)
        {
            if (!player.IsGunner)
            {
                Runners.Add(player);
            }
        }

        Debug.Log($"Runners updated. Total runners: {Runners.Count}");
    }
}
