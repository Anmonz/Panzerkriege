using Morpeh;
using Unity.IL2CPP.CompilerServices;
using Photon.Pun;
using UnityEngine;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public sealed class MovementProvider : MonoProvider<MovementComponent>, IPunObservable
{
    /// <summary>
    /// Передает через PUN2 вектор движенния
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="info"></param>
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(this.GetData().VectorMove);
        }
        else
        {
            // Network player, receive data
            this.GetData().VectorMove = (Vector3)stream.ReceiveNext();
        }
    }
}