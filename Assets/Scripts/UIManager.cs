using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public bool InputBlocked => m_blocked;

    private bool m_blocked = false;

    public void BlockInput(bool value)
    {
        Cursor.SetCursor(GameManager.Instance.m_defaultCursor, Vector2.zero, CursorMode.Auto);
        m_blocked = value;
    }

    public void SetCursor(Texture2D cursor)
    {
        if(!m_blocked)
        {
            Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
        }
    }
}
