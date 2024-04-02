using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMCtrl : MonoBehaviour
{
    void Start()
    {
        SoundManager.m_instance.PlayerBGM("Background");
    }
}
