using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Fusion;

public class LoginManager : MonoBehaviour
{
    [SerializeField] private NetworkRunner _networkRunnerPrefab = null;
    [SerializeField] private PlayerData _playerDataPrefab = null;
    [SerializeField] private InputField _roomName = null;
    [SerializeField] private string _gameSceneName = null;

    private NetworkRunner _runnerInstance = null;

    public InputField id;
    public InputField password;
    public Text notify;

    private void Start()
    {
        notify.text = "";
    }

    public void StartShardSession()
    {
        string roomName = string.IsNullOrEmpty(_roomName.text) ? "BasicRoom" : _roomName.text;

        SetPlayerData();
        StartGame(GameMode.Shared, roomName, _gameSceneName);
    }

    private void SetPlayerData()
    {
        PlayerData playerData = FindObjectOfType<PlayerData>();
        if(playerData == null)
            playerData = Instantiate(_playerDataPrefab);

        playerData.UseID = id.text;
    }

    private async void StartGame(GameMode mode, string roomName, string sceneName)
    {
        _runnerInstance = FindObjectOfType<NetworkRunner>();
        if (_runnerInstance == null)
            _runnerInstance = Instantiate(_networkRunnerPrefab);

        _runnerInstance.ProvideInput = true;

        var startGameArgs = new StartGameArgs()
        {
            GameMode = mode,
            SessionName = roomName,
            PlayerCount = 4,
        };

        StartGameResult res = await _runnerInstance.StartGame(startGameArgs);
        if (!res.Ok)
        {
            if (res.ShutdownReason == ShutdownReason.GameIsFull)
                notify.text = "방이 가득찼습니다.";
            else
                notify.text = res.ShutdownReason.ToString();
            return;
        }

        await _runnerInstance.StartGame(startGameArgs);

        _runnerInstance.SetActiveScene(sceneName);
    }

    public void SaveUserData()
    {
        if (!CheckInput(id.text, password.text))
            return;

        if (!PlayerPrefs.HasKey(id.text))
        {
            PlayerPrefs.SetString(id.text, password.text);
            PlayerPrefs.Save();
            notify.text = "아이디 생성이 완료됐습니다.";
        }
        else
        {
            notify.text = "이미 존재하는 아이디입니다.";
        }
    }

    public void CheckUserData()
    {
        if (!CheckInput(id.text, password.text))
            return;

        string pass = PlayerPrefs.GetString(id.text);

        if (password.text == pass)
        {
            //SceneManager.LoadScene(1);
            StartShardSession();
        }
        else
            notify.text = "입력하신 아이디와 패스워드가 일치하지 않습니다.";
    }

    bool CheckInput(string id, string pwd)
    {
        if (id == "" || pwd == "")
        {
            notify.text = "아이디 또는 패스워드를 입력해주세요.";
            return false;
        }
        else
        {
            return true;
        }
    }
}
