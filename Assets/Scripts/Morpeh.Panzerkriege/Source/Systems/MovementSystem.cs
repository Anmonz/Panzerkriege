using Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Morpeh.Globals;


/// <summary>
/// Система передвижения объектов с компонентами TransformComponent и MovementComponent
/// </summary>
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(MovementSystem))]
public sealed class MovementSystem : UpdateSystem {

    private Filter _filterMover;//Фильтр объектов передвижения

    /// <summary>
    /// Устанавливает фильтр объектов передвижения
    /// </summary>
    public override void OnAwake()
    {
        _filterMover = World.Filter.With<TransformComponent>().With<MovementComponent>(); 
    }

    /// <summary>
    /// Вызывает метод передвижения объекта
    /// </summary>
    /// <param name="deltaTime"></param>
    public override void OnUpdate(float deltaTime)
    {
        MoveTransform();
    }

    /// <summary>
    /// Метод передвижения объекта
    /// </summary>
    private void MoveTransform()
    {
        var movers = this._filterMover.Select<TransformComponent>();
        var speeds = this._filterMover.Select<MovementComponent>();

        for (int i = 0, length = this._filterMover.Length; i < length; i++)
        {
            ref var speed = ref speeds.GetComponent(i);
            //Проверка вектора передвижения
            if (speed.VectorMove != Vector3.zero)
            {
                //Передвижения объекта по верктору с заданной скоростью
                movers.GetComponent(i).transform.position += speed.VectorMove * speed.speed;
            }
        }
    }
}