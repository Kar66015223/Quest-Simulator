using TMPro;
using UnityEngine;

public class QuestProgressUIManager : MonoBehaviour
{
    private static QuestProgressUIManager _instance;
    public static QuestProgressUIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("QuestProgressUIManager Instance is null");
            }
            return _instance;
        }
    }

    public GameObject KillQuestProgressUI;
    public GameObject CollectQuestProgressUI;
    public GameObject TestQuestProgressUI;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;

            DontDestroyOnLoad(gameObject);

            Debug.Log("QuestProgressUIManager Singleton Initialized.");
        }
        else
        {
            Debug.Log("Duplicate QuestProgressUIManager found. Destroying self.");
            Destroy(gameObject);
        }
    }
}
