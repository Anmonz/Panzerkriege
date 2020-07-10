using Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[System.Serializable]
public struct ImmortalMarkComponent : IComponent {

    private float _timeStartImmortal;
    private float _timeImmortal;

    public float TimeStartImmortal
    {
        get => _timeStartImmortal;
        set => _timeStartImmortal = value;
    }

    public float TimeImmortal
    {
        get => _timeImmortal;
        set => _timeImmortal = value;
    }
}