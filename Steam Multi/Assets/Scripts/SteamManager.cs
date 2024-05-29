using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;
using TMPro;
using Steamworks.Data;
using System;

public class SteamManager : MonoBehaviour
{


    [SerializeField] private TMP_InputField LobbyIDInputField;

    [SerializeField] private TextMeshProUGUI LobbyID;

    private void OnEnable()
    {
        SteamMatchmaking.OnLobbyCreated += LobbyCreated;
        SteamMatchmaking.OnLobbyEntered += LobbyEntered;
        SteamFriends.OnGameLobbyJoinRequested += GameLobbyJoinRequested;
    }

    private void OnDisable()
    {
        SteamMatchmaking.OnLobbyCreated -= LobbyCreated;
        SteamMatchmaking.OnLobbyEntered -= LobbyEntered;
        SteamFriends.OnGameLobbyJoinRequested -= GameLobbyJoinRequested;
    }

    private void GameLobbyJoinRequested(Lobby lobby, SteamId id)
    {
        throw new NotImplementedException();
    }

    private void LobbyEntered(Lobby lobby)
    {
        throw new NotImplementedException();
    }

    private void LobbyCreated(Result result, Lobby lobby)
    {
        throw new NotImplementedException();
    }

    public async void HostLobby()
    {
        await SteamMatchmaking.CreateLobbyAsync(4);
    }
    
    public async void JoinLobbyWithID()
    {
        ulong ID;
        if (ulong.TryParse(LobbyIDInputField.text, out ID)) { return; }

        Lobby[] lobbies = await SteamMatchmaking.LobbyList.WithSlotsAvailable(1).RequestAsync();

        foreach (Lobby lobby in lobbies)
        {
            if (lobby.Id == ID)
            {
                await lobby.Join();
                return;
            }
        }

    }

}
