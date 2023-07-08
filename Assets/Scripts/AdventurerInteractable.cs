using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdventurerInteractable : MonoBehaviour
{
    public DialogueController dialogueController;

    [SerializeField] private string m_name = string.Empty;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        PlayerController.Instance.cameraMovement = false;
        CharacterData character = CharacterManager.Instance.GetCharacterDataByName(m_name);
        dialogueController.DisplayCharacterText(character);
    }
}
