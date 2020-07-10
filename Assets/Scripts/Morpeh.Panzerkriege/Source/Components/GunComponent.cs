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
    public Transform gunPosition;
    public float timeReload;
    public GameObject bulletPrefab;
    public UnityEvent gunShootEvent;
    
    
    private bool _isReloadGun;
    private float _startTimeReload;
    private bool _isShoot;

    public bool IsReloadGun
    {
        get => _isReloadGun;
        set => _isReloadGun = value;
    }

    public float StartTimeReload
    {
        get => _startTimeReload;
        set => _startTimeReload = value;
    }

    public bool IsShoot
    {
        get => _isShoot;
        set => _isShoot = value;
    }

}