using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _Singleton;

public class SoundManager : Singleton<SoundManager>
{
    public AudioSource m_effect;

    private AudioClip m_button_click;
    private AudioClip m_player_die;

    private void Start()
    {
        m_button_click = Resources.Load<AudioClip>("Sounds/Button_Click");
        m_player_die = Resources.Load<AudioClip>("Sounds/Player_Die");
    }

    public void ButtonClick()
    {
        m_effect.PlayOneShot(m_button_click);
    }

    public void PlayerDie()
    {
        m_effect.PlayOneShot(m_player_die);
    }
}
