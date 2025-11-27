using UnityEngine;

public class SurviveQuest : Quest
{
    public SurviveQuest()
    {
        this.questName = "เอาชีวิตรอด";
        this.questDesc = "มีฝูงสไลม์ขนาดใหญ่กำลังบุกมาที่หมู่บ้าน จงออกไปหยุดมันให้นานที่สุดเท่าที่ทำได้";
        this.questType = QuestType.MainQuest;
        this.questStatus = QuestStatus.Pending;
        this.questID = 4;
    }
    public override void CheckCompletion()
    {
        Debug.Log($"{questName} CheckCompletion is running");
    }

    public override void OnAccept()
    {
        Debug.Log($"{questName} OnAccept is running");
    }

    public override void QuestCompleted()
    {
        Debug.Log($"{questName} completed!");
    }

    public override void QuestFailed()
    {
        Debug.Log($"{questName} failed!");
    }
}
