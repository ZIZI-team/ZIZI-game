using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

/// <summary>
/// Networking를 담당하는 Class
/// </summary>
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
        if(instance== null) 
        {
            instance = this;
            if (transform.parent != null && transform.root != null)
            {
                DontDestroyOnLoad(this.transform.root.gameObject);
            }
            else
            {
                DontDestroyOnLoad(this.gameObject);
            }
        }
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
        CheckIfMasterClient();
        if (PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers)
        {
            SendisMaxRoom();
        }
    }

    /// <summary>
    /// MasterClient면 P1를 할당 하고 client면 P2를 할당 하는 함수
    /// </summary>
    private void CheckIfMasterClient()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            DataManager.Instance.gamedata.myP = "P1";
            Debug.Log(DataManager.Instance.gamedata.myP);
        }
        else
        {
            DataManager.Instance.gamedata.myP = "P2";
            Debug.Log(DataManager.Instance.gamedata.myP);
        }
    }

    #region PUNRPC

    [PunRPC]
    private void RPCOpColor(float r, float g, float b, float a)
    {
        DataManager.Instance.gamedata.opcolor = new Color(r,g,b,a);
    }
    /// <summary>
    /// 나의 색상을 상대에게 보내는 함수
    /// </summary>
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
    /// <summary>
    /// 방이 다 차면 이 함수를 호출하여 게임이 시작 될 준비를 함.
    /// </summary>
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
    /// <summary>
    /// 서로 색상을 곁치지 않게 하기 위한 함수.
    /// 내가 선택한 색상을 이 함수를 통해 상대에게 선택 할 수 없게 만듬.
    /// </summary>
    /// <param name="buttonIndex">6개의 색상 중 선택한 button의 Index</param>
    public void SendButtoninterable(int buttonIndex, bool interable)
    {
        photonView.RPC("RPCSelectColorButton", RpcTarget.Others, buttonIndex, interable);
    }

    [PunRPC]
    private void RPCSetTile()
    {
        TileManager.Instance.SetTile(0);
    }
    /// <summary>
    /// Online 게임 시작 시 TileMap를 생성해주는 함수.
    /// </summary>
    public void SendSetTile()
    {
        photonView.RPC("RPCSetTile", RpcTarget.Others);
    }

    [PunRPC]
    private void RPCStonePosition(string player ,int x, int y, int z)
    {
        TileManager.Instance.InstallStone(player, new Vector3Int(x, y, z));
    }
    /// <summary>
    /// 내가 선택한 위치에 돌을 올려 놓는 함수.
    /// </summary>
    /// <param name="player">P1 or P2</param>
    /// <param name="cellPos">선택한 위치의 값</param>
    public void SendStonePosition(string player,Vector3Int cellPos) 
    {
        photonView.RPC("RPCStonePosition", RpcTarget.All, player, cellPos.x, cellPos.y, cellPos.z);
    }

    [PunRPC]
    private void RPCEndGame()
    {
        UIManager.Instance.InstantiatePausedPanel();
    }
    /// <summary>
    /// 게임이 끝났다고 Network 해주는 함수.
    /// </summary>
    public void SendRPCEndGame()
    {
        photonView.RPC("RPCEndGame", RpcTarget.Others);
    }

    [PunRPC]
    private void PRCchangeTurn()
    {
        GameSystem.Instance.changeTurn();
    }
    /// <summary>
    /// 턴의 변경을 Network 해주는 함수.
    /// </summary>
    public void SendChangeTurn()
    {
        photonView.RPC("PRCchangeTurn", RpcTarget.All);
    }
    #endregion

    public void Disconnect() => PhotonNetwork.Disconnect();

    public override void OnDisconnected(DisconnectCause cause) => print("연결끊김");

}
