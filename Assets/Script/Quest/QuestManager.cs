using System.Collections.Generic;
using NUnit.Framework;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class QuestManager : MonoBehaviour
{
    private static QuestManager _instance;
    public static QuestManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("QuestManager Instance is null");
            }
            return _instance;
        }
    }

    public List<Quest> questList = new List<Quest>();
    public List<Button> questButtons = new List<Button>();
    public Button acceptButton;

    private Quest selectedQuest;

    public TMP_Text questDetailTitle;
    public TMP_Text questDetailDesc;

    public TMP_Text questProgressTitle;
    public TMP_Text[] questProgressStatus;

    [SerializeField] private Player player;

    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;

            DontDestroyOnLoad(gameObject);

            Debug.Log("QuestManager Singleton Initialized.");
        }
        else
        {
            Debug.Log("Duplicate GameManager found. Destroying self.");
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        if (player == null)
        {
            player = FindFirstObjectByType<Player>();
        }

        questList.Add(new KillQuest());
        questList.Add(new CollectQuest());
        questList.Add(new TestQuest());

        DisplayQuest();
    }

    public void Update()
    {
        if (selectedQuest != null)
        {
            foreach (Quest q in questList)
            {
                if (q.questStatus == Quest.QuestStatus.OnProgress)
                    q.CheckCompletion();
            }

            if (selectedQuest.questStatus == Quest.QuestStatus.Failed)
            {
                questProgressStatus[0].gameObject.SetActive(true);
            }
            if (selectedQuest.questStatus == Quest.QuestStatus.Completed)
            {
                questProgressStatus[1].gameObject.SetActive(true);
            }

            Debug.Log($"{selectedQuest.questName} {selectedQuest.questStatus}");
        }
    }

    public void DisplayQuest()
    {
        questButtons.Clear();

        List<GameObject> objs = GameObjectHelper.FindObjectsWithTagIncludingInactive("QuestButton");
        objs.Sort((a, b) => a.transform.GetSiblingIndex().CompareTo(b.transform.GetSiblingIndex()));

        foreach (GameObject obj in objs)
        {
            Button btn = obj.GetComponent<Button>();
            if (btn != null)
                questButtons.Add(btn);
        }

        if (questButtons != null)
        {
            for (int i = 0; i < questButtons.Count; i++)
            {
                int index = i;

                TMP_Text text = questButtons[i].GetComponentInChildren<TMP_Text>();
                text.text = questList[i].questName;

                questButtons[i].onClick.AddListener(() =>
                {
                    selectedQuest = questList[index];  // store selected quest
                    UpdateQuestDetails();
                });
            }

            acceptButton.onClick.AddListener(UpdateQuestProgress); 
        }
    }

    private void UpdateQuestDetails()
    {
        questDetailTitle.text = selectedQuest.questName;
        questDetailDesc.text = selectedQuest.questDesc;
    }

    private void UpdateQuestProgress()
    {
        if (selectedQuest == null) return;

        selectedQuest.questStatus = Quest.QuestStatus.OnProgress;
        questProgressTitle.text = selectedQuest.questName;
    }

    private GameObject GetSelectedObject()
    {
        return EventSystem.current.currentSelectedGameObject;
    }

    private Quest GetSelectedQuest()
    {
        GameObject selectedObj = GetSelectedObject();
        if (selectedObj != null && questButtons != null)
        {
            int index = questButtons.IndexOf(selectedObj.GetComponent<Button>());
            if (index == -1) return null; // item not found in the list

            selectedQuest = questList[index];
        }
        return selectedQuest;
    }
}