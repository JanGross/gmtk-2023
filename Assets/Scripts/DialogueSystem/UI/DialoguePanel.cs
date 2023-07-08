using System;
using System.Collections;
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

    private CharacterData m_currentCharacter;
    private bool m_skipped = false;

    private int m_questionCount = QuestionData.Questions.Length;
    private int m_questionsAsked = 0;

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

        m_characterSheetController.Cleanup();
        m_characterSheetController.SetName(characterData.name);

        m_currentCharacter = characterData;
        m_characterNameText.text = characterData.name;

        // TODO: will this be changed with an introductory text?
        m_characterText.text = "Select an option...";

        gameObject.SetActive(true);
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

        // TODO: we should update the sheet with this information.
        m_characterSheetController.AddLine(dialogueOption.text);

        // Increment questions asked.
        m_questionsAsked++;
    }

    // Callback from Unity on the skip button.
    public void Action_SkipButtonClicked()
    {
        m_skipped = true;
    }
}
