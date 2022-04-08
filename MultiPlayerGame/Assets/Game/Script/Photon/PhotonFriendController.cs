using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using PlayFabFriendInfo = PlayFab.ClientModels.FriendInfo;
using PhotonFriendInfo = Photon.Realtime.FriendInfo;
using UnityEngine;
using System;
using System.Linq;

public class PhotonFriendController : MonoBehaviourPunCallbacks
{
    public static Action<List<PhotonFriendInfo>> OnDisplayFriends;
    private void Awake()
    {
        PlayfabFriendController.OnFriendListUpdated += OnFriendListUpdateHanlder;
    }

    private void OnDestroy()
    {
        PlayfabFriendController.OnFriendListUpdated -= OnFriendListUpdateHanlder;
    }

    private void OnFriendListUpdateHanlder(List<PlayFabFriendInfo> friends)
    {
        if(friends.Count>0)
        {
            string[] friendDisplayName = friends.Select(f => f.TitleDisplayName).ToArray();
            PhotonNetwork.FindFriends(friendDisplayName);
        }
        else
        {
            List<PhotonFriendInfo> friendList = new List<PhotonFriendInfo>();
            OnDisplayFriends?.Invoke(friendList);
        }
    }

    public override void OnFriendListUpdate(List<PhotonFriendInfo> friendList)
    {
        OnDisplayFriends?.Invoke(friendList);
    }
}
