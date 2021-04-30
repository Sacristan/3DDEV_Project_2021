using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    int health = 100;

    public void ReceiveDamage(int damage)
    {
        health = Mathf.Clamp(health - damage, 0, 100);
        Debug.Log($"Received damage: {damage} currentHealth: {health}");
        if (health <= 0) Die();
    }

    private void Die()
    {
        Debug.Log("Player DEAD");
    }
}
