// DialogueManager.cs (ฉบับอัปเกรด)
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

    // --- เพิ่ม 2 บรรทัดนี้ ---
    public Button previousButton; // < ลากปุ่ม "ย้อนกลับ" มาใส่
    public Image portraitImage; // < (Optional) ลาก Image รูปตัวละครมาใส่

    // --- เปลี่ยนจาก Queue เป็น List ---
    private List<string> currentSentences;
    private int currentSentenceIndex;
    private Sprite currentPortrait;

    void Start()
    {
        // --- เปลี่ยนจาก Queue เป็น List ---
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
    }

    // เมธอดนี้ถูกเรียกโดย NPCDialogue.cs
    public void StartDialogue(Dialogue dialogue)
    {
        dialoguePanel.SetActive(true);
        nameText.text = dialogue.npcName;
        currentPortrait = dialogue.characterPortrait; // เก็บรูป

        // --- เคลียร์ List และเติมข้อมูลใหม่ ---
        currentSentences.Clear();
        currentSentences.AddRange(dialogue.sentences); // เพิ่มประโยคทั้งหมด

        // --- เริ่มที่ประโยคแรก ---
        currentSentenceIndex = 0;
        DisplayCurrentSentence();
    }

    // เมธอดสำหรับแสดงประโยค ณ index ปัจจุบัน
    private void DisplayCurrentSentence()
    {
        // แสดงข้อความ
        dialogueText.text = currentSentences[currentSentenceIndex];

        // แสดงรูปตัวละคร (ถ้ามี)
        if (portraitImage != null)
        {
            if (currentPortrait != null)
            {
                portraitImage.sprite = currentPortrait;
                portraitImage.gameObject.SetActive(true);
            }
            else
            {
                // ถ้า NPC นี้ไม่มีรูป ก็ซ่อนกรอบ Image ไปเลย
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

    // --- แก้ไขเมธอด "ถัดไป" ---
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
            // ถ้าถึงประโยคสุดท้ายแล้วกด "Next" = จบการสนทนา
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

        // (Optional) ถ้าอยากให้ปุ่ม "ถัดไป" เปลี่ยนเป็น "ปิด" ในหน้าสุดท้าย
        // TextMeshProUGUI nextButtonText = nextButton.GetComponentInChildren<TextMeshProUGUI>();
        // if (currentSentenceIndex == currentSentences.Count - 1)
        // {
        //     nextButtonText.text = "Close";
        // }
        // else
        // {
        //     nextButtonText.text = "Next";
        // }
    }

    void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        Debug.Log("End of conversation.");
    }
}