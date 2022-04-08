using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayfabFriendController : MonoBehaviour
{
    public static Action<List<FriendInfo>> OnFriendListUpdated;

    private List<FriendInfo> _friendInfos;

    private void Awake()
    {
        _friendInfos = new List<FriendInfo>();
        PhotonConnector.OnGetPhotonFriends += OnGetPhotonFriendsHandler;
        UIAddFriend.OnAddFriend += OnAddFriendHandler;
        UIFriend.OnRemoveFriend += OnRemoveFriendHanlder;
    }

    private void OnDestroy()
    {
        PhotonConnector.OnGetPhotonFriends -= OnGetPhotonFriendsHandler;
        UIAddFriend.OnAddFriend -= OnAddFriendHandler;
        UIFriend.OnRemoveFriend -= OnRemoveFriendHanlder;
    }

    private void OnGetPhotonFriendsHandler()
    {
        GetPlayfabFriends();
    }

    private void OnAddFriendHandler(string name)
    {
        var request = new AddFriendRequest { FriendTitleDisplayName = name };
        PlayFabClientAPI.AddFriend(request, OnFriendAddedSucess, OnError);
    }

    private void OnRemoveFriendHanlder(string name)
    {
        //string id = _friendInfos.FirstOrDefault(f => f.TitleDisplayName == name).ToString();

        foreach (FriendInfo info in _friendInfos)
        {
            if (info.TitleDisplayName == name)
            {
                var request = new RemoveFriendRequest { FriendPlayFabId = info.FriendPlayFabId };
                PlayFabClientAPI.RemoveFriend(request, OnFriendRemoveSucess, OnError);
            }
        }
    }

    private void OnFriendRemoveSucess(RemoveFriendResult obj)
    {
        GetPlayfabFriends();
    }

    private void OnFriendAddedSucess(AddFriendResult obj)
    {
        GetPlayfabFriends();
    }

    private void GetPlayfabFriends()
    {
        var request = new GetFriendsListRequest { IncludeFacebookFriends = false, IncludeSteamFriends = false, XboxToken = null };
        PlayFabClientAPI.GetFriendsList(request, OnGetFriendsListSucess, OnError);
    }

    private void OnGetFriendsListSucess(GetFriendsListResult obj)
    {
        _friendInfos = obj.Friends;
        OnFriendListUpdated?.Invoke(obj.Friends);
    }

    private void OnError(PlayFabError obj)
    {
        Debug.Log($"Playfab好友更新失敗， {obj.GenerateErrorReport()}");
    }
}
