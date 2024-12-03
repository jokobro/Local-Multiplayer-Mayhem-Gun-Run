using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupPlate : MonoBehaviour
{
    private List<GameObject> _players;
    private int _runnersAlive;



    private void Awake()
    {
        _players = new List<GameObject>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerState>() != null && !_players.Contains(other.gameObject))
        {
            _players.Add(other.gameObject);
            CheckAvailability();
        }
    }

    private void CheckAvailability()
    {
        if (_players.Count >= _runnersAlive)
        {
            // give powerup to runner using _players(_players.Count);
        }
    }

    void OnRunnerDeath()
    {
        CheckAvailability();
    }
}
