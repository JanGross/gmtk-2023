using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    [SerializeField] private string m_dialogueText;
}

public class QuestionData
{
    public string[] m_questions = new string[] 
    { 
        "Who are you?", 
        "Tell me something about yourself.", 
        "What is your greatest accomplishment?", 
        "Where do you see yourself in five years?", 
        "What are your salary requirements?" 
    };
}
