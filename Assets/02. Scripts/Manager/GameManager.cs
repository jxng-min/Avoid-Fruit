using System.Collections;
using System.Collections.Generic;
using _Singleton;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : Singleton<GameManager>
{
    // 게임 상태 관련 변수
    public enum GameState
    {
        SETTING = 0,
        PLAYING = 1,
        PAUSE = 2,
        DEAD = 3
    }

    public static GameState game_state;

    public GameObject m_state_canvas;
    public GameObject m_pause_panel;
    public GameObject m_dead_panel;

    void Start()
    {
        DontDestroyOnLoad(m_state_canvas);

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
        else
        {
            m_pause_panel.SetActive(false);
            m_dead_panel.SetActive(false);
            TimerCtrl.m_play_time = 0f;
            Time.timeScale = 1.0f;
        }
    }
}
