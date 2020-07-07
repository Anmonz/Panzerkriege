using Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Unity.Mathematics;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(FadeTimerSystem))]
public sealed class FadeTimerSystem : UpdateSystem {
    private Filter _filter;

    public override void OnAwake() {
        _filter = World.Filter.With<FadeComponent>();
    }

    public override void OnUpdate(float deltaTime) {
        var components = this._filter.Select<FadeComponent>();

        for (int i = 0, length = this._filter.Length; i < length; i++)
        {
            ref var component = ref components.GetComponent(i);
            
            if (component.IsStartFade)
            {
                if (component.TimeStartFade == 0) component.TimeStartFade = Time.time;
                
                float fadeState = math.lerp(0,1,(Time.time - component.TimeStartFade) / component.TimeFade);
                if (!component.IsDarken) fadeState = 1 - fadeState;
                
                component.FadeImage.color = new Color(component.FadeImage.color.r, component.FadeImage.color.g, component.FadeImage.color.b, fadeState);
                
                if((!component.IsDarken && fadeState == 0) || (component.IsDarken && fadeState == 1))
                {
                    component.IsStartFade = false;
                    component.TimeStartFade = 0;
                    component.IsDarken = !component.IsDarken;
                } 
            }
        }
    }

}