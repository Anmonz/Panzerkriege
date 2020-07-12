using Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using UnityEngine.Events;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[System.Serializable]
public struct DestroyComponent : IComponent
{
    public GameObject destroyObject;    //Уничтожающийся объект
    public UnityEvent destroyEvent;     //Событие уничтожения объекта

    private bool _isDestroy;            //Метка для уничтожения объекта

    /// <summary>
    /// Метка уничтожающая объект
    /// </summary>
    public bool IsDestroy
    {
        get => _isDestroy;
        set => _isDestroy = value;
    }
}