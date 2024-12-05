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
        GameObject playerObj = Instantiate(PlayerPrefabs[Players.Count], spawnPoint.position, Quaternion.Euler(-90, spawnPoint.rotation.eulerAngles.y, spawnPoint.rotation.eulerAngles.z));
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

        // Voeg event toe voor spelersverplaatsing na scene load
        Debug.Log("Laad de game scene...");
        SceneManager.LoadScene("Game Scene final");
        Debug.Log("Het spel is gestart!");

        StartCoroutine(LoadGameSceneWithDelay());
    }

    private IEnumerator LoadGameSceneWithDelay()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Game Scene final");
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        Debug.Log("Game scene volledig geladen. Spelers verplaatsen...");

        // Spelers verplaatsen nadat de scene volledig is geladen
        AssignNewSpawnPoints();
    }
    private void AssignRandomGunner()
    {
        int randomIndex = Random.Range(0, Players.Count);
        _gunner = Players[randomIndex];
        _gunner.AssignGunner(); // Zet de speler in de "gunner"-rol


        // Vervang de speler door een speciale gunner prefab
        GameObject gunnerObj = Instantiate(_gunnerPrefab, _gunner.transform.position, Quaternion.identity);

        Destroy(_gunner.gameObject); // Verwijder de originele speler
        DontDestroyOnLoad(gunnerObj);

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

    private void AssignNewSpawnPoints()
    {
        GameSpawnManager spawnManager = FindObjectOfType<GameSpawnManager>();
        if (spawnManager == null)
        {
            Debug.LogError("Geen SpawnManager gevonden in de game scene.");
            return;
        }

        for (int i = 0; i < Players.Count; i++)
        {
            Transform spawnPoint = spawnManager.GetSpawnPoint(i);
            if (spawnPoint != null)
            {
                Debug.Log($"Verplaats Speler {i + 1} naar SpawnPoint {spawnPoint.position}");

                // Verplaats speler
                Players[i].transform.position = spawnPoint.position;
                Players[i].transform.rotation = spawnPoint.rotation;

                // Controleer of de speler actief is
                if (!Players[i].gameObject.activeSelf)
                {
                    Players[i].gameObject.SetActive(true);
                }
            }
            else
            {
                Debug.LogWarning($"Geen spawnpoint beschikbaar voor speler {i + 1}.");
            }
        }
        Debug.Log("Spelers succesvol verplaatst naar de nieuwe spawnpoints.");
    }


    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"Scene {scene.name} is geladen.");

        if (scene.name == "Game Scene final")
        {
            GameSpawnManager spawnManager = FindObjectOfType<GameSpawnManager>();
            if (spawnManager != null)
            {
                for (int i = 0; i < Players.Count; i++)
                {
                    Debug.Log($"Verplaats speler {i + 1} naar {spawnManager.GameSceneSpawnPoints[i].position}");
                    Players[i].transform.position = spawnManager.GameSceneSpawnPoints[i].position;
                    Players[i].transform.rotation = spawnManager.GameSceneSpawnPoints[i].rotation;
                }
            }
            else
            {
                Debug.LogError("GameSpawnManager niet gevonden in de scene.");
            }


            foreach (var player in GameObject.FindGameObjectsWithTag("Player"))
            {
                Destroy(player);
            }
        }
    }


}
