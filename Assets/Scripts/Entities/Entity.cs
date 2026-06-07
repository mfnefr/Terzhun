using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class Entity : MonoBehaviour
{
    private Vector2 startPosition;
    public Rigidbody2D rb;
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
    public float maxMoveDistance;
    public float moveSpeed;
    public List<Attack> attacks = new List<Attack>();

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = rb.position;
    }

    public IEnumerator AttackSequence(Entity target, System.Action onSequenceComplete)
    {
        Vector2 targetPos = (Vector2)target.transform.position + ((rb.position - (Vector2)target.transform.position).normalized * 1.5f);

        while (Vector2.Distance(rb.position, targetPos) > 0.5f)
        {
            Vector2 newPos = Vector2.MoveTowards(rb.position, targetPos, moveSpeed * Time.fixedDeltaTime);
            rb.MovePosition(newPos);

            yield return new WaitForFixedUpdate(); 
        }

        target.health -= RollDamage();
        target.UpdateHealthBar();

        yield return new WaitForSeconds(0.5f);

        onSequenceComplete?.Invoke();
    }

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

    public void UsePotion()
    {
        health += rollManager.RollDice("2d4") + 2;

        if(health > maxHealth) health = maxHealth;

        UpdateHealthBar();
    }
}