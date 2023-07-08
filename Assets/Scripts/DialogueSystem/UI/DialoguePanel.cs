using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialoguePanel : MonoBehaviour
{
    public Action<string> OnQuestionsFinished;

    [SerializeField] private Transform m_questionHolder;
    [SerializeField] private Button m_questionButton;

    [SerializeField] private TMP_Text m_characterNameText;
    [SerializeField] private TMP_Text m_characterText;
    [SerializeField] private CharacterSheetController m_characterSheetController;

    private const float TypingSpeed = 0.03f;

    private List<int> m_questionIndexAsked = new List<int>();
    private List<Button> m_questionButtons = new List<Button>();

    private CharacterData m_currentCharacter;
    private bool m_skipped = false;

    private int m_questionCount = QuestionData.Questions.Length;
    private int m_questionsAsked = 0;

    private CharacterSheet m_characterSheet;
    private string m_lineToAdd = "";

    private bool QuestionsFinished => m_questionsAsked >= m_questionCount;

    private void Awake()
    {
        PopulateQuestionButtons();
        gameObject.SetActive(false);
    }

    // Sets the reference to the characterData to use.
    public void Setup(CharacterData characterData)
    {
        Cleanup();

        if (m_characterSheet != null)
        {
            // Clear the old sheet
            m_characterSheet.gameObject.SetActive(false);
        }

        m_questionsAsked = 0;
        m_currentCharacter = characterData;
        m_characterNameText.text = characterData.name;

        m_characterText.text = "Select an option...";

        SetupCharacterSheet();

        if (CharacterManager.Instance.CharacterInterviewed(characterData.name))
        {
            m_characterSheet.SetName(characterData.name);
            m_characterSheet.gameObject.SetActive(true);

            // Hide the dialogue panel.
            gameObject.SetActive(false);

            return;
        }

        m_questionIndexAsked.Clear();
        m_questionHolder.gameObject.SetActive(true);
        gameObject.SetActive(true);

        foreach(var button in m_questionButtons)
        {
            button.interactable = true;
        }
    }

    private void SetupCharacterSheet()
    {
        var sheetExists = m_characterSheetController.SheetExists(m_currentCharacter.name);

        if (sheetExists)
        {
            m_characterSheet = m_characterSheetController.GetSheet(m_currentCharacter.name);
            return;
        }

        // Create a new character sheet
        m_characterSheet = m_characterSheetController.CreateSheet(m_currentCharacter.name);
    }

    // Handles cleaing up for the next text.
    private void Cleanup()
    {
        m_skipped = false;
        m_characterText.text = "";
        StopAllCoroutines();
    }

    // Populates the questions the player can ask the NPC.
    private void PopulateQuestionButtons()
    {
        for (int i = 0; i < QuestionData.Questions.Length; i++)
        {
            var buttonObj = Instantiate(m_questionButton, m_questionHolder);
            
            // Sets the button text to the question text.
            var buttonText = buttonObj.GetComponentInChildren<TMP_Text>();
            buttonText.text = QuestionData.Questions[i];

            var button = buttonObj.GetComponent<Button>();

            var index = i;
            button.onClick.AddListener(() => OnQuestionButtonClicked(index));

            button.gameObject.SetActive(true);

            m_questionButtons.Add(button);
        }
    }

    private IEnumerator DisplayText(string text)
    {
        // Hide the questions whilst the character is talking.
        m_questionHolder.gameObject.SetActive(false);

        foreach (char letter in text.ToCharArray())
        {
            if (m_skipped)
            {
                break;
            }

            m_characterText.text += letter;
            yield return new WaitForSeconds(TypingSpeed);
        }

        m_skipped = true;
        m_characterText.text = text;

        // Add the current line to the character sheet.
        PopulateCharacterSheet();

        // If we've asked all the questions we should mark this character as interviewed and continue.
        if (QuestionsFinished)
        {
            OnQuestionsFinished?.Invoke(m_currentCharacter.name);
            yield break;
        }

        // Re-enable the questions.
        m_questionHolder.gameObject.SetActive(true);
    }

    // Handles displaying the correct dialogue for the question.
    private void OnQuestionButtonClicked(int index)
    {
        Cleanup();

        var dialogueOption = m_currentCharacter.m_dialogueOptions[index];
        StartCoroutine(DisplayText(dialogueOption.text));

        // Setup the line to add to the character sheet once the dialogue has finished.
        m_lineToAdd = $"<b>Q.) {QuestionData.Questions[index]}\n</b>A.) {dialogueOption.bulletizedText}";

        MarkQuestionAsked(index);
    }

    // Marks the question as asked if it hasn't been asked already.
    private void MarkQuestionAsked(int index)
    {
        if (m_questionIndexAsked.Contains(index))
            return;

        // Increment questions asked.
        m_questionsAsked++;
        m_questionIndexAsked.Add(index);

        m_questionButtons[index].interactable = false;
    }

    private void PopulateCharacterSheet()
    {
        m_characterSheet.AddLine(m_lineToAdd);
        m_lineToAdd = "";
    }

    // Callback from Unity on the skip button.
    public void Action_SkipButtonClicked()
    {
        m_skipped = true;
    }
}
