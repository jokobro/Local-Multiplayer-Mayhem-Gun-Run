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
    [SerializeField][Range(0, 10)] private float invulnerableDuration = 5f;
    Coroutine invulIEnumerator;

    public void Damage()
    {
        if (!invulnerable)
        {
            Debug.Log(gameObject.name + " Killed");
        }
    }

    public void RecieveInvulnerability() // refreshes invulnerability duration if given once again
    {
        if (invulIEnumerator != null)
        {
            StopCoroutine(invulIEnumerator);
        }
        invulIEnumerator = StartCoroutine(InvulnerableToggle());
    }

    private IEnumerator InvulnerableToggle()
    {
        invulnerable = true;
        yield return new WaitForSeconds(invulnerableDuration);
        invulnerable = false;
    }

    public void LoseInvulnerability()
    {
        if (invulIEnumerator != null)
        {
            StopCoroutine(invulIEnumerator);
        }
        invulnerable = false;
    }
}
