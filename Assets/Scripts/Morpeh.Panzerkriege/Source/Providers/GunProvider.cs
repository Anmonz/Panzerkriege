using Morpeh;
using Unity.IL2CPP.CompilerServices;
using Photon.Pun;


[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public sealed class GunProvider : MonoProvider<GunComponent>, IPunObservable
{
    /// <summary>
    /// Передает через PUN2 метку совершения выстрела
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="info"></param>
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(this.GetData().IsShoot);
        }
        else
        {
            // Network player, receive data
            this.GetData().IsShoot = (bool)stream.ReceiveNext();
        }
    }
}