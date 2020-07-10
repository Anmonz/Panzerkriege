using Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Morpeh.Globals;

/// <summary>
/// Система уничтожения объектов
/// </summary>
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(DestroySystem))]
public sealed class DestroySystem : UpdateSystem {

    [SerializeField] private GlobalEvent destroyEvent;//Событие уничтожения объекта

    private Filter _filter;//Фильтр

    /// <summary>
    /// Установка фильтра
    /// </summary>
    public override void OnAwake()
    {
        _filter = World.Filter.With<DestroyComponent>();
    }

    /// <summary>
    /// Уничтожение объекта
    /// при активации метки уничтожения 
    /// и вызове события уничтожения
    /// </summary>
    /// <param name="deltaTime"></param>
    public override void OnUpdate(float deltaTime)
    {
        //Проверка на вызов события уничтожения
        if (destroyEvent.IsPublished)
        {
            var destroys = this._filter.Select<DestroyComponent>();

            for (int i = 0, length = this._filter.Length; i < length; i++)
            {
                ref var destroyObj = ref destroys.GetComponent(i);

                //Проверка на метку уичтожения объекта
                if (destroyObj.IsDestroy)
                {
                    //Вызов события уничтожения на компоненте
                    destroyObj.destroyEvent.Invoke();
                    //Уничтожение игрового объекта
                    Destroy(destroyObj.destroyObject);
                }
            }

            destroyEvent.Dispose();
        }
    }
}