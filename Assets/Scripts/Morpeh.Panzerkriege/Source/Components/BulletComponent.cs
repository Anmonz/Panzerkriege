using Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using UnityEngine.Events;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[System.Serializable]
public struct BulletComponent : IComponent {
    public float bulletSpeed;               //Скорость пули
    public int damgeBullet;                 //Урон пули
    public UnityEvent bulletExplosionEvent; //Событие уничтожения пули при попадании в объект

    private Vector3 _direction; //Направление пули

    /// <summary>
    /// Направление пули
    /// </summary>
    public Vector3 Direction
    {
        get => _direction;
        set => _direction = value;
    }
}