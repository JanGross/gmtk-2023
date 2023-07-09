using System;
using System.Collections.Generic;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    [SerializeField] private DialoguePanel m_dialoguePanel;

    private int m_currentIndex = 0;

    public bool DialogueInProgress => m_dialoguePanel.gameObject.activeSelf;


    // DEBUG - Will need replacing with the character you selected.
    private void Start()
    {
        //DisplayCharacterText(CharacterManager.Instance.CharacterDatas[m_currentIndex]);

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
        GameManager.Instance.uiManager.BlockInput(false);
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
