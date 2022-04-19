using System;
using UnityEngine;
using UnityWebSockets;

[RequireComponent(typeof(WebsocketFactory))]
public class Heart : MonoBehaviour
{
    private class HeartData
    {
        public int heartBeat { get; private set; }

        public HeartData(int heartBeat)
        {
            this.heartBeat = heartBeat;
        }
    }

    private const int highest = 210, lowest = 40;
    public float Interpolated => Mathf.InverseLerp(lowest, highest, HeartRate);

    public static Heart Instance { get; private set; }
    public bool HasStarted => socket.IsOpen;

    public int HeartRate { get; private set; }

    private WebsocketWrapper socket;

    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(this);
        }

        WebsocketFactory factory = GetComponent<WebsocketFactory>();
        socket = factory.CreateWebSocket("wss://192.168.68.49:8080/");
        socket.OnClose += Reconnect;
        socket.OnMessage += HandleData;
        socket.Connect();
        HandleData( null,"{'heartBeat': 10}");
    }

    private void HandleData(object sender, string data)
    {
        var response = JsonUtility.FromJson<HeartData>(data);
        HeartRate = response.heartBeat;
        Debug.Log(HeartRate);
    }

    private void Reconnect(object o, EventArgs e)
    {
        Debug.Log("Reconnect");
        socket.Connect();
    }
}