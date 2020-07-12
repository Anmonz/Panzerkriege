using Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;


/// <summary>
/// Система отключает бессмертие (Убирает компонент метки бессмертия) по заданному времени
/// </summary>
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(ImmortalSystem))]
public sealed class ImmortalSystem : UpdateSystem {

    [SerializeField] private float timeImmortal;//Время бессмертия

    private Filter _immortalFillter;//Фильтр бессмертия

    /// <summary>
    /// Устанавливает фильтр
    /// </summary>
    public override void OnAwake() {
        _immortalFillter = World.Filter.With<ImmortalMarkComponent>();
    }

    /// <summary>
    /// Проверяет окончание времени существования метки бессмертия и убирает ее
    /// </summary>
    /// <param name="deltaTime"></param>
    public override void OnUpdate(float deltaTime)
    {
        var immortals = _immortalFillter.Select<ImmortalMarkComponent>();

        for (int i = 0, length = this._immortalFillter.Length; i < length; i++)
        {
            ref var immortal = ref immortals.GetComponent(i);

            //Проверяет окончание времени бессмертия
            if(immortal.TimeStartImmortal + timeImmortal < Time.time)
            {
                //Убирает метку бессмертия с сущности хранящей метку бессмертия 
                _immortalFillter.GetEntity(i).RemoveComponent<ImmortalMarkComponent>();
            }
        }
    }
}