using Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using System;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[System.Serializable]
public struct PlayerComponent : IComponent {
    [SerializeField] private int _numberPlayer;//Номер игрока 
    
    /// <summary>
    /// Номер игрока
    /// </summary>
    public int NumberPlayer
    {
        get => _numberPlayer;
    }
}