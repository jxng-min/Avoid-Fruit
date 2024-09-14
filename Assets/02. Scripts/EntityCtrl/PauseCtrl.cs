using System.Collections;
using System.Collections.Generic;
using _EventBus;
using UnityEngine;

public class PauseCtrl : MonoBehaviour
{
    public void Pause()
    {
        PlayerCtrl player_ctrl = GameObject.FindObjectOfType<PlayerCtrl>();

        SoundManager.Instance.ButtonClick();

        if(GameManager.Instance.State == GameManager.GameState.PLAYING)
        {
            player_ctrl.m_animator.StartPlayback();

            GameEventBus.Publish(GameEventType.PAUSE);
        }
        else if(GameManager.Instance.State == GameManager.GameState.PAUSE)
        {
            player_ctrl.m_animator.StopPlayback();

            GameObject[] fruits = GameObject.FindGameObjectsWithTag("POOP");
            for(int i = 0; i < GameManager.Instance.m_fruit_velocity_vec.Count; i++)
                fruits[i].GetComponent<Rigidbody2D>().velocity = GameManager.Instance.m_fruit_velocity_vec[i];
            GameManager.Instance.m_fruit_velocity_vec.Clear();

            GameEventBus.Publish(GameEventType.PLAYING);
        }
    }
}
