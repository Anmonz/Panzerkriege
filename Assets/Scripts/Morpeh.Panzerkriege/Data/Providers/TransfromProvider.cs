using Morpeh;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public sealed class TransfromProvider : MonoProvider<TransformComponent> {

    private void Start()
    {
        if(this.GetData().transform == null) this.GetData().transform = this.transform;
    }
}