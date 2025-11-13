// Dialogue.cs (ฉบับอัปเกรด)
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public string npcName;
    public Sprite characterPortrait; // << เพิ่มบรรทัดนี้ สำหรับรูปตัวละคร

    [TextArea(3, 10)]
    public string[] sentences;
}