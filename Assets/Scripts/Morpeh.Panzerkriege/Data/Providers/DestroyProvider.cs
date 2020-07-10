using Morpeh;
using Unity.IL2CPP.CompilerServices;
using Photon.Pun;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public sealed class DestroyProvider : MonoProvider<DestroyComponent>, IPunObservable
{

    public void Start()
    {
        if(this.GetData().destroyObject == null) this.GetData().destroyObject = gameObject;
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(this.GetData().IsDestroy);
        }
        else
        {
            // Network player, receive data
            this.GetData().IsDestroy = (bool)stream.ReceiveNext();
        }
    }
}