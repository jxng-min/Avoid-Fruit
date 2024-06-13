using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Threading;

public class ScoreCtrl : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI m_tmp;
    
    void OnEnable()
    {
        m_tmp.text = TimerCtrl.m_play_time.ToString("000");
    }
}
