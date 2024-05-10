using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    #region Singleton form
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

    #endregion

    public void Connect() => PhotonNetwork.ConnectUsingSettings();

    public override void OnConnectedToMaster() => PhotonNetwork.JoinRandomRoom();

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        PhotonNetwork.CreateRoom("", new RoomOptions { MaxPlayers = 2 });
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        UIManager.Instance.InitWaitingPlayerPanel();
    }

    public override void OnJoinedRoom()
    {
        if(PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers)
        {
            CheckingMasterClient();
            SendisMaxRoom();
        }
    }

    public void CheckingMasterClient()
    {
        if (!PhotonNetwork.IsMasterClient) { DataManager.Instance.gamedata.myP = "P2"; }
        else { DataManager.Instance.gamedata.myP = "P1"; }
       
        Debug.Log(DataManager.Instance.gamedata.myP);
    }


    #region PUNRPC

    [PunRPC]
    private void RPCOpColor(float r, float g, float b, float a)
    {
        DataManager.Instance.gamedata.opcolor = new Color(r,g,b,a);
    }
    public void SendMyColor(float r, float g, float b, float a)
    {
        // RPC를 호출하여 다른 플레이어에게 변수 값을 전달합니다.
        photonView.RPC("RPCOpColor", RpcTarget.Others, r, g, b, a);
    }


    [PunRPC]
    private void RPCMaxRoom()
    {
        DataManager.Instance.gamedata.isMaxRoomTriger = true;
    }

    
    public void SendisMaxRoom()
    {
        photonView.RPC("RPCMaxRoom", RpcTarget.All);
    }

    [PunRPC]
    private void RPCSelectColorButton(int buttonIndex, bool interable)
    {   
        int tmp = UIManager.Instance.sendSelectButtonindex;
        if (tmp != -1){ UIManager.Instance.colorPallate[tmp].interactable = !interable; }

        UIManager.Instance.colorPallate[buttonIndex].interactable = interable;
        UIManager.Instance.sendSelectButtonindex = buttonIndex;
    }
    public void SendButtoninterable(int buttonIndex, bool interable)
    {
        photonView.RPC("RPCSelectColorButton", RpcTarget.Others, buttonIndex, interable);
    }



    #endregion

    public void Disconnect() => PhotonNetwork.Disconnect();

    public override void OnDisconnected(DisconnectCause cause) => print("연결끊김");



    

}
