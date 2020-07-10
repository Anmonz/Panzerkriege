using Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Morpeh.Globals;
using UnityEngine.Events;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[System.Serializable]
public struct EventComponent : IComponent {
    [SerializeField] private GlobalEvent _currentGlobalEvent;
    [SerializeField] private UnityEvent _inspectorUnityEvent;

    public GlobalEvent CurentGloabalEvent
    {
        get => _currentGlobalEvent;
    }

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