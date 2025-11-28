using UnityEngine;

public class TestQuest : Quest
{
    public TestQuest()
    {
        this.questName = "กด E";
        this.questDesc = "กด E เพื่อเคลียร์เควสนี้!\nกด R เพื่อล้มเกลวเควสนี้!";
        this.questType = QuestType.SideQuest;
        this.questStatus = QuestStatus.Pending;
        this.questID = 6;
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

        Debug.Log($"{questName} CheckCompletion is running");
    }

    public override void OnAccept()
    {
        QuestProgressUIManager.Instance.TestQuestProgressUI.SetActive(true);
        Debug.Log($"{questName} OnAccept is running");
    }

    public override void QuestCompleted()
    {
        QuestProgressUIManager.Instance.TestQuestProgressUI.SetActive(false);
        Debug.Log($"{questName} completed!");
    }

    public override void QuestFailed()
    {
        QuestProgressUIManager.Instance.TestQuestProgressUI.SetActive(false);
        Debug.Log($"{questName} failed!");
    }
}
