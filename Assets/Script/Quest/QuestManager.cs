using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
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

    public GameObject questUI;
    public List<Quest> questList = new List<Quest>();
    public GameObject rowPrefab;
    public Transform rowParent;

    public Quest selectedQuest;

    public GameObject questDetailUI;
    public TMP_Text questDetailTitle;
    public TMP_Text questDetailDesc;

    public GameObject questProgressUI;
    public TMP_Text questProgressTitle;
    public TMP_Text[] questProgressStatus;

    private Player player;

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
            Debug.Log("Duplicate QuestManager found. Destroying self.");
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        if (player == null)
        {
            player = FindFirstObjectByType<Player>();
        }

        questList.Add(new BossQuest());
        questList.Add(new KillQuest());
        questList.Add(new CollectQuest());

        questList.Add(new SurviveQuest());
        questList.Add(new FindQuest());
        questList.Add(new EscortQuest());

        DisplayQuest();
    }

    public void Update()
    {
        if (selectedQuest != null)
        {
            foreach (Quest q in questList)
            {
                if (q.questStatus == Quest.QuestStatus.OnProgress)
                {
                    q.OnAccept();
                    q.CheckCompletion();
                }
            }

            questProgressStatus[0].gameObject.SetActive(false);
            questProgressStatus[1].gameObject.SetActive(false);

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
        foreach (Quest quests in questList)
        {
            GameObject questButton = Instantiate(rowPrefab, rowParent);
            quests.uiButton = questButton;

            TMP_Text buttonText = questButton.GetComponentInChildren<TMP_Text>();
            if (buttonText != null)
            {
                buttonText.text = quests.questName;
            } 

            Button btn = questButton.GetComponent<Button>();
            if (btn != null)
            {
                Quest capturedQuest = quests;
                btn.onClick.AddListener(() => OnButtonClicked(capturedQuest));
            }
        }
    }

    public void OnButtonClicked(Quest clickedQuest)
    {
        selectedQuest = clickedQuest;

        UpdateQuestDetails();

        questUI.SetActive(false);
        questDetailUI.SetActive(true);

        Debug.Log($"Clicked: {clickedQuest.questName}");
    }

    private void UpdateQuestDetails()
    {
        questDetailTitle.text = selectedQuest.questName;
        questDetailDesc.text = selectedQuest.questDesc;
    }

    public void UpdateQuestProgress()
    {
        if (selectedQuest == null) return;

        selectedQuest.questStatus = Quest.QuestStatus.OnProgress;
        questProgressTitle.text = selectedQuest.questName;
    }

    public void RemoveQuest()
    {
        if (selectedQuest != null)
        {
            questProgressUI.SetActive(false);
            questProgressStatus[0].gameObject.SetActive(false);
            questProgressStatus[1].gameObject.SetActive(false);

            if (selectedQuest.questStatus == Quest.QuestStatus.Completed)
            {
                questList.Remove(selectedQuest);
                Destroy(selectedQuest.uiButton);
                selectedQuest = null;

                return;
            }

            if (selectedQuest.questStatus == Quest.QuestStatus.Failed)
            {
                selectedQuest = null;
            } 
        }
    }
}