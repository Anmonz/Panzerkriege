using Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;


/// <summary>
/// Система проверки столкновения при передвижении
/// </summary>
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(MoveCollisionSystem))]
public sealed class MoveCollisionSystem : UpdateSystem {

    private Filter _filter;//Фильтр компонентов

    /// <summary>
    /// Устанавливает фильтр
    /// </summary>
    public override void OnAwake() 
    {
        _filter = World.Filter.With<CollisionComponent>().With<MovementComponent>();
    }

    /// <summary>
    /// Проверяет столкновение коллайдера при передвижении и останавливает движение
    /// </summary>
    /// <param name="deltaTime"></param>
    public override void OnUpdate(float deltaTime)
    {
        var components = this._filter.Select<CollisionComponent>();
        var movers = this._filter.Select<MovementComponent>();

        for (int i = 0, length = this._filter.Length; i < length; i++)
        {
            ref var mover = ref movers.GetComponent(i);
            ref var component = ref components.GetComponent(i);

            RaycastHit2D[] hits = new RaycastHit2D[10];

            //Проверяет столкновение коллайдера по вектору движения через установленную дистанцию
            if (component.collider2D.Cast(mover.VectorMove, hits, component.collisionDistance) > 0)
            {
                mover.VectorMove = Vector3.zero;//Остонавливает вектор движения
            }
        }
    }
}