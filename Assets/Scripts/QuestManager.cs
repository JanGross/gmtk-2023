using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;
    public int activeQuest = 0;

    public Quest[] quests = {
        new Quest(_name: "Advert Quest 1", _desc: "Find someone to hand out flyers to advertise our tavern to adventurers. Strength, dexterity, intelligence: doesn’t matter, find someone who would do this for as little money as possible.", _failedStr: "Damn, that didn’t work at all. That robot just randomly started playing an instrument, much to the dismay of the townspeople.", _strength: 0, _intelligence: 0, _charisma: 5),
        
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

}

[System.Serializable]
public class Quest
{
    public string name;
    public string description;
    public string failedStr;
    public int strength, intelligence, charisma;
    
    public Quest(string _name, string _desc, string _failedStr, int _strength, int _intelligence, int _charisma)
    {
        this.name = _name;
        this.description = _desc;
        this.failedStr = _failedStr;
        this.strength = _strength;
        this.intelligence = _intelligence;
        this.charisma = _charisma;

    }
}