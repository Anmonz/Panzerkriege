using Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Morpeh.Globals;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(StartTimerSystem))]
public sealed class StartTimerSystem : UpdateSystem {

    [SerializeField] private GlobalEvent startEvent;
    [SerializeField] private float startTimer;

    private Filter _filter;
    private float _startTime;

    public override void OnAwake() {
        _startTime = Time.time;
    }

    public override void OnUpdate(float deltaTime) {
        if (Time.time > (_startTime + startTimer))
        {
            startEvent.Publish();
        }
    }
}