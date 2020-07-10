using Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Unity.Mathematics;

/// <summary>
/// Система выцветания UI Image через альфу цвета
/// </summary>
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(FadeTimerSystem))]
public sealed class FadeTimerSystem : UpdateSystem {

    private Filter _filter;//Фильтр FadeComponent

    /// <summary>
    /// Установка фильтра 
    /// </summary>
    public override void OnAwake() {
        _filter = World.Filter.With<FadeComponent>();
    }

    /// <summary>
    /// Изменяет альфу цвета UI.Image по заданному времени 
    /// </summary>
    /// <param name="deltaTime"></param>
    public override void OnUpdate(float deltaTime) {
        var components = this._filter.Select<FadeComponent>();

        for (int i = 0, length = this._filter.Length; i < length; i++)
        {
            ref var component = ref components.GetComponent(i);

            //Проверка метки начала выцветания
            if (component.IsStartFade)
            {
                //Проверка на пустое время начала выцветания
                if (component.TimeStartFade == 0) 
                    component.TimeStartFade = Time.time;//Установка стартового времени выцветания 

                //Вычисление состояние выцветания относительно прошедшего времени
                float fadeState = math.lerp(0,1,(Time.time - component.TimeStartFade) / component.TimeFade);
                //Проверка на метку затемнения
                if (!component.IsDarken) fadeState = 1 - fadeState;
                //Установка нового цвета скопированного с текущего с измененным альфа каналом
                component.FadeImage.color = new Color(component.FadeImage.color.r, component.FadeImage.color.g, component.FadeImage.color.b, fadeState);

                //Проверка на окончание выцветания или затемнения
                if ((!component.IsDarken && fadeState == 0) || (component.IsDarken && fadeState == 1))
                {
                    component.EndFadeEvent.Invoke(); //Вызов соботия окончания выцветания
                    component.IsStartFade = false; //Убирание метки начала выцветания 
                    component.TimeStartFade = 0; //Обнуление времени начала выцветания
                } 
            }
        }
    }

}