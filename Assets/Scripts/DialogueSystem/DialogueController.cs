using System;
using System.Collections.Generic;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    [SerializeField] private DialoguePanel m_dialoguePanel;

    private int m_currentIndex = 0;

    // DEBUG - Will need replacing with the character you selected.
    private void Start()
    {
        DisplayCharacterText(CharacterManager.Instance.CharacterDatas[m_currentIndex]);

        m_dialoguePanel.OnQuestionsFinished += OnQuestionsFinished;
    }

    public void DisplayCharacterText(CharacterData character)
    {
        m_dialoguePanel.Setup(character);
    }

    public void OnQuestionsFinished(string characterName)
    {
        Debug.Log($"DialogueController: Character {characterName} finished interviewing");

        CharacterManager.Instance.SetInterviewed(characterName);

        // TODO: stop the dialogue and return to gameplay...
        // TODO: we could probably show a "Quit" button highlighted.
    }

    public void Debug_NextCharacter()
    {
        if (m_currentIndex + 1 >= CharacterManager.Instance.CharacterDatas.Count)
            return;

        m_currentIndex++;
        DisplayCharacterText(CharacterManager.Instance.CharacterDatas[m_currentIndex]);
    }

    public void Debug_PreviousCharacter()
    {
        if (m_currentIndex - 1 < 0)
            return;

        m_currentIndex--;
        DisplayCharacterText(CharacterManager.Instance.CharacterDatas[m_currentIndex]);
    }
}
