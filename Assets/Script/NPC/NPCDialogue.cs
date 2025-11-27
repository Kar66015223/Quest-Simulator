
using UnityEngine;

public class NPCDialogue : MonoBehaviour, IInteractable
{
    public Dialogue dialogue;
    public DialogueManager dialogueManager;

    [SerializeField]
    private bool _isInteractable = true;

    public bool isInteractable
    {
        get { return _isInteractable; }
        set { _isInteractable = value; }
    }

    // --- แก้ไข Interact() ---
    public void Interact(Player player)
    {
        if (!isInteractable) return;

        // --- แก้ไข: ซ่อน Prompt ผ่าน UIManager ---
        UIManager.Instance.SetPromptActive(false);
        // --- จบส่วนที่แก้ไข ---

        if (dialogueManager == null)
            dialogueManager = FindObjectOfType<DialogueManager>();

        if (dialogueManager != null)
            dialogueManager.StartDialogue(dialogue);
        else
            Debug.LogError("DialogueManager not found in scene!");
    }

    // --- แก้ไข OnTriggerEnter ---
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.SetInteractable(this);

                // --- แก้ไข: เปิด Prompt ผ่าน UIManager ---
                UIManager.Instance.SetPromptActive(true, this.transform);
            }
        }
    }

    // --- แก้ไข OnTriggerExit ---
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.ClearInteractable(this);

                // --- แก้ไข: เปิด Prompt ผ่าน UIManager ---
                UIManager.Instance.SetPromptActive(false);
            }
        }
    }
    

}