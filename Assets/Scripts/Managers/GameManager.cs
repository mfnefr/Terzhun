using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get; private set;}

    private void Awake()
    {
        Instance = this;
    }

    public bool IsGameActive()
    {
        if(Mouse.current == null || DialogueManager.Instance.dialogueIsPlaying || CombatManager.Instance.combatIsActive)
        {
            return false;
        }
        return true;
    }
}