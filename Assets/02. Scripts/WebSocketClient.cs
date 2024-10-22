using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _Singleton;
using WebSocketSharp;

public class WebSocketClient : Singleton<WebSocketClient>
{
    private WebSocket m_web_socket;

    private void Start()
    {
        m_web_socket = new WebSocket("ws://localhost:7777");
        m_web_socket.Connect();
        m_web_socket.OnOpen += RequestData;
        m_web_socket.OnMessage += Call;
    }

    private void RequestData(object sender, System.EventArgs event_arg)
    {
        Debug.Log("Server Address : " + ((WebSocket)sender).Url);
    }

    private void Call(object sender, MessageEventArgs event_arg)
    {
        Debug.Log("Player ID: " + event_arg.Data);
    }

    public void Send()
    {
        m_web_socket.Send("" + ", " + TimerCtrl.m_play_time.ToString());
    }
}