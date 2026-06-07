using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Entity
{
    [Header("Object References")]
    public InventoryManager inventoryManager;

    [Header("Player Settings")]
    private Vector3 targetPos;
    public bool attackedThisTurn = false;
    private bool isMoving = false;
    private Rigidbody2D rb;

    void Start()
    {
        maxHealth = 30;
        health = maxHealth;
        armorClass = 15;
        moveSpeed = 3f;

        rb = GetComponent<Rigidbody2D>();

        UpdateHealthBar();

        attacks.Add(new Attack("Sword", "1d8", DamageType.Slashing));
    }

    void Update()
    {
        if(!GameManager.Instance.IsGameActive() || inventoryManager.isInventoryOpen) return;

        // pohyb hráče
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
    }

    void FixedUpdate()
    {
        if (isMoving)
        {
            Vector2 newPos = Vector2.MoveTowards(rb.position, targetPos, moveSpeed * Time.fixedDeltaTime);
            rb.MovePosition(newPos);
        }
    }
}