using System;
using System.Text;
using SocketIOClient;
using UnityEngine;

public class Test : MonoBehaviour
{
    private SocketIO _client;
    private async void Start()
    {
        _client = new SocketIO("ws://s2.noj.tw:4567/socket.io/?EIO=4&transport=websocket"); 
        _client.OnConnected += async (sender, e) =>
        {
            await _client.EmitAsync("getServerName", new
            {
                source = "someone",
                bytes = Encoding.UTF8.GetBytes(".net core")
            });
        };
        _client.On("hello" , print);
        _client.On("getServerName", response =>
        {
            var result = response;
            print(result);
        });
        print("try to connect");
        await _client.ConnectAsync();
        print("connect ended");
    }

    private void Update()
    {
        if(_client.Connected)
            print("connected");
    }
}
