using UnityEngine;

public class KillQuest : Quest
{
    public KillQuest()
    {
        int random = Random.Range(10, 25);
        this.questName = $"Kill {random} slimes";
        this.questDesc = "Go to the battleground and kill some slimes";
        this.questType = QuestType.SideQuest;
        this.questStatus = QuestStatus.Pending;
    }

    public override void CheckCompletion()
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
