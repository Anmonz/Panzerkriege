using Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using UnityEngine.Events;
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[System.Serializable]
public struct GunComponent : IComponent
{
    public Transform gunPosition;   //Трансформ с точкой и направление в котором появится пуля после выстрела
    public float timeReload;        //Время перезарядки (Ожидания перед срабатыванием нового выстрела)
    public GameObject bulletPrefab; //Объект пули создающейся в gunPosition
    public UnityEvent gunShootEvent;//Событие выстрела 
    
    
    private bool _isReloadGun;      //Метка перезарядки
    private float _startTimeReload; //Время начала перезарядки
    private bool _isShoot;          //Метка указывающая на совершение выстрела

    /// <summary>
    /// Метка перезарядки
    /// </summary>
    public bool IsReloadGun
    {
        get => _isReloadGun;
        set => _isReloadGun = value;
    }

    /// <summary>
    /// Время начала перезарядки
    /// </summary>
    public float StartTimeReload
    {
        get => _startTimeReload;
        set => _startTimeReload = value;
    }

    /// <summary>
    /// Метка указывающая на совершение выстрела
    /// </summary>
    public bool IsShoot
    {
        get => _isShoot;
        set => _isShoot = value;
    }

}