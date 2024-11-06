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

public class WebClient11 : Singleton<WebClient11>
{
    public PlayerData m_data;

    [DllImport("__Internal")]
    private static extern void RequestUserId();

    [DllImport("__Internal")]
    private static extern void SendData(string json_data);

    private void Start()
    {
        m_data = new PlayerData();
        m_data.gameCategory = "Fruit";

    #if UNITY_WEBGL && !UNITY_EDITOR
        RequestUserId();
    #endif
    }

    public void OnUserIdReceived(string user_id)
    {
        if (long.TryParse(user_id, out long parsed_user_id))
            m_data.userId = parsed_user_id;
    }

    public void Send()
    {
    #if UNITY_WEBGL && !UNITY_EDITOR
        SendData(JsonUtility.ToJson(m_data));
    #endif
    }
}
