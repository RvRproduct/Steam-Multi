using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.EventSystems;
using Steamworks;
using Steamworks.Data;

public class ChatManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField MessageInputField;
    [SerializeField] private TextMeshProUGUI MessageTemplate;
    [SerializeField] private GameObject MessagesContainer;


    private void Start()
    {
        MessageTemplate.text = "";
    }

    private void OnEnable()
    {
        SteamMatchmaking.OnChatMessage += ChatSent;
        SteamMatchmaking.OnLobbyEntered += LobbyEntered;
        SteamMatchmaking.OnLobbyMemberJoined += LobbyMemberJoined;
        SteamMatchmaking.OnLobbyMemberLeave += LobbyMemberLeave;
    }

    private void OnDisable()
    {
        SteamMatchmaking.OnChatMessage -= ChatSent;
        SteamMatchmaking.OnLobbyEntered -= LobbyEntered;
        SteamMatchmaking.OnLobbyMemberJoined -= LobbyMemberJoined;
        SteamMatchmaking.OnLobbyMemberLeave -= LobbyMemberLeave;
    }

    private void LobbyMemberLeave(Lobby lobby, Friend friend) => AddMessageToBox(friend.Name + " Left the lobby ");

    private void LobbyMemberJoined(Lobby lobby, Friend friend) => AddMessageToBox(friend.Name + " Joined the lobby ");

    private void LobbyEntered(Lobby lobby) => AddMessageToBox("You entered the lobby");

    private void ChatSent(Lobby lobby, Friend friend, string msg)
    {
        AddMessageToBox(friend.Name + ": " + msg);

    }

    private void AddMessageToBox(string msg)
    {
        GameObject message = Instantiate(MessageTemplate.gameObject, MessagesContainer.transform);
        message.GetComponent<TextMeshProUGUI>().text = msg;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            ToggleChatBox();
        }
    }

    private void ToggleChatBox()
    {
        if (MessageInputField.gameObject.activeSelf)
        {
            if (!String.IsNullOrEmpty(MessageInputField.text))
            {
                LobbySaver.instance.currentLobby?.SendChatString(MessageInputField.text);
                MessageInputField.text = "";
            }

            MessageInputField.gameObject.SetActive(false);
            EventSystem.current.SetSelectedGameObject(null);
        }
        else
        {
            MessageInputField.gameObject.SetActive(true);
            EventSystem.current.SetSelectedGameObject(MessageInputField.gameObject);
        }
    }
}
