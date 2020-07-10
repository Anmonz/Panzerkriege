﻿using Morpeh;
using Unity.IL2CPP.CompilerServices;
using Photon.Pun;
using UnityEngine;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public sealed class TransfromProvider : MonoProvider<TransformComponent>, IPunObservable
{

    private void Start()
    {
        if(this.GetData().transform == null) this.GetData().transform = this.transform;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            Vector3 pos = this.GetData().transform.position;
            stream.Serialize(ref pos);
            Quaternion rot = this.GetData().transform.rotation;
            stream.Serialize(ref rot);
        }
        else
        {
            // Network player, receive data
            Vector3 pos = Vector3.zero;
            stream.Serialize(ref pos);
            this.GetData().transform.position = pos;
            Quaternion rot = Quaternion.identity;
            stream.Serialize(ref rot);
            this.GetData().transform.rotation = rot;
        }
    }
}