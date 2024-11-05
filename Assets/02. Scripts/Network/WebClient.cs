using System;
using System.Collections;
using System.Collections.Generic;
using _Singleton;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;


#if UNITY_WEBGL
using System.Runtime.InteropServices;
#endif

public class PlayerData
{
    public long userId;
    public string gameCategory;
    public int score;
}

public class WebClient : Singleton<WebClient>
{
    public PlayerData m_data;
    
    private static long m_user_id = 0;

    [DllImport("__Internal")]
    private static extern long RequestUserId();

    [DllImport("__Internal")]
    private static extern void SendData(string json_data);

    private void Start()
    {
        m_data = new PlayerData();

    if (Application.platform == RuntimePlatform.WebGLPlayer)
        RequestUserId();

        m_data.userId = m_user_id;
        m_data.gameCategory = "Fruit";
        Debug.Log("Unity에서 받은 유저 ID: " + m_data.userId);
    }
    
    public void OnUserIdReceived(string userIdString)
    {
        if (long.TryParse(userIdString, out long user_id))
            m_data.userId = user_id;
        else
            Debug.LogError("유저 ID 변환에 실패했습니다.");
    }
    public void Send()
    {
    #if UNITY_WEBGL && !UNITY_EDITOR
        SendData(JsonUtility.ToJson(m_data));
    #endif
    }
}
