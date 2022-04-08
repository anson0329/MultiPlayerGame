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
        Debug.Log("�A�w�g�s�u�WPhoton Master Server");

        if (!PhotonNetwork.InLobby)
        {
            PhotonNetwork.JoinLobby();
        }
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("�A�w�g�s�u�WPhoton Lobby");
        OnGetPhotonFriends?.Invoke();
        //CreatePhotonRoom("TestRoom");
    }

    public override void OnCreatedRoom()
    {
        Debug.Log($"�A�w�g�ЫؤF�@�ӷs�ж��A�ж��W�G{PhotonNetwork.CurrentRoom.Name}");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log($"�A�[�J�F�@�өж��A�ж��W�G{PhotonNetwork.CurrentRoom.Name}");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log($"�A�[�J�ж����ѡA���~�T���G{message}");
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log($"��L���a�[�J�ж��A���a�W�G{newPlayer.NickName}");
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log($"��L���a���}�ж��A���a�W�G{otherPlayer.NickName}");
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        Debug.Log($"�s��Master Client is ���a�W�G{newMasterClient.NickName}");
    }
    #endregion
}
