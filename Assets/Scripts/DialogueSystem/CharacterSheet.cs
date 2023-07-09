using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterSheet : MonoBehaviour
{
    [SerializeField] private TMP_Text m_characterNameText;
    [SerializeField] private TMP_Text m_line;
    [SerializeField] private Transform m_lineHolder;

    private Dictionary<string, GameObject> m_lines = new Dictionary<string, GameObject>();

    public void Cleanup()
    {
        foreach (var line in m_lines)
        {
            Destroy(line.Value);
        }

        m_lines.Clear();
    }

    public void SetName(string name)
    {
        m_characterNameText.text = name;
    }

    // Adds a line to the character sheet.
    public void AddLine(string lineText)
    {
        if (LineExists(lineText))
            return;

        var line = Instantiate(m_line, m_lineHolder);
        line.text = lineText;

        line.gameObject.SetActive(true);

        m_lines.Add(lineText, line.gameObject);
    }

    // Returns if the line has already been added.
    private bool LineExists(string lineText)
    {
        return m_lines.ContainsKey(lineText);
    }

    public void Action_Close()
    {
        PlayerController.Instance.cameraMovement = true;
        gameObject.SetActive(false);
    }
}
