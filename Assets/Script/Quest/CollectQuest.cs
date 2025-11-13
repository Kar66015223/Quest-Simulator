using UnityEngine;

public class CollectQuest : Quest
{
    public CollectQuest()
    {
        int random = Random.Range(20, 30);
        this.questName = $"Collect {random} coins";
        this.questDesc = "I lost some coin on the battleground, find and return them to me";
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
