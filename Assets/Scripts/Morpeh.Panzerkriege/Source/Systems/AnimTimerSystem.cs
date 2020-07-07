using Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using System;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(AnimTimerSystem))]
public sealed class AnimTimerSystem : UpdateSystem {
    private Filter _filter;

    public override void OnAwake()
    {
        _filter = World.Filter.With<AnimTimerComponent>();
    }

    public override void OnUpdate(float deltaTime)
    {
        var components = this._filter.Select<AnimTimerComponent>();

        for (int i = 0, length = this._filter.Length; i < length; i++)
        {
            ref var component = ref components.GetComponent(i);

            if (component.IsStartTimer)
            {
                if (component.TimeStartTimer == 0) component.TimeStartTimer = Time.time;

                float timerState = Time.time - component.TimeStartTimer;
                if (!component.IsIncrease) timerState = component.TimerTime - timerState;

                int time = (int)Math.Ceiling(timerState);
                if ((!component.IsIncrease && time == 1) || (component.IsIncrease && time == component.TimerTime)) component.TimerText.text = component.EndText;
                else component.TimerText.text = Convert.ToString(time);

                component.TimerText.fontSize = component.FontMinSize + (int)Math.Round((component.FontMaxSize - component.FontMinSize) * (timerState - (int)Math.Truncate(timerState)));
                component.TimerText.color = new Color(component.TimerText.color.r, component.TimerText.color.g, component.TimerText.color.b, (timerState - (int)Math.Truncate(timerState)));

                if ((!component.IsIncrease && timerState <= 0) || (component.IsIncrease && timerState >= component.TimerTime))
                {
                    component.IsStartTimer = false;
                    component.TimeStartTimer = 0;
                    component.TimerText.color = Color.clear;
                }
            }
        }
    }
}