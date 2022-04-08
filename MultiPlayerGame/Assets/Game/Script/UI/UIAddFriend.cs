using System;
using UnityEngine;

public class UIAddFriend : MonoBehaviour
{
    [SerializeField]
    private string _displayName;

    public static Action<string> OnAddFriend;

    public void SetAddFriendName(string name)
    {
        _displayName = name;
    }

    public void AddFriend()
    {
        if(string.IsNullOrEmpty(_displayName))
        {
            return;
        }

        OnAddFriend?.Invoke(_displayName);
    }
}
