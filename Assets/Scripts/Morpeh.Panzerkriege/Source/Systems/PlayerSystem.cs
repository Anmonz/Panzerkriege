using Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Morpeh.Globals;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(PlayerSystem))]
public sealed class PlayerSystem : UpdateSystem {
    [SerializeField] private GlobalEventInt keyCodeEvent;
    [SerializeField] private GlobalEvent gunFireEvent;
    [SerializeField] private GlobalEvent startEvent;

    private Filter _filter;
    private ControlsComponent _control;
    private bool isStarted = false;

    public override void OnAwake() {
        _filter = World.Filter.With<PlayerComponent>().With<GunComponent>().With<MovementComponent>().With<TransformComponent>();
        ref var controls = ref World.Filter.With<ControlsComponent>().Select<ControlsComponent>();
        _control = controls.GetComponent(0);
    }

    public override void OnUpdate(float deltaTime) 
    {
        if (startEvent.IsPublished)
        {
            isStarted = true;
        }
        if(isStarted)
        { 
                OnFireInput();
                OnMoveInput();
        }
    }

    private void OnFireInput()
    {
        if(gunFireEvent.IsPublished)
        {
            var players = this._filter.Select<GunComponent>();

            for (int i = 0, length = this._filter.Length; i < length; i++)
            {
                ref var player = ref players.GetComponent(i);
                if(!player.IsReloadGun) player.IsShoot = true;
            }

            gunFireEvent.Dispose();
        }
    }

    private void OnMoveInput()
    {
        if (keyCodeEvent.IsPublished)
        {
            var players = this._filter.Select<MovementComponent>();
            var transforms = this._filter.Select<TransformComponent>();

            for (int i = 0, length = this._filter.Length; i < length; i++)
            {
                ref var player = ref players.GetComponent(i);
                player.VectorMove = CreateMoveVector((KeyCode)keyCodeEvent.BatchedChanges.Peek());
                
                if(player.VectorMove != Vector3.zero)
                    transforms.GetComponent(i).transform.up = player.VectorMove;
            }

            keyCodeEvent.Dispose();
        }
    }

    private Vector3 CreateMoveVector(KeyCode keyCode)
    {
        Vector3 vectorMove = Vector3.zero;

        if (keyCode == _control.keyMoveDown)
        {
            vectorMove = Vector3.down;
        }
        else if (keyCode == _control.keyMoveLeft)
        {
            vectorMove = Vector3.left;
        }
        else if (keyCode == _control.keyMoveRight)
        {
            vectorMove = Vector3.right;
        }
        else if (keyCode == _control.keyMoveUp)
        {
            vectorMove = Vector3.up;
        }

        return vectorMove;
    }
}