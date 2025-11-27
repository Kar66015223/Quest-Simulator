using UnityEngine;

public class EscortQuest : Quest
{
    public EscortQuest()
    {
        this.questName = "ช่วยเหลือเต่า";
        this.questDesc = "ฉันพลัดหลงกับเต่าสัตว์เลี้ยงของฉันระหว่างทางมาที่นี่ ช่วยออกไปตามหามันกลับมาที";
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
