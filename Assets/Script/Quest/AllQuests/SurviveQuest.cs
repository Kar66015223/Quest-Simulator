using UnityEngine;

public class SurviveQuest : Quest
{
    public SurviveQuest()
    {
        this.questName = "Stay Alive";
        this.questDesc = "A horde of slimes are coming to the village, go out and distract them as long as you can";
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
