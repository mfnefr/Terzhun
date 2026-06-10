using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Entity
{
    [Header("Animation")]
    private Animator anim;
    [Header("Player Settings")]
    private Vector3 targetPos;
    public bool attackedThisTurn = false;

    void Start()
    {
        anim = GetComponent<Animator>();

        isMoving = false;
        maxHealth = 30;
        health = maxHealth;
        armorClass = 15;
        moveSpeed = 3f;

        UpdateHealthBar();

        attacks.Add(new Attack("Sword", "1d8", DamageType.Slashing));
    }

    void Update()
    {
        if (!GameManager.Instance.IsGameActive() || InventoryManager.Instance.isInventoryOpen)
        {
            if (isMoving)
            {
                isMoving = false;
                anim.SetBool("isMoving", false);
            }
            return;
        }

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector2 mousePos = Mouse.current.position.ReadValue();
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10f));
            targetPos = worldPos;
            isMoving = true;
        }

        if (isMoving && Vector2.Distance(transform.position, targetPos) < 0.1f)
        {
            isMoving = false;
        }

        if (isMoving)
        {
            Vector2 direction = (targetPos - transform.position).normalized;
            anim.SetFloat("MoveX", direction.x);
            anim.SetFloat("MoveY", direction.y);
        }

        anim.SetBool("isMoving", isMoving);
    }

    void FixedUpdate()
    {
        if (!GameManager.Instance.IsGameActive() || InventoryManager.Instance.isInventoryOpen) return;

        if (isMoving)
        {
            Vector2 newPos = Vector2.MoveTowards(rb.position, targetPos, moveSpeed * Time.fixedDeltaTime);
            rb.MovePosition(newPos);
        }
    }
}