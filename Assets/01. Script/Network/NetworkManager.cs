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

    private UIManager uimanager;

    void Awake() 
    {
        PhotonNetwork.ConnectUsingSettings();
        if(instance== null) { instance = this; }
    }
     


    public void Connect() => PhotonNetwork.ConnectUsingSettings();

    public override void OnConnectedToMaster() => PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions { MaxPlayers = 2 }, null);
    public override void OnJoinedRoom()
    {

    }

    // RPC �޼ҵ�
    [PunRPC]
    private void OpColor(Color opcolor)
    {
        DataManager.Instance.gamedata.opcolor = opcolor;
        Debug.Log("����Ǿ���");
    }

    public void SendMyColor(Color color)
    { 
        // RPC�� ȣ���Ͽ� �ٸ� �÷��̾�� ���� ���� �����մϴ�.
        photonView.RPC("OpColor", RpcTarget.All, color);
    }



    public void Disconnect() => PhotonNetwork.Disconnect();

    public override void OnDisconnected(DisconnectCause cause) => print("�������");



    

}
