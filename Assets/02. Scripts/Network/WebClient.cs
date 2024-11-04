using System;
using System.Collections;
using System.Collections.Generic;
using _Singleton;
using UnityEngine;

#if UNITY_WEBGL
using System.Runtime.InteropServices;
#endif

public class PlayerData
{
    public long userId;
    public string gameCategory;
    public int score;
}

public class WebSocketClient : Singleton<WebSocketClient>
{
    public PlayerData m_data;

    [DllImport("__Internal")]
    private static extern long RequestUserId();

    [DllImport("__Internal")]
    private static extern void SendData(string json_data);

    private void Start()
    {
        m_data = new PlayerData();
        m_data.userId = RequestUserId();
        m_data.gameCategory = "Fruit";
    }

    public void Send()
    {
    #if UNITY_WEBGL && !UNITY_EDITOR
        SendData(JsonUtility.ToJson(m_data));
    #else
        Debug.Log("WebGL 환경이 아니기 때문에 점수 전송을 생략합니다.");
    #endif
    }
}
