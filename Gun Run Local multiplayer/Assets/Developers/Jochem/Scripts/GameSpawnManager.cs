using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSpawnManager : MonoBehaviour
{
    [SerializeField] public Transform[] GameSceneSpawnPoints;

    public Transform GetSpawnPoint(int index)
    {
        if (index >= 0 && index < GameSceneSpawnPoints.Length)
        {
            return GameSceneSpawnPoints[index];
        }
        Debug.LogWarning("Geen geldig spawnpoint beschikbaar.");
        return null;
    }
}
