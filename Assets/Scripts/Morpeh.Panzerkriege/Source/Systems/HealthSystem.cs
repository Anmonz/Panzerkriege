using Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Morpeh.Globals;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(HealthSystem))]
public sealed class HealthSystem : UpdateSystem {
    [SerializeField] private GlobalEvent destroyEvent;
    private Filter _filter;

    public override void OnAwake()
    {
        _filter = World.Filter.With<HealthComponent>().With<DestroyComponent>();
    }

    public override void OnUpdate(float deltaTime)
    {
        var components = this._filter.Select<HealthComponent>();
        var destroys = this._filter.Select<DestroyComponent>();

        for (int i = 0, length = this._filter.Length; i < length; i++)
        {
            if (components.GetComponent(i).healthPoints <= 0)
            {
                destroys.GetComponent(i).IsDestroy = true;
                destroyEvent.Publish();
            }
        }
    }
}