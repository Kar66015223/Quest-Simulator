using UnityEngine;

public class FindQuest : Quest
{
    public FindQuest()
    {
        this.questName = "ตามหาเพชร";
        this.questDesc = "ฉันทำเพชรอันล้ำค่าหายระหว่างทางมาที่นี่ ช่วยออกไปตามหามันที";
        this.questType = QuestType.SideQuest;
        this.questStatus = QuestStatus.Pending;
    }
    public override void CheckCompletion()
    {
        throw new System.NotImplementedException();
    }

    public override void OnAccept()
    {
        throw new System.NotImplementedException();
    }

    public override void QuestCompleted()
    {
        throw new System.NotImplementedException();
    }

    public override void QuestFailed()
    {
        throw new System.NotImplementedException();
    }
}
