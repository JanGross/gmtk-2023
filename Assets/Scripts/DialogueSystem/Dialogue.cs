using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public string text;
}

public class QuestionData
{
    public static string[] Questions = new string[] 
    { 
        "Who are you?", 
        "Tell me something about yourself.", 
        "What is your greatest accomplishment?", 
        "Where do you see yourself in five years?", 
        "What are your salary requirements?" 
    };
}
