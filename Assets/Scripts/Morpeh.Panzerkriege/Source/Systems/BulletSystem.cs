using Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

/// <summary>
/// Система передвижения пули
/// </summary>
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(BulletSystem))]
public sealed class BulletSystem : UpdateSystem {

    private Filter _filterBullet;//Фильтр пуль с трансформой

    /// <summary>
    /// Установка фильтра
    /// </summary>
    public override void OnAwake()
    {
        _filterBullet = World.Filter.With<TransformComponent>().With<BulletComponent>();
    }

    /// <summary>
    /// Вызов метода передвижения пули
    /// </summary>
    /// <param name="deltaTime"></param>
    public override void OnUpdate(float deltaTime)
    {
        MoveBullet(deltaTime);
    }

    /// <summary>
    /// Метод передвижения пули 
    /// относительно прошедшего времени
    /// и в заданном направлении
    /// </summary>
    /// <param name="deltaTime"></param>
    private void MoveBullet(float deltaTime)
    {
        var transforms = this._filterBullet.Select<TransformComponent>();
        var bullets = this._filterBullet.Select<BulletComponent>();

        for (int i = 0, length = this._filterBullet.Length; i < length; i++)
        {
            ref var bullet = ref bullets.GetComponent(i);
            ref var transform = ref transforms.GetComponent(i);

            if (bullet.Direction == Vector3.zero) bullet.Direction = transform.transform.up;
            //Установка новой позиции пули
            transform.transform.position += bullet.Direction * bullet.bulletSpeed * deltaTime;
        }
    }
}