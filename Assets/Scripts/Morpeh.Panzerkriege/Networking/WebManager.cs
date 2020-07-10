using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using TMPro;
using Photon.Pun.Demo.PunBasics;

public class WebManager : MonoBehaviourPunCallbacks
{
    private static WebManager instance;

    public static WebManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<WebManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(WebManager).Name;
                    instance = obj.AddComponent<WebManager>();
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this as WebManager;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public override void OnLeftRoom()
    {
        PhotonNetwork.LoadLevel(0);
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

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

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogWarningFormat("PUN Basics: OnDisconnected() was called by PUN with reason {0}", cause);

        // #Critical: The first we try to do is to join a potential existing room. If there is, good, else, we'll be called back with OnJoinRandomFailed()

        PhotonNetwork.LoadLevel(0);
    }

    public override void OnPlayerLeftRoom(Player other)
    {
        Debug.LogFormat("OnPlayerLeftRoom() {0}", other.NickName); // seen when other disconnects


        if (PhotonNetwork.IsMasterClient)
        {
            Debug.LogFormat("OnPlayerLeftRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom

            PhotonNetwork.LoadLevel(0);
        }
    }
}
