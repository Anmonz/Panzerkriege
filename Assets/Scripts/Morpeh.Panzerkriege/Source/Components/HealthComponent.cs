﻿using Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using System.Collections.Generic;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[System.Serializable]
public struct HealthComponent : IComponent {
    public int healthPoints;    //Количество пунктов жизни

    private Stack<int> _damages;//Стек набора полученных повреждений

    /// <summary>
    /// Стек набора полученных повреждений
    /// </summary>
    public Stack<int> Damages
    {
        get
        {
            if(_damages == null)
            {
                _damages = new Stack<int>();
                return _damages;
            }
            else
            {
                return _damages;
            }
        }
    }

}