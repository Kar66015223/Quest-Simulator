using UnityEngine;

public class EscortQuest : Quest
{
    public EscortQuest()
    {
        this.questName = "Escort TurtleShell";
        this.questDesc = "I lost my pet turtle on the way here, I miss him. Find and bring him back to me.";
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
