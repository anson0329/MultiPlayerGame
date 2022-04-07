using PlayFab;
using PlayFab.ClientModels;
using System;
using UnityEngine;

public class PlayfabLogin : MonoBehaviour
{
    [SerializeField]
    private string _userName;

    #region Unity Methods
    private void Start()
    {
        if (string.IsNullOrEmpty(PlayFabSettings.TitleId))
        {
            PlayFabSettings.TitleId = "62482";
        }
    }
    #endregion

    #region Private Methods
    private bool IsValidUserName()
    {
        if(_userName.Length>=3 && _userName.Length<10)
        {
            return true;
        }
        return false;
    }

    private void LoginWithCustomID()
    {
        Debug.Log($"Login with Playfab as {_userName}");

        var request = new LoginWithCustomIDRequest { CustomId = _userName, CreateAccount = true };
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginCustomIDSucess, OnErrorCallbacks); 
    }

    private void UpdateDisplayName(string displayName)
    {
        Debug.Log($"Updating Playfab account's Display name to {displayName}");

        var request = new UpdateUserTitleDisplayNameRequest { DisplayName = displayName };
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnDisplayNameSucess, OnErrorCallbacks);
    }
    #endregion

    #region Public Methods
    public void SetUserName(string name)
    {
        _userName = name;
        PlayerPrefs.SetString("USERNAME", name);
    }

    public void LoginBtnClick()
    {
        if (!IsValidUserName()) return;

        LoginWithCustomID();
    }
    #endregion

    #region Playfab Callbacks

    private void OnErrorCallbacks(PlayFabError obj)
    {
        Debug.Log($"錯誤回報: {obj.GenerateErrorReport()}");
    }

    private void OnLoginCustomIDSucess(LoginResult obj)
    {
        Debug.Log($"使用{_userName}登入Playfab完成");
        UpdateDisplayName(_userName);
        SceneController.LoadScene("MainScene");
    }

    private void OnDisplayNameSucess(UpdateUserTitleDisplayNameResult result)
    {
        Debug.Log($"更新DisplayName of the Playfab帳號完成");
    }

    #endregion
}
