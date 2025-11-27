using UnityEngine;

public class BossQuest : Quest
{
    public BossQuest()
    {
        this.questName = "ฆ่าราชาสไลม์";
        this.questDesc = "มีราชาสไลม์ตัวใหญ่เดินเพ่นพ่านอยู่ใกล้หมู่บ้าน จงออกไปจัดการมัน";
        this.questType = QuestType.MainQuest;
        this.questStatus = QuestStatus.Pending;
        this.questID = 3;
    }

    public override void CheckCompletion()
    {
        if (!BossState.bossSpawned)
            return;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject boss = GameObject.FindGameObjectWithTag("Boss");

        string currentScene = LoadSceneManager.Instance.GetCurrentSceneName();

        if (player == null)
        {
            this.questStatus = QuestStatus.Failed;
            QuestFailed();
            return;
        }

        if (currentScene == "Battleground")
        {
            if (boss == null)
            {
                this.questStatus = QuestStatus.Completed;
                QuestCompleted();
                return;
            }
        }

        Debug.Log($"{questName} CheckCompletion is running");
    }

    public override void OnAccept()
    {
        Debug.Log($"{questName} OnAccept is running");
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
