using Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Morpeh.Globals;
using System;

/// <summary>
/// Система проверки столкновения пуль
/// </summary>
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(BulletCollisionSystem))]
public sealed class BulletCollisionSystem : UpdateSystem {


    private Filter _filterBullets; //фильтр пуль

    /// <summary>
    /// Устанавливает фильтры
    /// </summary>
    public override void OnAwake()
    {
        _filterBullets = World.Filter.With<CollisionComponent>().With<BulletComponent>().With<DestroyComponent>();
    }

    /// <summary>
    /// Проверяет столкновение пуль
    /// </summary>
    /// <param name="deltaTime"></param>
    public override void OnUpdate(float deltaTime)
    {
        var components = this._filterBullets.Select<CollisionComponent>();
        var bullets = this._filterBullets.Select<BulletComponent>();
        var destroys = this._filterBullets.Select<DestroyComponent>();

        for (int i = 0, length = this._filterBullets.Length; i < length; i++)
        {
            ref var destroy = ref destroys.GetComponent(i);
            ref var bullet = ref bullets.GetComponent(i);
            ref var component = ref components.GetComponent(i);

            RaycastHit2D[] hits = new RaycastHit2D[10];

            //Проверка столкновения коллайдера пули с другими коллайдерами
            if (component.collider2D.Cast(bullet.Direction, hits, component.collisionDistance) > 0)
            {
                //Проверка коллайдера
                if (hits[0].collider != null)
                {
                    //Проверка на HealthProvider
                    var healthPVDR = hits[0].collider.gameObject.GetComponent<HealthProvider>();
                    if (healthPVDR != null)
                    {
                        //добавления урона к первому столкнувшемуся объекту
                        ref var healthCPNT = ref healthPVDR.GetData();
                        healthCPNT.Damages.Push(bullet.damgeBullet);
                    }
                }

                //проигрывание звука взрыва
                bullet.bulletExplosionEvent.Invoke();
                //Уничтожение пули
                destroy.IsDestroy = true;
            }
        }
    }
}