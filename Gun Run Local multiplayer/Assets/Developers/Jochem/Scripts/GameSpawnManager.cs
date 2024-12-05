using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSpawnManager : MonoBehaviour
{
    [SerializeField] public Transform[] spawnPoints; // Spawnpunten in deze scene

    public Transform GetSpawnPoint(int index)
    {
        if (index >= 0 && index < spawnPoints.Length)
        {
            return spawnPoints[index];
        }
        Debug.LogWarning("Geen geldig spawnpoint beschikbaar.");
        return null;
    }
}
