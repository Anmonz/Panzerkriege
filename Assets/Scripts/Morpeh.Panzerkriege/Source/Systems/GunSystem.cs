using Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Morpeh.Globals;
using System.Collections;
using System.Linq;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(GunSystem))]
public sealed class GunSystem : UpdateSystem {

    private Filter _filterGun;

    public override void OnAwake() {
        _filterGun = World.Filter.With<GunComponent>();
    }

    public override void OnUpdate(float deltaTime) {
        var guns = this._filterGun.Select<GunComponent>();

        for (int i = 0, length = this._filterGun.Length; i < length; i++)
        {
            ref var gun = ref guns.GetComponent(i);

            if (!gun.IsReloadGun)
            {
                if (gun.IsShoot)
                {
                    gun.IsShoot = false;
                    Fire(ref gun);
                }
            }
            else
            {
                if (Time.time > gun.StartTimeReload + gun.timeReload)
                {
                    gun.IsReloadGun = false;
                }
            }
        }
    }

    public void Fire(ref GunComponent gun)
    {
        Instantiate(gun.bulletPrefab, gun.gunPosition.position,gun.gunPosition.rotation);

        gun.IsReloadGun = true;
        gun.StartTimeReload = Time.time;
    }

}