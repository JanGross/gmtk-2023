using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdventurerInteractable : MonoBehaviour
{
    public int m_spawnQuestID = 0;

    [SerializeField] private string m_name = string.Empty;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnMouseEnter()
    {
        GameManager.Instance.uiManager.SetCursor(GameManager.Instance.m_talkCursor);
    }

    void OnMouseExit()
    {
        GameManager.Instance.uiManager.SetCursor(GameManager.Instance.m_defaultCursor);
    }

    private void OnMouseDown()
    {
        if (GameManager.Instance.uiManager.InputBlocked)
            return;

        if (GameManager.Instance.dialogueController.DialogueInProgress)
            return;

        GameManager.Instance.uiManager.BlockInput(true);
        PlayerController.Instance.cameraMovement = false;
        CharacterData character = CharacterManager.Instance.GetCharacterDataByName(m_name);
        GameManager.Instance.dialogueController.DisplayCharacterText(character);
    }
}
