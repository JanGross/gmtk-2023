using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Characters/Generate Character Data", order = 1)]
public class CharacterData : ScriptableObject
{
    public Sprite m_image;
    public string m_name;

    public int m_charisma;
    public int m_strength;
    public int m_dexterity;
    public int m_intelligence;

    [Header("The index of the dialogue relates to what question it should link too.")]
    public List<Dialogue> m_dialogueOptions;
}
