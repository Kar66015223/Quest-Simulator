using Unity.VisualScripting;
using UnityEngine;

public abstract class Quest
{
    public string questName;
    public string questDesc;
    public QuestType questType;
    public QuestStatus questStatus;

    public enum QuestType
    {
        MainQuest,
        SideQuest
    }

    public enum QuestStatus
    {
        Completed,
        Pending,
        OnProgress,
        Failed
    }

    public abstract void CheckCompletion();

    public abstract void QuestCompleted();

    public abstract void QuestFailed();
}
