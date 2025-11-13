// NPCDialogue.cs (ฉบับแก้ไข: เพิ่ม Prompt เข้ามา)
using UnityEngine;

public class NPCDialogue : MonoBehaviour, IInteractable
{
    public Dialogue dialogue;
    public DialogueManager dialogueManager;

    // --- เพิ่มบรรทัดนี้ ---
    public GameObject interactPrompt; // << ลาก UI "กด E" มาใส่ที่นี่

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

        // --- เพิ่มคำสั่งซ่อน Prompt เมื่อเริ่มคุย ---
        if (interactPrompt != null)
            interactPrompt.SetActive(false);
        // --- จบส่วนที่เพิ่ม ---

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

                // --- เพิ่ม Logic เปิด Prompt ---
                if (interactPrompt != null)
                    interactPrompt.SetActive(true);
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

                // --- เพิ่ม Logic ปิด Prompt ---
                if (interactPrompt != null)
                    interactPrompt.SetActive(false);
            }
        }
    }
}