using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class QuestLogInteractable : MonoBehaviour
{
    public GameObject journal;
    public PlayerController playerController;

    private List<CharacterData> m_availableAdventurers = new List<CharacterData>();

    public Transform adventurerPage;
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
        foreach (var character in CharacterManager.Instance.CharacterDatas)
        {
            if (CharacterManager.Instance.CharacterInterviewed(character.name)) {
                m_availableAdventurers.Append(character);
            }
        }

        journal.SetActive(true);
        if (m_availableAdventurers.Count > 0)
        {
            SetJournalAdventurerPage(0);
            adventurerPage.gameObject.SetActive(true);
        }
        playerController.cameraMovement = false;
    }
    
    public void CloseJournal()
    {
        journal.SetActive(false);
        adventurerPage.gameObject.SetActive(false);
        playerController.cameraMovement = true;
    }

    public void SetJournalAdventurerPage(int id)
    {
        CharacterData chara = m_availableAdventurers[id];
        TMP_Text nameLabel = adventurerPage.Find("AdventurerName").gameObject.GetComponent<TMP_Text>();
        nameLabel.text = chara.m_name;
    }
}
