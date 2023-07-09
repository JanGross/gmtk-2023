using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public bool InputBlocked => m_blocked;

    private bool m_blocked = false;

    public void BlockInput(bool value)
    {
        m_blocked = value;
    }
}
