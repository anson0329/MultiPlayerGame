using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFriend : MonoBehaviour
{
    [SerializeField]
    private Text _friendNameText;
    [SerializeField]
    private FriendInfo _friendInfo;

    public static Action<string> OnRemoveFriend;

    public void Initialize(FriendInfo info)
    {
        _friendInfo = info;
        _friendNameText.text = info.UserId;
    }

    public void RemvoeFriend()
    {
        OnRemoveFriend?.Invoke(_friendInfo.UserId);
    }
}
