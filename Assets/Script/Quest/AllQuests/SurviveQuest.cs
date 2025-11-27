using UnityEngine;

public class SurviveQuest : Quest
{
    public SurviveQuest()
    {
        this.questName = "เอาชีวิตรอด";
        this.questDesc = "มีฝูงสไลม์ขนาดใหญ่กำลังบุกมาที่หมู่บ้าน จงออกไปหยุดมันให้นานที่สุดเท่าที่ทำได้";
        this.questType = QuestType.MainQuest;
        this.questStatus = QuestStatus.Pending;
        this.questID = 4;
    }
    public override void CheckCompletion()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        EnemySpawner spawners = Object.FindAnyObjectByType<EnemySpawner>();

        string currentScene = LoadSceneManager.Instance.GetCurrentSceneName();

        if (player == null)
        {
            questStatus = QuestStatus.Failed;
            QuestFailed();
            return;
        }

        QuestProgressUIManager.Instance.Timer.SetActive(true);

        if (spawners != null)
        {
            if (!spawners.waveStarted)
                return;

            if (spawners.timeRemaining <= 0f)
            {
                questStatus = QuestStatus.Completed;
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
        QuestProgressUIManager.Instance.Timer.SetActive(false);
        Debug.Log($"{questName} completed!");
    }

    public override void QuestFailed()
    {
        QuestProgressUIManager.Instance.Timer.SetActive(false);
        Debug.Log($"{questName} failed!");
    }
}
