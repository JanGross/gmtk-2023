using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    [SerializeField] private DialoguePanel m_dialoguePanel;
    [SerializeField] private List<CharacterData> m_characterDatas;

    // DEBUG - Will need replacing with the character you selected.
    private void Start()
    {
        DisplayCharacterText(m_characterDatas[0]);
    }

    public void DisplayCharacterText(CharacterData character)
    {
        m_dialoguePanel.Setup(character);
    }
}
