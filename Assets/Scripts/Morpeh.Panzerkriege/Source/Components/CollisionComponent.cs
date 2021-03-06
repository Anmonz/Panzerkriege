﻿using Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[System.Serializable]
public struct CollisionComponent : IComponent {
    public Collider2D collider2D;   //Коллайдер
    public float collisionDistance; //Растояние проверки коллизии (BulletCollisionSystem, MoveCollisionSystem)
}