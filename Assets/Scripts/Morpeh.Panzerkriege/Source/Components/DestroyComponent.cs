using Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using UnityEngine.Events;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[System.Serializable]
public struct DestroyComponent : IComponent {
    public GameObject destroyObject;
    public UnityEvent destroyEvent;

    private bool _isDestroy;

    public bool IsDestroy
    {
        get => _isDestroy;
        set => _isDestroy = value;
    }
}