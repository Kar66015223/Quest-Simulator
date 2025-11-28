
using TMPro;
using UnityEngine;

[System.Serializable]
public class Dialogue
{

    public string npcName;
    public Sprite characterPortrait; // << เพิ่มบรรทัดนี้ สำหรับรูปตัวละคร
    public TMP_Text nameText;    // ช่องสำหรับชื่อ NPC
    private Transform mainCameraTransform;

    [TextArea(3, 10)]
    public string[] sentences;


    void Start()
    {
        // ค้นหา Transform ของกล้องหลักเมื่อเริ่ม
        if (Camera.main != null)
        {
            mainCameraTransform = Camera.main.transform;
        }
    }

      void LateUpdate()
     {
        if (mainCameraTransform != null)
        {
            // 1. ทำให้ชื่อ NPC หันเข้าหากล้อง
            if (nameText != null)
            {
                nameText.transform.rotation = Quaternion.LookRotation(nameText.transform.position - mainCameraTransform.position);
            }

        }
     }
    }