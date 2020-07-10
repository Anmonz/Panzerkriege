using Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Morpeh.Globals;
using System.Collections;
using System.Linq;

/// <summary>
/// Система стрельбы и перезарядки оружия
/// </summary>
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(GunSystem))]
public sealed class GunSystem : UpdateSystem {

    private Filter _filterGun; //Фильтр для GunComponent

    /// <summary>
    /// Устанавливает фильтр
    /// </summary>
    public override void OnAwake() {
        _filterGun = World.Filter.With<GunComponent>();
    }

    /// <summary>
    /// Проверяет перезарядку и метку стрельбы орудия
    /// </summary>
    /// <param name="deltaTime"></param>
    public override void OnUpdate(float deltaTime) {
        var guns = this._filterGun.Select<GunComponent>();

        for (int i = 0, length = this._filterGun.Length; i < length; i++)
        {
            ref var gun = ref guns.GetComponent(i);

            //Проверка на перезарядку орудия
            if (!gun.IsReloadGun)
            {
                if (gun.IsShoot) //Провека метки стрельбы
                {
                    //Убирание метки стрельбы с орудия
                    gun.IsShoot = false; 
                    //Вызов метода стрельбы для орудия
                    Fire(ref gun);
                }
            }
            else
            {
                //Проверка времени окончания перезарядки
                if (Time.time > gun.StartTimeReload + gun.timeReload)
                {
                    gun.IsReloadGun = false;
                }
            }
        }
    }


    /// <summary>
    /// Метод реализации стрельбы орудия
    /// Создает пулю 
    /// Задает начало перезарядки
    /// </summary>
    /// <param name="gun"></param>
    public void Fire(ref GunComponent gun)
    {
        //Создание пули через префаб в позиции орудия и получение ссылки на компонент пули
        ref var bullet = ref Instantiate(gun.bulletPrefab, gun.gunPosition.position, gun.gunPosition.rotation).GetComponent<BulletProvider>().GetData();
        //Установка направления пули
        bullet.Direction = gun.gunPosition.up;
        //Проигрывает звук выстрела
        gun.gunShootEvent.Invoke();
        //Задает метку и время начала перезарядки
        gun.IsReloadGun = true;
        gun.StartTimeReload = Time.time;
    }

}