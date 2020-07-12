using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

/// <summary>
/// Запускает активацию PUN2 и соединение с сервером
/// загружает сцену игры при подключении второго игрока
/// </summary>
public class WebLauncher : MonoBehaviourPunCallbacks
{

    private byte maxPlayersPerRoom = 2;//максимальное количество игроков

    private string gameVersion = "1";//версия игры

    private bool isConnecting = false;//метка подключения к серверу


    /// <summary>
    /// Устанавливает настройку PUN2 для автоматической синхронизации загрузки сцен
    /// </summary>
    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    /// <summary>
    /// Пытается подключится к серверу PUN2
    /// </summary>
    private void Update()
    {
        if(!isConnecting) Connect();
    }

    /// <summary>
    /// Устанавливает соединение с сервером PUN с стандартными настройками
    /// и задает версию игры
    /// </summary>
    private void Connect()
    {
        if (!PhotonNetwork.IsConnected)
        {
            isConnecting = PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = gameVersion;
        }
    }

    /// <summary>
    /// Обратная связь при подключении к серверу PUN
    /// </summary>
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinRandomRoom();//Вход в случаюную комнату PUN
    }

    /// <summary>
    /// Обратная связь при обрыве связи с сервером
    /// </summary>
    /// <param name="cause"></param>
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogWarningFormat("PUN Basics: OnDisconnected() was called by PUN with reason {0}", cause);

        //Снятие метки подключения к серверу
        if (isConnecting)
        {
            isConnecting = false;
        }
    }

    /// <summary>
    /// Обратная связь при ошибки входа в случайную комнату PUN (Нет свободных комнат)
    /// </summary>
    /// <param name="returnCode"></param>
    /// <param name="message"></param>
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("PUN Basics:OnJoinRandomFailed() was called by PUN. No random room available, so we create one.\nCalling: PhotonNetwork.CreateRoom");

        //Создание своей комнаты PUN
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxPlayersPerRoom });
    }

    /// <summary>
    /// Обратная связь при входе в комнату PUN
    /// </summary>
    public override void OnJoinedRoom()
    {
        Debug.Log("PUN Basics: OnJoinedRoom() called by PUN. Now this client is in a room.");

    }

    /// <summary>
    /// Обратная связь при подключении в вашу комнату игрока
    /// </summary>
    /// <param name="newPlayer"></param>
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.LogFormat("OnPlayerEnteredRoom() {0}", newPlayer.NickName); // not seen if you're the player connecting

        //Проверка на права пользователя комнаты
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.LogFormat("OnPlayerEnteredRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom

            //Загрузка уровня игры
            PhotonNetwork.LoadLevel(1);
        }
    }


}
