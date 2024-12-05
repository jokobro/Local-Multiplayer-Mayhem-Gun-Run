using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] PlayerPrefabs; // Prefab van de speler
    [SerializeField] private GameObject _gunnerPrefab;
    [SerializeField] private Transform[] SpawnPoints; // Spawnposities voor spelers
    public List<PlayerController> Players = new List<PlayerController>(); // Lijst met alle spelers
    public List<PlayerController> Runners = new List<PlayerController>(); // Lijst met renners (geen gunner)
    public int MaxPlayers = 4;
    private bool _gameStarted = false;
    private PlayerController _gunner;
   

    public void AddPlayer()
    {
        if (Players.Count >= MaxPlayers)
        {
            Debug.Log("Max aantal spelers bereikt!");
            return;
        }

        if (Players.Count >= PlayerPrefabs.Length)
        {
            Debug.LogError("Niet genoeg unieke prefabs beschikbaar voor spelers.");
            return;
        }

        int playerNumber = Players.Count + 1; // Bepaal het PlayerNumber
        Transform spawnPoint = SpawnPoints[Players.Count]; // Kies spawnlocatie
        GameObject playerObj = Instantiate(PlayerPrefabs[Players.Count],spawnPoint.position,Quaternion.Euler(-90, spawnPoint.rotation.eulerAngles.y, spawnPoint.rotation.eulerAngles.z));
        PlayerController playerController = playerObj.GetComponent<PlayerController>();
        playerController.PlayerNumber = playerNumber; // Ken een uniek PlayerNumber toe
        DontDestroyOnLoad(playerObj);
       
        Players.Add(playerController);
        Debug.Log($"Speler {playerNumber} is gejoint!");
    }

    public void StartGame()
    {
        if (Players.Count < 2)
        {
            Debug.LogError("Niet genoeg spelers om het spel te starten. Minimaal 2 spelers nodig.");
            return;
        }

        AssignRandomGunner();
        _gameStarted = true;
        SceneManager.LoadScene("Game Scene final");
        Debug.Log("Het spel is gestart!");
    }

    private void AssignRandomGunner()
    {
       int randomIndex = Random.Range(0, Players.Count);
        _gunner = Players[randomIndex];
        _gunner.AssignGunner(); // Zet de speler in de "gunner"-rol


        // Vervang de speler door een speciale gunner prefab
        GameObject gunnerObj = Instantiate(_gunnerPrefab, _gunner.transform.position, Quaternion.identity);

        Destroy(_gunner.gameObject); // Verwijder de originele speler
         //DontDestroyOnLoad(gunnerObj);
        
         // Zorg ervoor dat de gunner blijft bestaan
        Debug.Log($"Player {_gunner.PlayerNumber} is de gunner!");
    }

    // Sla de renners op (alle spelers behalve de gunner)
    private void SaveRunners()
    {
        Runners.Clear();
        foreach (PlayerController player in Players)
        {
            if (player != _gunner)
            {
                Runners.Add(player);
            }
        }
        Debug.Log("Runners zijn opgeslagen!");
    }
}
