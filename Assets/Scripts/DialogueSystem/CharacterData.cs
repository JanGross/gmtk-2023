using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Characters/Generate Character Data", order = 1)]
public class CharacterData : ScriptableObject
{
    public string m_name;

    public int m_charisma;
    public int m_strength;
    public int m_dexterity;
    public int m_intelligence;
    public int m_money;

    // Note - the index of the dialogue will control what question it is linked too.
    public List<Dialogue> m_dialogueOptions;
}
