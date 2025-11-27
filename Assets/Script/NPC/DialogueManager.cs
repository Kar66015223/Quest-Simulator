using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject dialoguePanel;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public Button nextButton;

    public Button previousButton;
    public Image portraitImage;

    
    [Header("Player Control")]
    public Player playerController; 

    private List<string> currentSentences;
    private int currentSentenceIndex;
    private Sprite currentPortrait;

    [Header("Quest Elements")]
    public Button acceptQuestButton;
    public Button completeQuestButton;
    public GameObject questUI;

    void Start()
    {
        currentSentences = new List<string>();
        dialoguePanel.SetActive(false);

        // --- ตั้งค่าปุ่ม ---
        if (nextButton != null)
        {
            nextButton.onClick.AddListener(DisplayNextSentence);
        }
        if (previousButton != null)
        {
            previousButton.onClick.AddListener(DisplayPreviousSentence);
        }
        // ผูกปุ่ม "รับเควส"
        if (acceptQuestButton != null)
        {
            acceptQuestButton.onClick.AddListener(AcceptQuest);
            acceptQuestButton.gameObject.SetActive(false); // ซ่อนไว้ตอนเริ่ม
        }
    }

    // เมธอดนี้ถูกเรียกโดย NPCDialogue.cs
    public void StartDialogue(Dialogue dialogue)
    {
        dialoguePanel.SetActive(true);
        nameText.text = dialogue.npcName;
        currentPortrait = dialogue.characterPortrait;

        currentSentences.Clear();
        currentSentences.AddRange(dialogue.sentences);

        currentSentenceIndex = 0;
        DisplayCurrentSentence();

        // --- ส่วนที่ 1: แสดงปุ่ม "รับเควส" ทันทีที่บทสนทนาเริ่ม ---
        if (acceptQuestButton != null)
        {
            // ตรวจสอบให้แน่ใจว่าปุ่ม Next/Previous แสดงอยู่
            if (nextButton != null) nextButton.gameObject.SetActive(true);

            acceptQuestButton.gameObject.SetActive(true);
        }
        // --- ส่วนที่เพิ่ม: ล็อกการเคลื่อนที่ของผู้เล่น ---
        if (playerController == null)
        {
            // *** เปลี่ยนจาก PlayerController เป็น Player ***
            playerController = FindObjectOfType<Player>();
        }

        if (playerController != null)
        {
            // สั่งให้ Player หยุดรับ Input จากคีย์บอร์ด
            playerController.canReceiveInput = false; // <-- **แก้ไขตรงนี้**
        }

        if (QuestManager.Instance.selectedQuest != null)
        {
            if (QuestManager.Instance.selectedQuest.questStatus == Quest.QuestStatus.Completed || QuestManager.Instance.selectedQuest.questStatus == Quest.QuestStatus.Failed)
            {
                completeQuestButton.gameObject.SetActive(true);
                acceptQuestButton.gameObject.SetActive(false);
            } 
        }
    }

    // เมธอดสำหรับแสดงประโยค ณ index ปัจจุบัน
    private void DisplayCurrentSentence()
    {
        // แสดงข้อความ
        dialogueText.text = currentSentences[currentSentenceIndex];

        // แสดงรูปตัวละคร (ถ้ามี)
        // ... (โค้ดแสดงรูปเดิม) ...
        if (portraitImage != null)
        {
            if (currentPortrait != null)
            {
                portraitImage.sprite = currentPortrait;
                portraitImage.gameObject.SetActive(true);
            }
            else
            {
                portraitImage.gameObject.SetActive(false);
            }
        }

        // อัปเดตปุ่ม
        UpdateButtons();
    }

    // --- เมธอดใหม่สำหรับปุ่ม "ย้อนกลับ" ---
    public void DisplayPreviousSentence()
    {
        // ถ้ายังไม่ถึงประโยคแรก (index > 0)
        if (currentSentenceIndex > 0)
        {
            currentSentenceIndex--; // ลด index
            DisplayCurrentSentence();
        }
    }

    // --- ส่วนที่ 2: แก้ไขเมธอด "ถัดไป" ให้เน้นแค่การคุยต่อ/จบการคุย ---
    public void DisplayNextSentence()
    {
        // ถ้ายังมีประโยคถัดไป (ยังไม่ถึงประโยคสุดท้าย)
        if (currentSentenceIndex < currentSentences.Count - 1)
        {
            currentSentenceIndex++; // เพิ่ม index
            DisplayCurrentSentence();
        }
        else
        {
            // ถ้าถึงประโยคสุดท้ายแล้ว ให้ปิดกล่องบทสนทนา (ผู้เล่นมีปุ่มรับเควสแยกอยู่แล้ว)
            EndDialogue();
        }
    }

    // --- เมธอดใหม่สำหรับซ่อน/แสดงปุ่ม ---
    private void UpdateButtons()
    {
        // ซ่อนปุ่ม "ย้อนกลับ" ถ้าอยู่ประโยคแรกสุด
        if (previousButton != null)
        {
            previousButton.gameObject.SetActive(currentSentenceIndex > 0);
        }

        // *** Optional: หากต้องการเปลี่ยนปุ่ม "Next" เป็น "Close" เมื่อถึงหน้าสุดท้าย ***
        TextMeshProUGUI nextButtonText = nextButton.GetComponentInChildren<TextMeshProUGUI>();
        if (currentSentenceIndex == currentSentences.Count - 1)
        {
            if (nextButtonText != null) nextButtonText.text = "Close";
        }
        else
        {
            if (nextButtonText != null) nextButtonText.text = "Next";
        }
    }

    // --- เมธอด: เมื่อกดปุ่ม "รับเควส" ---
    public void AcceptQuest()
    {
        if (questUI != null)
        {
            questUI.SetActive(true); // แสดง UI เควส
        }
        EndDialogue(); // ปิดกล่องบทสนทนาและซ่อนปุ่มทั้งหมด
    }

    // --- ส่วนที่ 3: แก้ไขเมธอด EndDialogue() ให้ซ่อนทุกอย่าง ---
    public void EndDialogue()
    {
        dialoguePanel.SetActive(false);

        // ซ่อนปุ่มทั้งหมดที่เกี่ยวข้องกับการสนทนา
        if (acceptQuestButton != null)
        {
            acceptQuestButton.gameObject.SetActive(false);
        }
        if (nextButton != null)
        {   
            // อาจจะซ่อน Next Button ด้วยเพื่อความแน่ใจ
            nextButton.gameObject.SetActive(false);
        }
        if (previousButton != null)
        {
            previousButton.gameObject.SetActive(false);
        }
        // --- ส่วนที่เพิ่ม: ปลดล็อกการเคลื่อนที่ของผู้เล่น ---
        if (playerController == null)
        {
            // *** เปลี่ยนจาก PlayerController เป็น Player ***
            playerController = FindObjectOfType<Player>();
        }
        if (playerController != null)
        {
            // สั่งให้ Player กลับมารับ Input จากคีย์บอร์ดได้
            playerController.canReceiveInput = true; // <-- **แก้ไขตรงนี้**
        }

        Debug.Log("End of conversation.");
    }
}