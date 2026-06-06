using UnityEngine;
// using TMPro;

public class Enemy : Entity
{
    private bool isPlayerInRange = false;

    void Start()
    {
        maxHealth = 20;
        health = maxHealth;
        armorClass = 10;

        UpdateHealthBar();

        attacks.Add(new Attack("Bite", "1d4", DamageType.Piercing));
        attacks.Add(new Attack("Claw", "1d6", DamageType.Slashing));
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = true;
            CombatManager.Instance.currentEnemy = this;
            CombatManager.Instance.EnterCombatMode();
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }
}