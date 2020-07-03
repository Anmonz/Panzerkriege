using Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(MoveCollisionSystem))]
public sealed class MoveCollisionSystem : UpdateSystem {
    private Filter _filter;

    public override void OnAwake() 
    {
        _filter = World.Filter.With<CollisionComponent>().With<MovementComponent>();
    }

    public override void OnUpdate(float deltaTime)
    {
        var components = this._filter.Select<CollisionComponent>();
        var movers = this._filter.Select<MovementComponent>();

        for (int i = 0, length = this._filter.Length; i < length; i++)
        {
            ref var mover = ref movers.GetComponent(i);
            ref var component = ref components.GetComponent(i);

            RaycastHit2D[] hits = new RaycastHit2D[10];

            if (component.collider2D.Cast(mover.VectorMove, hits, component.collisionDistance) > 0)
            {
                mover.VectorMove = Vector3.zero;
            }
        }
    }
}