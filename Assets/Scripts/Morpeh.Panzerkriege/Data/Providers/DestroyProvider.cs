using Morpeh;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public sealed class DestroyProvider : MonoProvider<DestroyComponent> {

    public void Start()
    {
        this.GetData().destroyObject = gameObject;
    }
}