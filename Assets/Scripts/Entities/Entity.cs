using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Entity : MonoBehaviour
{
    [Header("Static Values")]
    public static string INICIATIVE_DIE = "1d20";
    public RollManager rollManager;

    [Header("Health Bar")]
    public Image healthBarFill;
    // public TextMeshProUGUI healthText;

    [Header("Stats")]
    public int maxHealth;
    public int health;
    public int armorClass;
    public float moveSpeed;
    public List<Attack> attacks = new List<Attack>();

    public int RollIniciative()
    {
        return rollManager.RollDice(INICIATIVE_DIE);
    }

    public int RollDamage()
    {
        Attack attack = attacks[Random.Range(0, attacks.Count)];

        return rollManager.RollDice(attack.diceThrow);
    }

    public void UpdateHealthBar()
    {
        float healthPercentage = (float)health/maxHealth; 

        healthBarFill.fillAmount = healthPercentage;
        // healthText.text = health + "/" + maxHealth;
    }
}