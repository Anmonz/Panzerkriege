using Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using UnityEngine.UI;
using UnityEngine.Events;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[System.Serializable]
public struct AnimTimerComponent : IComponent {
    [SerializeField] private Text _timerText;
    [SerializeField] private string _endText;
    [SerializeField] private float _timerTime;
    [SerializeField] private bool _isIncrease;
    [SerializeField] private bool _isStartTimer;
    [SerializeField] private int _fontMaxSize;
    [SerializeField] private int _fontMinSize;

    [SerializeField] private UnityEvent _endTimerEvent;

    private float _timeStartTimer;

    public UnityEvent EndTimerEvent
    {
        get => _endTimerEvent;
    }

    public Text TimerText
    {
        get => _timerText;
    }

    public string EndText
    {
        get => _endText;
    }

    public float TimerTime
    {
        get => _timerTime;
    }

    public bool IsIncrease
    {
        get => _isIncrease;
        set => _isIncrease = value;
    }

    public bool IsStartTimer
    {
        get => _isStartTimer;
        set => _isStartTimer = value;
    }

    public int FontMaxSize
    {
        get => _fontMaxSize;
    }

    public int FontMinSize
    {
        get => _fontMinSize;
    }

    public float TimeStartTimer
    {
        get => _timeStartTimer;
        set => _timeStartTimer = value;
    }
}