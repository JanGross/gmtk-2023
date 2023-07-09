using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Journal : MonoBehaviour
{
    public GameObject journal;
    private List<CharacterData> m_availableAdventurers = new List<CharacterData>();

    public Transform adventurerPage;
    public Transform questPage;

    private int m_selectedAdventurer = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if (GameManager.Instance.dialogueController.DialogueInProgress)
        {
            Debug.Log("Dialog in progress, not howing journal");
            return;
        }

        GameManager.Instance.uiManager.BlockInput(true);

        m_availableAdventurers.Clear();
        foreach (var character in CharacterManager.Instance.CharacterDatas)
        {
            if (CharacterManager.Instance.CharacterInterviewed(character.name)) {
                Debug.Log("Available Adventurer:" +  character.name);
                m_availableAdventurers.Add(character);
            }
        }
        SetJournalQuestPage();

        if (m_availableAdventurers.Count > 0)
        {
            SetJournalAdventurerPage(m_selectedAdventurer);
            adventurerPage.gameObject.SetActive(true);
        }
        journal.SetActive(true);
        PlayerController.Instance.cameraMovement = false;
    }
    
    public void CloseJournal()
    {
        journal.SetActive(false);
        adventurerPage.gameObject.SetActive(false);
        PlayerController.Instance.cameraMovement = true;

        GameManager.Instance.uiManager.BlockInput(false);
    }

    public void SetJournalAdventurerPage(int id)
    {
        CharacterData chara = m_availableAdventurers[id];
        adventurerPage.Find("AdventurerName").gameObject.GetComponent<TMP_Text>().text = chara.name;
        adventurerPage.Find("AdventurerAvatar").gameObject.GetComponent<Image>().sprite = chara.m_avatar;
        string cv = "";
        foreach (var line in chara.m_dialogueOptions)
        {
            cv += $"- {line.bulletizedText}\n";
        }
        adventurerPage.Find("AdventurerCV").gameObject.GetComponent<TMP_Text>().text = cv;

    }

    public void SetJournalQuestPage()
    {
        Quest quest = QuestManager.Instance.GetActiveQuest();
        questPage.Find("QuestName").gameObject.GetComponent<TMP_Text>().text = quest.name;
        questPage.Find("QuestDescription").gameObject.GetComponent<TMP_Text>().text = quest.description;
    }

    public void NextAdventurer()
    {
        m_selectedAdventurer = ++m_selectedAdventurer % m_availableAdventurers.Count();
        SetJournalAdventurerPage(m_selectedAdventurer);
    }

    public void PreviousAdventurer()
    {
        m_selectedAdventurer = --m_selectedAdventurer;
        if (m_selectedAdventurer < 0)
        {
            m_selectedAdventurer = m_availableAdventurers.Count() - 1;
        }
        SetJournalAdventurerPage(m_selectedAdventurer);
    }

    public void AssignAdventurer()
    {
        Quest activeQuest = QuestManager.Instance.GetActiveQuest();
        bool success = QuestManager.Instance.RunQuestWithAdventurer(m_availableAdventurers[m_selectedAdventurer], activeQuest);
        Debug.Log("THE QUESTR ESULT WAS: " + success);
        adventurerPage.gameObject.SetActive(false);

        questPage.Find("QuestResult").gameObject.SetActive(true);
        if (success)
        {
            questPage.Find("QuestResult/NextQuest").gameObject.SetActive(true);
            questPage.Find("QuestResult/RetryQuest").gameObject.SetActive(false);
        } else
        {
            questPage.Find("QuestResult/NextQuest").gameObject.SetActive(false);
            questPage.Find("QuestResult/RetryQuest").gameObject.SetActive(true);
        }

        questPage.Find("QuestResult/QuestResultText").gameObject.GetComponent<TMP_Text>().text = success ? activeQuest.successStr : activeQuest.failedStr;
        
    }

    public void StartNextQuest()
    {
        Debug.Log("Starting next day");
        questPage.Find("QuestResult").gameObject.SetActive(false);
        QuestManager.Instance.NextQuest();
        CloseJournal();
        Debug.Log("Started next quest");
    }

    public void RetryQuest()
    {
        Debug.Log("Retrying quest");
        questPage.Find("QuestResult").gameObject.SetActive(false);
        QuestManager.Instance.RetryQuest();
        CloseJournal();
    }
}
