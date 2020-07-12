using Morpeh;
using Unity.IL2CPP.CompilerServices;
using Photon.Pun;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public sealed class HealthProvider : MonoProvider<HealthComponent>, IPunObservable
{

    /// <summary>
    /// Передает через PUN2 количество жизней
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="info"></param>
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(this.GetData().healthPoints);
        }
        else
        {
            // Network player, receive data
            this.GetData().healthPoints = (int)stream.ReceiveNext();
        }
    }
}