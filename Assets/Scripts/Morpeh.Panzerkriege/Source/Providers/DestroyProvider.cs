using Morpeh;
using Unity.IL2CPP.CompilerServices;
using Photon.Pun;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public sealed class DestroyProvider : MonoProvider<DestroyComponent>, IPunObservable
{
    /// <summary>
    /// Добавляет текущий gameObject если он небыл задан в DestroyComponent.destroyObject
    /// </summary>
    public void Start()
    {
        if(this.GetData().destroyObject == null) this.GetData().destroyObject = gameObject;
    }
    
    /// <summary>
    /// Передает через PUN2 метку уничтожения объекта
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="info"></param>
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