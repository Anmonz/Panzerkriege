using Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using UnityEngine.Events;
using Photon.Pun;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[System.Serializable]
public struct DestroyComponent : IComponent, IPunObservable
{
    public GameObject destroyObject;
    public UnityEvent destroyEvent;

    private bool _isDestroy;

    public bool IsDestroy
    {
        get => _isDestroy;
        set => _isDestroy = value;
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(this._isDestroy);
        }
        else
        {
            // Network player, receive data
            this._isDestroy = (bool)stream.ReceiveNext();
        }
    }
}