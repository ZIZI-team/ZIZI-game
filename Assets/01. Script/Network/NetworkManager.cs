using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    private static NetworkManager instance;

    public static NetworkManager Instance
    {
        get
        {
            if(instance == null) { return null; } return instance;
        }
    }



    void Awake() 
    {
        PhotonNetwork.ConnectUsingSettings();
        if(instance== null) { instance = this; }
    }
     


    public void Connect() => PhotonNetwork.ConnectUsingSettings();

    public override void OnConnectedToMaster() => PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions { MaxPlayers = 2 }, null);
    public override void OnJoinedRoom()
    {
        if(PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers)
        {
            SendMaxRoom();
        }
    }

    // RPC �޼ҵ�
    [PunRPC]
    private void SaveOpColor(float r, float g, float b, float a)
    {
        DataManager.Instance.gamedata.opcolor = new Color(r,g,b,a);
        Debug.Log(a.ToString());
    }

    [PunRPC]
    private void MaxRoom()
    {
        UIManager.Instance.waitingPlayerPanel.SetActive(false);   
        UIManager.Instance.selectColorPanel.SetActive(true);
    }

    public void SendMyColor(float r, float g, float b, float a)
    { 
        // RPC�� ȣ���Ͽ� �ٸ� �÷��̾�� ���� ���� �����մϴ�.
        photonView.RPC("SaveOpColor", RpcTarget.Others, r,g,b,a);
    }

    public void SendMaxRoom()
    {
        photonView.RPC("MaxRoom", RpcTarget.All);
    }
    


    public void Disconnect() => PhotonNetwork.Disconnect();

    public override void OnDisconnected(DisconnectCause cause) => print("�������");



    

}