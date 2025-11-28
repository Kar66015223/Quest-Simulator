using UnityEngine;

public class KillQuest : Quest
{
    public int killGoal = Random.Range(10, 20);
    public KillQuest()
    {
        this.questName = $"ล่าสไลม์ {killGoal} ตัว";
        this.questDesc = "ข้างนอกนั่นมีสไลม์เพ่นพ่านอยู่เต็มไปหมด ออกไปจัดการมันให้จำนวนมันน้อยลงหน่อย";
        this.questType = QuestType.SideQuest;
        this.questStatus = QuestStatus.Pending;
        this.questID = 1;
    }

    public override void CheckCompletion()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        string currentScene = LoadSceneManager.Instance.GetCurrentSceneName();

        if (GameManager.Instance.killCount >= killGoal && currentScene == "MainGame")
        {
            this.questStatus = QuestStatus.Completed;
            QuestCompleted();
        }

        if (player == null)
        {
            this.questStatus = QuestStatus.Failed;
            QuestFailed();
        }

        Debug.Log($"{questName} CheckCompletion is running");
    }

    public override void OnAccept()
    {
        QuestProgressUIManager.Instance.KillQuestProgressUI.SetActive(true);

        Debug.Log($"{questName} OnAccept is running");
    }

    public override void QuestCompleted()
    {
        QuestProgressUIManager.Instance.KillQuestProgressUI.SetActive(false);
        GameManager.Instance.killCount = 0;
        GameManager.Instance.UpdateKillUI();

        Debug.Log($"{questName} completed!");
    }

    public override void QuestFailed()
    {
        QuestProgressUIManager.Instance.KillQuestProgressUI.SetActive(false);
        GameManager.Instance.killCount = 0;
        GameManager.Instance.UpdateKillUI();

        Debug.Log($"{questName} failed!");
    }
}
