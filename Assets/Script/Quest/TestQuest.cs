using UnityEngine;

public class TestQuest : Quest
{
    public TestQuest()
    {
        this.questName = "Press E";
        this.questDesc = "Press E to complete this quest!\nPress R to fail this quest!";
        this.questType = QuestType.SideQuest;
        this.questStatus = QuestStatus.Pending;
    }
    public override void CheckCompletion()
    {
        if (this.questStatus == QuestStatus.OnProgress)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                this.questStatus = QuestStatus.Completed;
                QuestCompleted();
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                this.questStatus = QuestStatus.Failed;
                QuestFailed();
            } 
        }
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
