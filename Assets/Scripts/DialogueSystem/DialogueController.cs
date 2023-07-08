using System;
using System.Collections.Generic;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    [SerializeField] private DialoguePanel m_dialoguePanel;

    // DEBUG - Will need replacing with the character you selected.
    private void Start()
    {
        DisplayCharacterText(CharacterManager.Instance.CharacterDatas[0]);

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
}
