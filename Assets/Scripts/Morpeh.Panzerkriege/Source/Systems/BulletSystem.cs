using Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(BulletSystem))]
public sealed class BulletSystem : UpdateSystem {

    private Filter _filterBullet;

    public override void OnAwake()
    {
        _filterBullet = World.Filter.With<TransformComponent>().With<BulletComponent>();
    }

    public override void OnUpdate(float deltaTime)
    {
        MoveBullet(deltaTime);
    }

    private void MoveBullet(float deltaTime)
    {
        var transforms = this._filterBullet.Select<TransformComponent>();
        var bullets = this._filterBullet.Select<BulletComponent>();

        for (int i = 0, length = this._filterBullet.Length; i < length; i++)
        {
            ref var bullet = ref bullets.GetComponent(i);
            ref var transform = ref transforms.GetComponent(i);

            bullet.Direction = transform.transform.up;
            transform.transform.position += bullet.Direction * bullet.bulletSpeed * deltaTime;
        }
    }
}