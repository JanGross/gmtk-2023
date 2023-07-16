using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class Journal : MonoBehaviour
{
    [SerializeField] private GameObject m_journal;
    [SerializeField] private AudioClip m_pageSound, m_openSound, m_closeSound;

    [SerializeField] 
    private Transform m_adventurerPage, m_questPage;

    private List<CharacterData> m_availableAdventurers = new List<CharacterData>();
    private int m_selectedAdventurer = 0;


    void OnMouseEnter()
    {
        GameManager.Instance.uiManager.SetCursor(GameManager.Instance.m_lookCursor);
    }

    void OnMouseExit()
    {
        GameManager.Instance.uiManager.SetCursor(GameManager.Instance.m_defaultCursor);
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
                m_availableAdventurers.Add(character);
            }
        }
        SetJournalQuestPage();

        if (m_availableAdventurers.Count > 0)
        {
            SetJournalAdventurerPage(m_selectedAdventurer);
            m_adventurerPage.gameObject.SetActive(true);
        }
        m_journal.SetActive(true);
        GetComponent<AudioSource>().PlayOneShot(m_openSound,1);
    }
    
    public void CloseJournal()
    {
        GetComponent<AudioSource>().PlayOneShot(m_closeSound,1);
        m_journal.SetActive(false);
        m_adventurerPage.gameObject.SetActive(false);
        PlayerController.Instance.cameraMovement = true;

        GameManager.Instance.uiManager.BlockInput(false);
    }

    private void SetJournalAdventurerPage(int id)
    {
        CharacterData chara = m_availableAdventurers[id];
        m_adventurerPage.Find("AdventurerName").gameObject.GetComponent<TMP_Text>().text = chara.name;
        m_adventurerPage.Find("AdventurerAvatar").gameObject.GetComponent<Image>().sprite = chara.m_avatar;
        string cv = "";
        foreach (var line in chara.m_dialogueOptions)
        {
            cv += $"- {line.bulletizedText}\n";
        }
        m_adventurerPage.Find("AdventurerCV").gameObject.GetComponent<TMP_Text>().text = cv;

    }

    private void SetJournalQuestPage()
    {
        Quest quest = QuestManager.Instance.GetActiveQuest();
        m_questPage.Find("QuestName").gameObject.GetComponent<TMP_Text>().text = quest.name;
        m_questPage.Find("QuestDescription").gameObject.GetComponent<TMP_Text>().text = quest.description;
    }

    public void NextAdventurer()
    {
        m_selectedAdventurer = ++m_selectedAdventurer % m_availableAdventurers.Count();
        SetJournalAdventurerPage(m_selectedAdventurer);
        AudioSource audio = GetComponent<AudioSource>();
        audio.PlayOneShot(m_pageSound,1);
    }

    public void PreviousAdventurer()
    {
        m_selectedAdventurer = --m_selectedAdventurer;
        if (m_selectedAdventurer < 0)
        {
            m_selectedAdventurer = m_availableAdventurers.Count() - 1;
        }
        SetJournalAdventurerPage(m_selectedAdventurer);
        GetComponent<AudioSource>().PlayOneShot(m_pageSound);

    }

    public void AssignAdventurer()
    {
        Quest activeQuest = QuestManager.Instance.GetActiveQuest();
        bool success = QuestManager.Instance.RunQuestWithAdventurer(m_availableAdventurers[m_selectedAdventurer], activeQuest);
        m_adventurerPage.gameObject.SetActive(false);

        m_questPage.Find("QuestResult").gameObject.SetActive(true);
        if (success)
        {
            m_questPage.Find("QuestResult/NextQuest").gameObject.SetActive(true);
            m_questPage.Find("QuestResult/RetryQuest").gameObject.SetActive(false);
        } else
        {
            m_questPage.Find("QuestResult/NextQuest").gameObject.SetActive(false);
            m_questPage.Find("QuestResult/RetryQuest").gameObject.SetActive(true);
        }

        m_questPage.Find("QuestResult/QuestResultText").gameObject.GetComponent<TMP_Text>().text = success ? activeQuest.successStr : activeQuest.failedStr;
        
    }

    public void StartNextQuest()
    {
        Debug.Log("Starting next day");
        m_questPage.Find("QuestResult").gameObject.SetActive(false);
        QuestManager.Instance.NextQuest();
        CloseJournal();
    }

    public void RetryQuest()
    {
        Debug.Log("Retrying quest");
        m_questPage.Find("QuestResult").gameObject.SetActive(false);
        QuestManager.Instance.RetryQuest();
        CloseJournal();
    }
}
