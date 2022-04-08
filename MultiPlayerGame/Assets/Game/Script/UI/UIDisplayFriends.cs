using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDisplayFriends : MonoBehaviour
{
    [SerializeField]
    private Transform _friendContainer;
    [SerializeField]
    private UIFriend _uiFriendSample;

    private void Awake()
    {
        PhotonFriendController.OnDisplayFriends += OnDisplayFriendHandler;
    }

    private void OnDestroy()
    {
        PhotonFriendController.OnDisplayFriends -= OnDisplayFriendHandler;
    }
    private void OnDisplayFriendHandler(List<FriendInfo> obj)
    {
        foreach(Transform child in _friendContainer)
        {
            Destroy(child.gameObject);
        }

        foreach(FriendInfo friend in obj)
        {
            UIFriend uiFriend = Instantiate(_uiFriendSample, _friendContainer);
            uiFriend.Initialize(friend);
            uiFriend.gameObject.SetActive(true);
        }
    }

}
