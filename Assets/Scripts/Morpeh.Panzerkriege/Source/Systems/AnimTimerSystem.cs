using Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using System;

/// <summary>
/// Система анимированного таймера
/// </summary>
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(AnimTimerSystem))]
public sealed class AnimTimerSystem : UpdateSystem {
    private Filter _filter; //Фильтр таймера

    /// <summary>
    /// Установка фильтра
    /// </summary>
    public override void OnAwake()
    {
        _filter = World.Filter.With<AnimTimerComponent>();
    }

    /// <summary>
    /// Обновляет таймер 
    /// </summary>
    /// <param name="deltaTime"></param>
    public override void OnUpdate(float deltaTime)
    {
        var components = this._filter.Select<AnimTimerComponent>();

        for (int i = 0, length = this._filter.Length; i < length; i++)
        {
            ref var component = ref components.GetComponent(i);

            //Проверка на активацию таймера
            if (component.IsStartTimer)
            {
                //Проверка на пустое время начала работы таймера
                if (component.TimeStartTimer == 0) 
                    component.TimeStartTimer = Time.time;//Установка времени начала работы таймера

                //Вычисление прошедшего времени таймера
                float timerState = Time.time - component.TimeStartTimer;
                //Проверка на увеличивающийя тип таймера
                if (!component.IsIncrease) 
                    timerState = component.TimerTime - timerState; //Оставшееся время

                //Вычисление целого числа от времени
                int time = (int)Math.Ceiling(timerState);
                //Проверка на последнюю секунду таймера
                if ((!component.IsIncrease && time == 1) || (component.IsIncrease && time == component.TimerTime)) 
                    component.TimerText.text = component.EndText; //Установка окончательного текста таймера
                else 
                    component.TimerText.text = Convert.ToString(time);//Установка текущего времени таймера

                //Установка размера текста таймера относительно долей текущей секунды
                component.TimerText.fontSize = component.FontMinSize + (int)Math.Round((component.FontMaxSize - component.FontMinSize) * (timerState - (int)Math.Truncate(timerState)));
                //Установка прозрачности текста таймера относительно долей текущей секунды
                component.TimerText.color = new Color(component.TimerText.color.r, component.TimerText.color.g, component.TimerText.color.b, (timerState - (int)Math.Truncate(timerState)));

                //Проверка на окончание таймера
                if ((!component.IsIncrease && timerState <= 0) || (component.IsIncrease && timerState >= component.TimerTime))
                {
                    component.EndTimerEvent.Invoke();//Вызов события окончания таймера
                    component.IsStartTimer = false;//Убирание метки работы таймера
                    component.TimeStartTimer = 0;//Обнуление времени начала таймера
                    component.TimerText.color = Color.clear;//Установка цвета текста на пустой
                }
            }
        }
    }
}