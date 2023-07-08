using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager Instance;

    [SerializeField] private List<CharacterData> m_characterDatas;

    public ReadOnlyCollection<CharacterData> CharacterDatas => m_characterDatas.AsReadOnly();

    private Dictionary<string, bool> m_interviewed = new Dictionary<string, bool>();

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

    // Returns if a character has been interviewed.
    public bool CharacterInterviewed(string name)
    {
        return m_interviewed.ContainsKey(name);
    }

    // Stores a character as interviewed.
    public void SetInterviewed(string name)
    {
        m_interviewed.Add(name, true);
    }
}
