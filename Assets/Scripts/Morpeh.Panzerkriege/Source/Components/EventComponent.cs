using Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Morpeh.Globals;
using UnityEngine.Events;

/// <summary>
/// Вызывает UnityEvent при Publish Morpeh.ECS.GlobalEventInt
/// </summary>
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[System.Serializable]
public struct EventComponent : IComponent {
    [SerializeField] private GlobalEventInt _currentGlobalEvent;    //Считываемое событие ECS
    [SerializeField] private UnityEvent _inspectorUnityEvent;       //Событие Unity срабатывающее при активации события ECS

    /// <summary>
    /// Считываемое событие ECS
    /// </summary>
    public GlobalEventInt CurentGloabalEvent
    {
        get => _currentGlobalEvent;
    }

    /// <summary>
    /// Событие Unity срабатывающее при активации события ECS
    /// </summary>
    public UnityEvent InspectorUnityEvent
    {
        get
        {
            if(_inspectorUnityEvent == null)
            {
                _inspectorUnityEvent = new UnityEvent();
            }

            return _inspectorUnityEvent;
        }
    }
}