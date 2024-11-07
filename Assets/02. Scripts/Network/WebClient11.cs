using System;
using System.Collections;
using System.Collections.Generic;
using _Singleton;
using UnityEngine;

#if UNITY_WEBGL
using System.Runtime.InteropServices;
#endif

[System.Serializable]
public class ScoreData
{
    public int score;
}

public class WebClient11 : Singleton<WebClient11>
{
    [DllImport("__Internal")]
    private static extern void SendData(string json_data);

    public void Send(int score)
    {
        ScoreData score_data = new ScoreData();
        score_data.score = score;
        Debug.Log(JsonUtility.ToJson(score_data));

    #if UNITY_WEBGL && !UNITY_EDITOR
        SendData(JsonUtility.ToJson(score_data));
    #endif
    }
}
