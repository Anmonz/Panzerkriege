using Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Morpeh.Globals;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(InputSystem))]
public sealed class InputSystem : UpdateSystem {
    [SerializeField] private GlobalEvent gunFireEvent;
    [SerializeField] private GlobalEventInt keyCodeEvent;

    private KeyCode _keyCodeDown;
    private ControlsComponent _control;
    
    public override void OnAwake()
    {
        ref var controls = ref World.Filter.With<ControlsComponent>().Select<ControlsComponent>();
        _control = controls.GetComponent(0);
        _keyCodeDown = KeyCode.None;
    }

    public override void OnUpdate(float deltaTime)
    {
        //if (_control != )
        {
            CheckMoveKeysCodes();
            CheckFireKeyCode(_control.keyMoveFire);
        }
    }

    private void CheckKeyCodeDown(KeyCode keyCode)
    {
        if (Input.GetKey(keyCode))
        {
            if (_keyCodeDown == KeyCode.None || _keyCodeDown == keyCode)
            {
                _keyCodeDown = keyCode;
                keyCodeEvent.Publish((int)keyCode);
            }
        }
        else
        {
            if (_keyCodeDown == keyCode)
            {
                _keyCodeDown = KeyCode.None;
                keyCodeEvent.Publish((int)KeyCode.None);
            }
        }
    }

    private void CheckMoveKeysCodes()
    {
        CheckKeyCodeDown(_control.keyMoveDown);
        CheckKeyCodeDown(_control.keyMoveLeft);
        CheckKeyCodeDown(_control.keyMoveRight);
        CheckKeyCodeDown(_control.keyMoveUp);
    }

    private void CheckFireKeyCode(KeyCode keyCode)
    {
        if (Input.GetKeyDown(keyCode))
        {
            gunFireEvent.Publish();
        }
    }
}