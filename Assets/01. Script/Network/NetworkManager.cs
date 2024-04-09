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
    private void OpColor(float r, float g, float b)
    {
        DataManager.Instance.gamedata.opcolor = new Color(r,g,b);
        Debug.Log(r.ToString()+g.ToString()+b.ToString());
    }

    public void SendMyColor(float r, float g, float b)
    { 
        // RPC�� ȣ���Ͽ� �ٸ� �÷��̾�� ���� ���� �����մϴ�.
        photonView.RPC("OpColor", RpcTarget.All, r,g,b);
    }



    public void Disconnect() => PhotonNetwork.Disconnect();

    public override void OnDisconnected(DisconnectCause cause) => print("�������");



    

}
