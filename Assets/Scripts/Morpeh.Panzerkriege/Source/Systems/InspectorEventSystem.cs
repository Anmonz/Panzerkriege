using Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;


/// <summary>
/// Система дублирования события ECS в UnityEvent
/// </summary>
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(InspectorEventSystem))]
public sealed class InspectorEventSystem : UpdateSystem {
    private Filter _filter; //Фильтер евентов

    /// <summary>
    /// Устанавливает фильтр
    /// </summary>
    public override void OnAwake() {
        _filter = World.Filter.With<EventComponent>();
    }

    /// <summary>
    /// Вызывает UnityEvent EventComponent при вызове GloabalEvent
    /// </summary>
    /// <param name="deltaTime"></param>
    public override void OnUpdate(float deltaTime) {
        var events = this._filter.Select<EventComponent>();

        for (int i = 0, length = this._filter.Length; i < length; i++)
        {
            ref var currentEvent = ref events.GetComponent(i);

            if(currentEvent.CurentGloabalEvent.IsPublished)
            {
                currentEvent.InspectorUnityEvent.Invoke();
            }
        }
    }
}