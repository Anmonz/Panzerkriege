using Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Morpeh.Globals;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(BulletCollisionSystem))]
public sealed class BulletCollisionSystem : UpdateSystem {
    [SerializeField] private GlobalEvent destroyEvent;
    private Filter _filter;
    private Filter _filterColliders;

    public override void OnAwake()
    {
        _filter = World.Filter.With<CollisionComponent>().With<BulletComponent>().With<DestroyComponent>();
        _filterColliders = World.Filter.With<CollisionComponent>().With<HealthComponent>();
    }

    public override void OnUpdate(float deltaTime)
    {
        var components = this._filter.Select<CollisionComponent>();
        var bullets = this._filter.Select<BulletComponent>();
        var destroys = this._filter.Select<DestroyComponent>();

        for (int i = 0, length = this._filter.Length; i < length; i++)
        {
            ref var destroy = ref destroys.GetComponent(i);
            ref var bullet = ref bullets.GetComponent(i);
            ref var component = ref components.GetComponent(i);

            RaycastHit2D[] hits = new RaycastHit2D[10];

            if (component.collider2D.Cast(bullet.Direction, hits, component.collisionDistance) > 0)
            {
                DestroyObject(hits[0].collider, bullet);
                destroy.IsDestroy = true;
                destroyEvent.Publish();
            }
        }
    }

    public void DestroyObject(Collider2D colliderCheck, BulletComponent bullet)
    {
        var colliders = this._filterColliders.Select<CollisionComponent>();
        var healths = this._filterColliders.Select<HealthComponent>();

        for (int i = 0, length = this._filterColliders.Length; i < length; i++)
        {
            ref var health = ref healths.GetComponent(i);
            ref var collider = ref colliders.GetComponent(i);

            if (collider.collider2D == colliderCheck)
            {
                health.healthPoints -= bullet.damgeBullet;
            }
        }
    }
}