using Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Morpeh.Globals;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(DestroySystem))]
public sealed class DestroySystem : UpdateSystem {
    [SerializeField] private GlobalEvent destroyEvent;
    private Filter _filter;

    public override void OnAwake()
    {
        _filter = World.Filter.With<DestroyComponent>();
    }

    public override void OnUpdate(float deltaTime)
    {
        if (destroyEvent.IsPublished)
        {
            var destroys = this._filter.Select<DestroyComponent>();

            for (int i = 0, length = this._filter.Length; i < length; i++)
            {
                ref var destroyObj = ref destroys.GetComponent(i);

                if (destroyObj.IsDestroy)
                {
                    destroyObj.destroyEvent.Invoke();
                    Destroy(destroyObj.destroyObject);
                }
            }

            destroyEvent.Dispose();
        }
    }
}