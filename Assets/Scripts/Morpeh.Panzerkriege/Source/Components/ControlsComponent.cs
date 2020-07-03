using Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[System.Serializable]
public struct ControlsComponent : IComponent {
    public KeyCode keyMoveUp;
    public KeyCode keyMoveDown;
    public KeyCode keyMoveLeft;
    public KeyCode keyMoveRight;
    public KeyCode keyMoveFire;
}