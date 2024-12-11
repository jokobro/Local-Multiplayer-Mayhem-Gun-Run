using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] PlayerPrefabs; // Prefabs van de spelers.
    [SerializeField] private GameObject _gunnerPrefab; // prefab voor de gunner.
    [SerializeField] private Transform[] SpawnPoints; // Spawnlocaties voor spelers.
    public List<PlayerController> Players = new List<PlayerController>(); // Lijst met alle spelers in de game.
    public List<PlayerController> Runners = new List<PlayerController>(); // Lijst met alle "runners" (spelers behalve de gunner).
    public int MaxPlayers = 4; // Maximale toegestane spelers.
    private PlayerController _gunner; // Verwijzing naar de speler die als gunner is aangewezen.

    // Voeg een nieuwe speler toe aan de game.
    public void AddPlayer()
    {
        // Controleer of het maximale aantal spelers is bereikt.
        if (Players.Count >= MaxPlayers)
        {
            Debug.Log("Max aantal spelers bereikt!");
            return;
        }

        // Controleer of er voldoende unieke player-prefabs zijn.
        if (Players.Count >= PlayerPrefabs.Length)
        {
            Debug.LogError("Niet genoeg unieke prefabs beschikbaar voor spelers.");
            return;
        }

        // Bepaal een uniek PlayerNumber en spawnlocatie voor de nieuwe speler.
        int playerNumber = Players.Count + 1;
        Transform spawnPoint = SpawnPoints[Players.Count];

        // Instantieer het spelerobject op de spawnlocatie.
        GameObject playerObj = Instantiate(
            PlayerPrefabs[Players.Count],
            spawnPoint.position,
            Quaternion.Euler(-90, spawnPoint.rotation.eulerAngles.y, spawnPoint.rotation.eulerAngles.z)
        );

        // Configureer de PlayerController-component.
        PlayerController playerController = playerObj.GetComponent<PlayerController>();
        playerController.PlayerNumber = playerNumber;
        DontDestroyOnLoad(playerObj); // Zorg ervoor dat het spelerobject niet wordt vernietigd bij scene-overgangen.

        // Voeg de speler toe aan de lijst van spelers.
        Players.Add(playerController);
        Debug.Log($"Speler {playerNumber} is gejoint!");
    }

    // Start het spel.
    public void StartGame()
    {
        // Controleer of er voldoende spelers zijn om het spel te starten.
        if (Players.Count < 2)
        {
            Debug.LogError("Niet genoeg spelers om het spel te starten. Minimaal 2 spelers nodig.");
            return;
        }

        AssignRandomGunner(); // Wijs willekeurig een speler aan als gunner.
        

        Debug.Log("Laad de game scene...");
        SceneManager.LoadScene("Game Scene final"); // Laad de gamescene.
        Debug.Log("Het spel is gestart!");

        StartCoroutine(LoadGameSceneWithDelay()); // Start de coroutine voor het laden van de scene.
    }

    // Coroutine om de game scene met een delay volledig te laden.
    private IEnumerator LoadGameSceneWithDelay()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Game Scene final");
        while (!asyncLoad.isDone)
        {
            yield return null; // Wacht tot de scene volledig is geladen.
        }

        Debug.Log("Game scene volledig geladen. Spelers verplaatsen...");
        AssignNewSpawnPoints(); // Verplaats de spelers naar nieuwe spawnpoints.
    }

    // Wijs willekeurig een speler aan als gunner.
    private void AssignRandomGunner()
    {
        int randomIndex = Random.Range(0, Players.Count); // Kies een willekeurige index.
        _gunner = Players[randomIndex];
        _gunner.AssignGunner(); // Zet de speler in de "gunner"-rol.

        // Vervang de speler door een speciale gunner prefab.
        GameObject gunnerObj = Instantiate(_gunnerPrefab, _gunner.transform.position, Quaternion.identity);

        Destroy(_gunner.gameObject); // Verwijder het originele spelerobject.
        DontDestroyOnLoad(gunnerObj); // Zorg ervoor dat de gunner niet wordt vernietigd bij scene-overgangen.

        Debug.Log($"Player {_gunner.PlayerNumber} is de gunner!");
    }

    // Sla de runners (spelers behalve de gunner) op.
    private void SaveRunners()
    {
        Runners.Clear(); // Maak de lijst leeg.
        foreach (PlayerController player in Players)
        {
            if (player != _gunner)
            {
                Runners.Add(player); // Voeg spelers toe die niet de gunner zijn.
            }
        }
        Debug.Log("Runners zijn opgeslagen!");
    }

    // Verplaats spelers naar nieuwe spawnlocaties.
    private void AssignNewSpawnPoints()
    {
        GameSpawnManager spawnManager = FindObjectOfType<GameSpawnManager>(); // Zoek de GameSpawnManager in de scene.
        if (spawnManager == null)
        {
            Debug.LogError("Geen SpawnManager gevonden in de game scene.");
            return;
        }

        for (int i = 0; i < Players.Count; i++)
        {
            Transform spawnPoint = spawnManager.GetSpawnPoint(i); // Haal de spawnlocatie op.
            if (spawnPoint != null)
            {
                Debug.Log($"Verplaats Speler {i + 1} naar SpawnPoint {spawnPoint.position}");

                // Verplaats speler naar de nieuwe spawnlocatie.
                Players[i].transform.position = spawnPoint.position;
                Players[i].transform.rotation = spawnPoint.rotation;

                // Zorg ervoor dat de speler actief is.
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

    // Callback voor wanneer een scene is geladen.
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

            // Verwijder alle spelers die al aanwezig zijn in de nieuwe scene (dubbele objecten).
            foreach (var player in GameObject.FindGameObjectsWithTag("Player"))
            {
                Destroy(player);
            }
        }
    }
}
