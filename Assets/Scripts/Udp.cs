using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class Udp : MonoBehaviour
{
    public NoteList noteList;
    private UdpClient UDPrecv;
    private IPEndPoint endpoint;
    private byte[] recvBuf;
    private Thread recvThread;
    private bool messageReceive;
    // Start is called before the first frame update
    void Start()
    {
        UDPrecv = new UdpClient(new IPEndPoint(IPAddress.Any, 1111));
        endpoint = new IPEndPoint(IPAddress.Any, 0);

        recvThread = new Thread(new ThreadStart(RecvThread));
        recvThread.IsBackground = true;
        recvThread.Start();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void BroadThread(byte[] buf)
    {
        UdpClient UDPsend = new UdpClient(new IPEndPoint(IPAddress.Any, 0));
        IPEndPoint endpoint = new IPEndPoint(IPAddress.Broadcast, 1111);
        UDPsend.Send(buf, buf.Length, endpoint);
    }

    private void ReceiveCallback(IAsyncResult ar)
    {
        recvBuf = UDPrecv.EndReceive(ar, ref endpoint);
        string msg = Encoding.Default.GetString(recvBuf);
        messageReceive = true;
        Debug.Log("Udp.ReceiveCallback msg : " + msg);
        string[] sArray = msg.Split('|');
        noteList.SyncNote(sArray[0], sArray[1], sArray[2]);
    }

    private void RecvThread()
    {
        messageReceive = true;
        while (true)
        {
            if (messageReceive)
            {
                UDPrecv.BeginReceive(new AsyncCallback(ReceiveCallback), null);
                messageReceive = false;
            }
            else
            {
                Thread.Sleep(100);
            }
        }
    }
}