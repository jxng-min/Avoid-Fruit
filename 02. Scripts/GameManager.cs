using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        SETTING = 0,
        PLAYING = 1,
        PAUSE = 2,
        DEAD = 3
    }

    public static GameState game_state;

    [SerializeField]
    private GameObject m_pause_panel;

    [SerializeField]
    private GameObject m_dead_panel;

    void Start()
    {
        Time.timeScale = 1.0f;
        TimerCtrl.m_play_time = 0f;
        game_state = GameState.SETTING;
    }

    void Update()
    {
        if(game_state == GameState.PLAYING)
        {
            m_pause_panel.SetActive(false);
            m_dead_panel.SetActive(false);
            Time.timeScale = 1.0f;
        }
        else if(game_state == GameState.PAUSE)
        {
            m_pause_panel.SetActive(true);
            Time.timeScale = 0.0f;        
        }
        else if(game_state == GameState.DEAD)
        {
            m_dead_panel.SetActive(true);
            Time.timeScale = 0.0f;
        }
    }
}
