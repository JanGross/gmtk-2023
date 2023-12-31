using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Image fadeRect;
    public DialogueController dialogueController;
    public UIManager uiManager;

    public Texture2D m_talkCursor, m_lookCursor, m_defaultCursor;

    [SerializeField] private float m_fadeT;
    private float m_fadeTotal;
    [SerializeField] private bool m_showFade = false;
    [SerializeField] private float m_fadeDuration = 1;
    private Action m_fadeCallback;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("There can only be one instance of the CharacterManager class");
        }
    }

    private void Update()
    {
        //Fade image towrads target
        if(m_showFade)
        {
            if (m_fadeTotal < m_fadeDuration / 2)
            {
                m_fadeT += Time.deltaTime;
            } else
            {
                m_fadeCallback?.Invoke();
                m_fadeCallback = null;
                m_fadeT -= Time.deltaTime;
            }
            m_fadeTotal += Time.deltaTime;
            
            
            if(m_fadeT < 0)
            {
                m_showFade = false;
                m_fadeT = 0;
                m_fadeTotal = 0;
            }

            fadeRect.color = new Color(0f, 0f, 0f, Mathf.Lerp(0, 1, m_fadeT));
        }
    }

    public void FadePingPong(Action callback = null)
    {
        m_fadeCallback = callback;
        m_fadeTotal = 0;
        m_showFade = true;
    }

    public void ShowGameResultScene(bool success)
    {
        SceneManager.LoadScene(success ? "WinScene" : "LossScene");
    }
}
