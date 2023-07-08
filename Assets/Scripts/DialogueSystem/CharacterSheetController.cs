using System.Collections.Generic;
using UnityEngine;

public class CharacterSheetController : MonoBehaviour
{
    [SerializeField] private CharacterSheet m_characterSheet;
    [SerializeField] private Transform m_characterSheetTransform;

    public IReadOnlyCollection<CharacterSheet> CharacterSheets => m_characterSheets.Values;

    private Dictionary<string, CharacterSheet> m_characterSheets = new Dictionary<string, CharacterSheet>();

    // Returns if a character sheet exists for the character.
    public bool SheetExists(string characterName)
    {
        return m_characterSheets.ContainsKey(characterName);
    }

    // Get the character sheet for the passed in character.
    public CharacterSheet GetSheet(string name)
    {
        if (!m_characterSheets.ContainsKey(name))
            return null;

        CharacterSheet sheet = null;
        m_characterSheets.TryGetValue(name, out sheet);

        return sheet;
    }

    // Creates a new character sheet.
    public CharacterSheet CreateSheet(string characterName)
    {
        var sheet = Instantiate(m_characterSheet, m_characterSheetTransform);

        m_characterSheets.Add(characterName, sheet);

        return sheet;
    }
}
