using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Entity
{
    [Header("Object References")]
    public InventoryManager inventoryManager;

    [Header("Player Stettings")]
    private Vector3 targetPos;
    public bool attackedThisTurn = false;
    private bool isMoving = false;

    void Start()
    {
        maxHealth = 30;
        health = maxHealth;
        armorClass = 15;
        moveSpeed = 3f;

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

        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed*Time.deltaTime);
            if(Vector3.Distance(transform.position, targetPos) < 0.1f)
            {
                isMoving = false;
            }
        }
    }
}