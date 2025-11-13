using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class QuestManager : MonoBehaviour
{
    public List<Quest> firstList = new List<Quest>();

    public List<Button> questButtons = new List<Button>();

    [SerializeField] private Player player;

    public void Start()
    {
        if (player == null)
        {
            player = FindFirstObjectByType<Player>();
        }

        firstList.Add(new KillQuest());
        firstList.Add(new CollectQuest());

        DisplayQuest();
    }

    public void DisplayQuest()
    {
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("QuestButton"))
        {
            questButtons.Add(obj.GetComponent<Button>());
        }

        for (int i = 0; i < questButtons.Count; i++)
        {
            Quest q = firstList[i];
            TMP_Text text = questButtons[i].GetComponentInChildren<TMP_Text>();
            text.text = q.questName;
        }
    }
}
