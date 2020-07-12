using Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[System.Serializable]
public struct ControlsComponent : IComponent {
    public KeyCode keyMoveUp;   //Код кнопки движения вверх
    public KeyCode keyMoveDown; //Код кнопки движения вниз
    public KeyCode keyMoveLeft; //Код кнопки движения влево
    public KeyCode keyMoveRight;//Код кнопки движения вправо
    public KeyCode keyMoveFire; //Код кнопки стрельбы
}