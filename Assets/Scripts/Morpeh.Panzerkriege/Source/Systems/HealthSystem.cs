using Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Morpeh.Globals;

/// <summary>
/// Система проверки окончания жизней в HealthComponent
/// </summary>
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(HealthSystem))]
public sealed class HealthSystem : UpdateSystem {

    [SerializeField] private GlobalEvent destroyEvent;//Событие уничтожения объекта

    private Filter _filterHealth;//Фильтр HealthComponent DestroyComponent
    private Filter _filterImmortalHealth; // фильтр безсмертных

    /// <summary>
    /// Устанавливает фильтр
    /// </summary>
    public override void OnAwake()
    {
        _filterHealth = World.Filter.With<HealthComponent>().With<DestroyComponent>().Without<ImmortalMarkComponent>();
        _filterImmortalHealth = World.Filter.With<HealthComponent>().With<ImmortalMarkComponent>();

    }

    /// <summary>
    /// Вызывает методы проверки здоровья
    /// </summary>
    /// <param name="deltaTime"></param>
    public override void OnUpdate(float deltaTime)
    {
        CheckHealth();
        CheckImmortalHealth();
    }

    /// <summary>
    /// Проверяет объекты с HealthComponent, 
    /// Отнимает полученный урон
    /// при окончании жихней (healthPoints) ставит метку уничтожения на DestroyComponent 
    /// и вызывает событие уничтожения
    /// </summary>
    public void CheckHealth()
    {
        var components = this._filterHealth.Select<HealthComponent>();
        var destroys = this._filterHealth.Select<DestroyComponent>();

        for (int i = 0, length = this._filterHealth.Length; i < length; i++)
        {
            ref var health = ref components.GetComponent(i);

            while (health.Damages.Count > 0)
            {
                health.healthPoints -= health.Damages.Pop();
            }

            //ПРоверка обнуления жизней
            if (health.healthPoints <= 0)
            {
                //Установка уничтожения оюъекта
                destroys.GetComponent(i).IsDestroy = true;
                destroyEvent.Publish();
            }
        }
    }

    /// <summary>
    /// Очищает полученный урон для безсмертных
    /// </summary>
    private void CheckImmortalHealth()
    {
        var components = this._filterImmortalHealth.Select<HealthComponent>();

        for (int i = 0, length = this._filterImmortalHealth.Length; i < length; i++)
        {
            components.GetComponent(i).Damages.Clear();
        }
    }
}