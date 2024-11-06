using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using _Singleton;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using System.Net;

public class GameManager : Singleton<GameManager>
{
    public enum GameState
    {
        SETTING, PLAYING, PAUSE, DEAD
    }

    public GameState State { get; private set;}
    public GameObject m_state_canvas;
    public GameObject m_pause_panel;
    public GameObject m_dead_panel;

    [SerializeField] 
    private JoyStickValue m_joy_value;

    public List<Vector2> m_fruit_velocity_vec;

    private void Start()
    {
        DontDestroyOnLoad(m_state_canvas);
        Setting();
    }

    public void Setting()
    {
        State = GameState.SETTING;

        m_pause_panel.SetActive(false);
        m_dead_panel.SetActive(false);

        TimerCtrl.m_play_time = 0f;
    }

    public void Playing()
    {
        State = GameState.PLAYING;

        m_pause_panel.SetActive(false);
        m_dead_panel.SetActive(false);
    }

    public void Pause()
    {
        State = GameState.PAUSE;

        PlayerCtrl player_ctrl = GameObject.FindObjectOfType<PlayerCtrl>();
        player_ctrl.m_rigidbody.velocity = new Vector2(0f, 0f);

        GameObject[] fruits = GameObject.FindGameObjectsWithTag("POOP");
        for(int i = 0; i < fruits.Length; i++)
        {
            m_fruit_velocity_vec.Add(fruits[i].GetComponent<Rigidbody2D>().velocity);
            fruits[i].GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
        m_pause_panel.SetActive(true);
    }

    public void Dead()
    {
        State = GameState.DEAD;

        GameObject joy_stick = GameObject.FindGameObjectWithTag("JOYSTICK");
        Destroy(joy_stick);

        m_joy_value.m_joy_touch = Vector2.zero;

        PlayerCtrl player_ctrl = FindObjectOfType<PlayerCtrl>();
        player_ctrl.m_rigidbody.velocity = Vector2.zero;
        player_ctrl.DeadPlayer();

        GameObject[] fruits = GameObject.FindGameObjectsWithTag("POOP");
        foreach(GameObject fruit in fruits)
            fruit.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        m_fruit_velocity_vec.Clear();

        m_dead_panel.SetActive(true);

        WebClient11.Instance.m_data.score = Convert.ToInt32(TimerCtrl.m_play_time);
        WebClient11.Instance.Send();
    }
}
