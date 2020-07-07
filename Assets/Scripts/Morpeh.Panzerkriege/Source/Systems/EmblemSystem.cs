using Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Morpeh.Globals;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(EmblemSystem))]
public sealed class EmblemSystem : UpdateSystem {

    [SerializeField] private GlobalEvent EndGameEvent;

    private Filter _filter;

    public override void OnAwake()
    {
        _filter = World.Filter.With<HealthComponent>().With<EmblemComponent>();
    }

    public override void OnUpdate(float deltaTime)
    {
        var components = this._filter.Select<HealthComponent>();

        for (int i = 0, length = this._filter.Length; i < length; i++)
        {
            if (components.GetComponent(i).healthPoints <= 0)
            {
                EndGameEvent.Publish();
            }
        }
    }
}