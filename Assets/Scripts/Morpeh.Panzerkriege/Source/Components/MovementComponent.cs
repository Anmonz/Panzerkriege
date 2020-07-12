using Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using UnityEngine.Events;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[System.Serializable]
public struct MovementComponent : IComponent
{
    public float speed; //Скорость передвижения
    public UnityEvent moveStartEvent; //Событие начала движения
    public UnityEvent moveEndEvent; //Событие окончания движения
    public UnityEvent moveHitEvent; //Событие столкновения с препядствием

    private Vector3 _vectorMove;    //Вектор направления движения

    /// <summary>
    /// Вектор направления движения
    /// </summary>
    public Vector3 VectorMove
    {
        get => _vectorMove;
        set => _vectorMove = value;
    }
}