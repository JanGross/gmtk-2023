using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Journal : MonoBehaviour
{
    public GameObject journal;
    private List<CharacterData> m_availableAdventurers = new List<CharacterData>();

    public Transform adventurerPage;
    public Transform questPage;
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
        Debug.Log("MOUSE DOWN ON INVENTORY");
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
            SetJournalAdventurerPage(0);
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
    }

    public void SetJournalAdventurerPage(int id)
    {
        CharacterData chara = m_availableAdventurers[id];
        TMP_Text nameLabel = adventurerPage.Find("AdventurerName").gameObject.GetComponent<TMP_Text>();
        nameLabel.text = chara.m_name;
    }

    public void SetJournalQuestPage()
    {
        Quest quest = QuestManager.Instance.GetActiveQuest();
        questPage.Find("QuestName").gameObject.GetComponent<TMP_Text>().text = quest.name;
        questPage.Find("QuestDescription").gameObject.GetComponent<TMP_Text>().text = quest.description;
    }
}
