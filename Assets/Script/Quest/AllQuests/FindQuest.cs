using UnityEngine;

public class FindQuest : Quest
{
    public FindQuest()
    {
        this.questName = "Find diamond";
        this.questDesc = "I lost my diamond on the way here, it was in a crate, find and return them to me";
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
