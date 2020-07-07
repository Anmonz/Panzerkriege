using Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Morpeh.Globals;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(EndGameSystem))]
public sealed class EndGameSystem : UpdateSystem {

    [SerializeField] private GlobalEvent endGameEvent;
    [SerializeField] private WebManager webManager;
    [SerializeField] private float endTimeRestart;

    private bool isEnd = false;
    private float startTheEnd;

    public override void OnAwake() {
    }

    public override void OnUpdate(float deltaTime) {
        
        if(endGameEvent.IsPublished)
        {
            isEnd = true;
            startTheEnd = Time.time;
        }

        if(isEnd)
        { 
            if(Time.time >= startTheEnd + endTimeRestart)
            {
                webManager.LeaveRoom();
            }
        }
    }
}