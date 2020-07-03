using Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Morpeh.Globals;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(MovementSystem))]
public sealed class MovementSystem : UpdateSystem {
    private Filter _filterMover;

    public override void OnAwake()
    {
        _filterMover = World.Filter.With<TransformComponent>().With<MovementComponent>(); 
    }

    public override void OnUpdate(float deltaTime)
    {
        MoveTransform(deltaTime);
    }

    private void MoveTransform(float deltaTime)
    {
        var movers = this._filterMover.Select<TransformComponent>();
        var speeds = this._filterMover.Select<MovementComponent>();

        for (int i = 0, length = this._filterMover.Length; i < length; i++)
        {
            ref var speed = ref speeds.GetComponent(i);
            if (speed.VectorMove != Vector3.zero)
            {
                movers.GetComponent(i).transform.position += speed.VectorMove * speed.speed;
            }
        }
    }
}