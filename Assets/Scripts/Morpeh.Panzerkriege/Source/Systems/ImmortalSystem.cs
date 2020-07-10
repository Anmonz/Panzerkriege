using Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;


/// <summary>
/// Система отключает безсмертие (Убирает компонент метки безсмертия) по заданному времени
/// </summary>
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(ImmortalSystem))]
public sealed class ImmortalSystem : UpdateSystem {

    [SerializeField] private float timeImmortal;//Время безсмертия

    private Filter _immortalFillter;//Фильтр безсмертия

    /// <summary>
    /// Устанавливает фильтр
    /// </summary>
    public override void OnAwake() {
        _immortalFillter = World.Filter.With<ImmortalMarkComponent>();
    }

    /// <summary>
    /// Проверяет окончание времени существования метки безсмертия и убирает ее
    /// </summary>
    /// <param name="deltaTime"></param>
    public override void OnUpdate(float deltaTime)
    {
        var immortals = _immortalFillter.Select<ImmortalMarkComponent>();

        for (int i = 0, length = this._immortalFillter.Length; i < length; i++)
        {
            ref var immortal = ref immortals.GetComponent(i);

            //Проверяет окончание времени безсмертия
            if(immortal.TimeStartImmortal + timeImmortal < Time.time)
            {
                //Убирает метку безсмертия с сущности хранящей метку безсмертия 
                _immortalFillter.GetEntity(i).RemoveComponent<ImmortalMarkComponent>();
            }
        }
    }
}