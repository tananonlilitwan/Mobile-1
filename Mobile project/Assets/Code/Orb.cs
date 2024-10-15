using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : MonoBehaviour
{
    private PlayerHP playerHP;

    void Awake()
    {
        // Get the PlayerHP component from the player GameObject
        playerHP = GetComponent<PlayerHP>();
    }

    private void OnTriggerEnter2D(Collider2D pCollider2D)
    {
        // Check if the collider is the player by verifying if it has the PlayerHP component
        PlayerHP player = pCollider2D.GetComponent<PlayerHP>();
        if (player != null)
        {
            // Heal the player
            player.Heal(10f);

            // Destroy the orb after healing the player
            Destroy(gameObject);
        }
    }
}