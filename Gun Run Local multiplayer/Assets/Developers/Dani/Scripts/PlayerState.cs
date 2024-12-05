using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerStates
{
    alive,
    dead
}

public class PlayerState : MonoBehaviour
{
    public PlayerStates state = PlayerStates.alive;
    public bool invulnerable;
    [SerializeField][Range(0, 10)] private float _invulnerableDuration = 5f;
    private Coroutine _invulIEnumerator;

    public void Damage()
    {
        if (!invulnerable && state != PlayerStates.dead)
        {
            GetComponent<PlayerController>().Death();
            Debug.Log(gameObject.name + " Killed");
            state = PlayerStates.dead;
        }
    }

    public void RecieveInvulnerability() // refreshes invulnerability duration if given again
    {
        if (_invulIEnumerator != null)
        {
            StopCoroutine(_invulIEnumerator);
        }
        _invulIEnumerator = StartCoroutine(InvulnerableToggle());
    }

    private IEnumerator InvulnerableToggle()
    {
        invulnerable = true;
        yield return new WaitForSeconds(_invulnerableDuration);
        invulnerable = false;
    }

    public void LoseInvulnerability()
    {
        if (_invulIEnumerator != null)
        {
            StopCoroutine(_invulIEnumerator);
        }
        invulnerable = false;
    }
}
