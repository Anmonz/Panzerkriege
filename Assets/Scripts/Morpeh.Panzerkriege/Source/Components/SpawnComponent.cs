using Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[System.Serializable]
public struct SpawnComponent : IComponent {
    [SerializeField] private int _numberPlayer;


    public int NumberPlayer
    {
        get => _numberPlayer;
    }
}