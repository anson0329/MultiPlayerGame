using Photon.Pun;
using Photon.Realtime;
using System;
using UnityEngine;

public class PhotonConnector : MonoBehaviourPunCallbacks
{
    public static Action OnGetPhotonFriends;

    #region Unity Method
    private void Start()
    {
        string nickName = PlayerPrefs.GetString("USERNAME");
        ConncetToPhoton(nickName);
    }
    #endregion

    #region Private Method
    private void ConncetToPhoton(string nickName)
    {
        Debug.Log($"Connect to Photon as {nickName}");

        PhotonNetwork.AuthValues = new AuthenticationValues(nickName);
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.NickName = nickName;
        PhotonNetwork.ConnectUsingSettings();
    }

    private void CreatePhotonRoom(string roomName)
    {
        RoomOptions ro = new RoomOptions();
        ro.IsOpen = true;
        ro.IsVisible = true;
        ro.MaxPlayers = 4;
        PhotonNetwork.JoinOrCreateRoom(roomName, ro, TypedLobby.Default);
    }
    #endregion

    #region Public Method
    #endregion

    #region Photon Callbacks
    public override void OnConnectedToMaster()
    {
        Debug.Log("你已經連線上Photon Master Server");

        if (!PhotonNetwork.InLobby)
        {
            PhotonNetwork.JoinLobby();
        }
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("你已經連線上Photon Lobby");
        OnGetPhotonFriends?.Invoke();
        //CreatePhotonRoom("TestRoom");
    }

    public override void OnCreatedRoom()
    {
        Debug.Log($"你已經創建了一個新房間，房間名：{PhotonNetwork.CurrentRoom.Name}");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log($"你加入了一個房間，房間名：{PhotonNetwork.CurrentRoom.Name}");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log($"你加入房間失敗，錯誤訊息：{message}");
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log($"其他玩家加入房間，玩家名：{newPlayer.NickName}");
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log($"其他玩家離開房間，玩家名：{otherPlayer.NickName}");
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        Debug.Log($"新的Master Client is 玩家名：{newMasterClient.NickName}");
    }
    #endregion
}
