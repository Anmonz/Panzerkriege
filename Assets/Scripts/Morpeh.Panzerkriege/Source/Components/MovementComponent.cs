using Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[System.Serializable]
public struct MovementComponent : IComponent {
    public float speed;

    private Vector3 _vectorMove;

    public Vector3 VectorMove
    {
        get => _vectorMove;
        set => _vectorMove = value;
    }
}