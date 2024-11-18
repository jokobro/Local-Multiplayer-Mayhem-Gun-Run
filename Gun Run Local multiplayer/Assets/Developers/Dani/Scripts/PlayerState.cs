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
    Coroutine invulIEnumerator;

    public void Damage()
    {
        if (!invulnerable)
        {
            Debug.Log(gameObject.name + " Killed");
        }
    }

    public void RecieveInvulnerability(float invulnerableTime)
    {
        if (invulIEnumerator != null)
        {
            StopCoroutine(invulIEnumerator);
        }
        invulIEnumerator = StartCoroutine(InvulnerableToggle(invulnerableTime));
    }

    private IEnumerator InvulnerableToggle(float duration)
    {
        invulnerable = true;
        yield return new WaitForSeconds(duration);
        invulnerable = false;
    }
}
