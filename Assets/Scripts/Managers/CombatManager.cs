using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatManager : MonoBehaviour
{
    private enum CombatState
    {
        Start,
        PlayerTurn,
        EnemyTurn,
        Won,
        Lost
    }

    [Header("Object References")]
    public static CombatManager Instance {get; private set;}
    public CameraController cameraController;

    [Header("Combat UI")]
    public GameObject combatPanel;
    public Button attackButton;

    [Header("Combatants")]
    public Player player;
    public Enemy currentEnemy;

    [Header("Combat Settings")]
    public bool combatIsActive;
    private List<GameObject> turnOrder = new List<GameObject>();
    private CombatState currentState;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        currentState = CombatState.Start;
        combatIsActive = false;
        combatPanel.SetActive(false);
        attackButton.interactable = false;
    }

    private void MakeTurnOrder()
    {
        turnOrder.Clear();

        int playerIniciative = player.RollIniciative();
        int enemyIniciative = currentEnemy.RollIniciative();

        if(playerIniciative >= enemyIniciative)
        {
            turnOrder.Add(player.gameObject);
            turnOrder.Add(currentEnemy.gameObject);
        }
        else
        {
            turnOrder.Add(currentEnemy.gameObject);
            turnOrder.Add(player.gameObject);
        }

        Debug.Log("Enemy iniciative: " + enemyIniciative);
        Debug.Log("Player iniciative: " + playerIniciative);
    }

    private void StartCombat()
    {
        if(turnOrder[0] == player.gameObject)
        {
            currentState = CombatState.PlayerTurn;
            PlayerTurn();
        }
        else
        {
            currentState = CombatState.EnemyTurn;
            StartCoroutine(EnemyTurn());
        }
    }

    private void PlayerTurn()
    {
        attackButton.interactable = true;
    }

    public void OnAttackButton()
    {
        if(currentState != CombatState.PlayerTurn || !combatIsActive) return;

        attackButton.interactable = false;

        MakeTurn(player, currentEnemy);

        if(CheckForEndCombat()) return;

        currentState = CombatState.EnemyTurn;
        StartCoroutine(EnemyTurn());
    }

    private IEnumerator EnemyTurn()
    {
        yield return new WaitForSeconds(1.5f);

        if(!combatIsActive) yield break;

        MakeTurn(currentEnemy, player);

        if(CheckForEndCombat()) yield break;

        currentState = CombatState.PlayerTurn;
        PlayerTurn();
    }

    private void MakeTurn(Entity attacker, Entity defender)
    {
        int damage = attacker.RollDamage();
        defender.health -= damage;
        defender.UpdateHealthBar();
    }

    private bool CheckForEndCombat()
    {
        if(currentEnemy.health <= 0)
        {
            currentState = CombatState.Won;
            Destroy(currentEnemy.gameObject);
            ExitCombatMode();
            return true;
        }
        else if(player.health <= 0)
        {
            currentState = CombatState.Lost;
            Destroy(player.gameObject);
            ExitCombatMode();
            return true;
        }
        return false;
    }

    public void EnterCombatMode()
    {
        combatIsActive = true;
        combatPanel.SetActive(true);

        player.transform.position = player.transform.position;
        cameraController.ZoomOut();

        MakeTurnOrder();
        StartCombat();
    }

    public void ExitCombatMode()
    {
        combatIsActive = false;
        combatPanel.SetActive(false);
        cameraController.ZoomIn();
    }
}