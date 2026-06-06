using UnityEngine;
using UnityEngine.InputSystem;

public class NPC : MonoBehaviour
{
    [SerializeField] private TextAsset textAsset;
    private GameObject visualBox;
    private bool isPlayerInRange = false;
    
    void Start()
    {
        // načtení ikonky pro dialog
        GameObject prefab = Resources.Load<GameObject>("InteractHint");

        visualBox = Instantiate(prefab, transform.position + Vector3.up, Quaternion.identity);
        visualBox.transform.SetParent(transform);
        visualBox.SetActive(false);

    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if(context.started && isPlayerInRange && !DialogueManager.Instance.dialogueIsPlaying)
        {
            DialogueManager.Instance.EnterDialogueMode(textAsset);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = true;
            visualBox.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = false;
            visualBox.SetActive(false);
            DialogueManager.Instance.ExitDialogueMode();
        }
    }
}
