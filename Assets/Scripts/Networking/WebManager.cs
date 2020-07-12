using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using TMPro;
using Photon.Pun.Demo.PunBasics;

/// <summary>
/// Реализует паттер одиночка
/// Отслеживает состояние связи с сервером PUN
/// Создает объекты связаные по сети
/// </summary>
public class WebManager : MonoBehaviourPunCallbacks
{
    private static WebManager _instance; //Ссылка на единственный объект класса

    /// <summary>
    /// Передает ссылку на единственный объект класса
    /// Если он не задан ищет или создает новый
    /// </summary>
    public static WebManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<WebManager>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(WebManager).Name;
                    _instance = obj.AddComponent<WebManager>();
                }
            }
            return _instance;
        }
    }

    /// <summary>
    /// Проверяет текущий объект класса если он уже задат уничтажает лишнии
    /// </summary>
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this as WebManager;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Обратная связь при выходе из комнаты
    /// </summary>
    public override void OnLeftRoom()
    {
        PhotonNetwork.LoadLevel(0);
    }

    /// <summary>
    /// Метод реализующий выход из комнаты PUN
    /// </summary>
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    /// <summary>
    /// Метод создающий связаный объект через сервер PUN
    /// </summary>
    /// <param name="objectGame">Префаб</param>
    /// <param name="transform">Позиция создания объекта</param>
    /// <returns></returns>
    public GameObject Instantiate(GameObject objectGame, Transform transform)
    {
        return PhotonNetwork.Instantiate(objectGame.name, transform.position, transform.rotation, 0);
    }

    /// <summary>
    /// Передает номер игрока
    /// </summary>
    public int NumberPlayer
    {
        get
        {
            if(PhotonNetwork.IsMasterClient)
            {
                return 1;
            }
            else
            {
                return 2;
            }
        }
    }

    /// <summary>
    /// Обратная связь при разрыве соединения с сервером
    /// </summary>
    /// <param name="cause"></param>
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogWarningFormat("PUN Basics: OnDisconnected() was called by PUN with reason {0}", cause);

        // #Critical: The first we try to do is to join a potential existing room. If there is, good, else, we'll be called back with OnJoinRandomFailed()

        //Загрузка начального уровня лобби
        PhotonNetwork.LoadLevel(0);
    }

    /// <summary>
    /// Обратная связь при выходе игрока из комнаты
    /// </summary>
    /// <param name="other"></param>
    public override void OnPlayerLeftRoom(Player other)
    {
        Debug.LogFormat("OnPlayerLeftRoom() {0}", other.NickName); // seen when other disconnects

        //Проверка на мастера комнаты PUN 
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.LogFormat("OnPlayerLeftRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom

            //Загрузка начального уровня лобби
            PhotonNetwork.LoadLevel(0);
        }
    }
}
