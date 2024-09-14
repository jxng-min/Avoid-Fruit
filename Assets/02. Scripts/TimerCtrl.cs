using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class TimerCtrl : MonoBehaviour
{
    public static float m_play_time;
    [SerializeField] private TextMeshProUGUI m_tmp;

    void Update()
    {
        if(GameManager.Instance.State == GameManager.GameState.PLAYING)
        {
            if(m_play_time >= 999)
                m_play_time = 999f;
            else
                m_play_time += Time.deltaTime;

            m_tmp.text = m_play_time.ToString("000");
        }

    }
}
