using Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Morpeh.Globals;
using System;

/// <summary>
/// Система Уничтоения объектов при окончании установленного времени жизни
/// </summary>
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(LifeTimeSystem))]
public sealed class LifeTimeSystem : UpdateSystem {

    private Filter _filter;//Филььтр компонентов LifeTimeComponent DestroyComponent

    /// <summary>
    /// Устанавливает фильтр
    /// </summary>
    public override void OnAwake()
    {
        _filter = World.Filter.With<LifeTimeComponent>().With<DestroyComponent>();
    }

    /// <summary>
    /// Проверяет окончание времени жизни и задает метку уничтожения объекта
    /// </summary>
    /// <param name="deltaTime"></param>
    public override void OnUpdate(float deltaTime)
    {
        var components = this._filter.Select<LifeTimeComponent>();
        var destroys = this._filter.Select<DestroyComponent>();

        for (int i = 0, length = this._filter.Length; i < length; i++)
        {
            //Проверка времени жизни объекта при его уменьшениии
            if ((components.GetComponent(i).lifeTime -= deltaTime) < 0)
            {
                destroys.GetComponent(i).IsDestroy = true;//Установка метки уничтожения
            }
        }
    }
}