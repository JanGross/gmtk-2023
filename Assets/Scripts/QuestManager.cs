using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;
    public int activeQuest = 0;

    public Quest[] quests = {
        new Quest(_name: "Advert Quest 1", _desc: "Find someone to hand out flyers to advertise our tavern to adventurers. Strength, dexterity, intelligence: doesn’t matter, find someone who would do this for as little money as possible.", _failedStr: "Damn, that didn’t work at all. That robot just randomly started playing an instrument, much to the dismay of the townspeople.", _successStr: "Perfect, this place will be full of adventurers in no time.", _strength: 0, _intelligence: 0, _charisma: 5),
    };

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("There can only be one instance of the CharacterManager class");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (Quest quest in quests)
        {
            Debug.Log(quest.name);
            Debug.Log(quest.description);
        }
    }

    public Quest GetActiveQuest()
    {
        return quests[activeQuest];
    }

    public bool RunQuestWithAdventurer(CharacterData characterData, Quest quest)
    {
        if (characterData.m_charisma < quest.charisma)
        {
            Debug.Log("Character failed charisma check");
            return false;
        }
        if (characterData.m_strength < quest.strength)
        {
            Debug.Log("Character failed strength check");
            return false;
        }
        if (characterData.m_intelligence < quest.intelligence)
        {
            Debug.Log("Character failed intlligence check");
            return false;
        }

        return true;
    }

    public void NextQuest()
    {
        activeQuest++;
        if (activeQuest >= quests.Length)
        {
            Debug.Log("All quests completed");
        }

        foreach (var adventurer in Resources.FindObjectsOfTypeAll<AdventurerInteractable>())
        {
            if (adventurer.m_spawnQuestID == activeQuest)
            {
                adventurer.gameObject.SetActive(true);
            }
        }
    }
}

[System.Serializable]
public class Quest
{
    public string name;
    public string description;
    public string successStr;
    public string failedStr;
    public int strength, intelligence, charisma;
    
    public Quest(string _name, string _desc, string _failedStr, string _successStr, int _strength, int _intelligence, int _charisma)
    {
        this.name = _name;
        this.description = _desc;
        this.failedStr = _failedStr;
        this.successStr = _successStr;
        this.strength = _strength;
        this.intelligence = _intelligence;
        this.charisma = _charisma;

    }
}