using UnityEngine;

public class BossQuest : Quest
{
    public BossQuest()
    {
        this.questName = "Kill Slime King";
        this.questDesc = "There's a huge slime on the battleground, find and kill it.";
        this.questType = QuestType.MainQuest;
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
