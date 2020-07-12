using Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using UnityEngine.UI;
using UnityEngine.Events;

/// <summary>
/// Анимированный таймер
/// Система: AnimTimerSystem
/// </summary>
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[System.Serializable]
public struct AnimTimerComponent : IComponent {
    [SerializeField] private Text _timerText;   //Объекта вывода текста в Unity.UI
    [SerializeField] private string _endText;   //СТрока выводящаяся на последней секунде таймера
    [SerializeField] private float _timerTime;  //Время работы таймера
    [SerializeField] private bool _isIncrease;  //Метка на изменения отсчета таймера на увеличение
    [SerializeField] private bool _isStartTimer;//Метка начала работы таймера
    [SerializeField] private int _fontMaxSize;  //Максимальный размер шрифта таймера
    [SerializeField] private int _fontMinSize;  //Минимальный размер шрифта таймера

    [SerializeField] private UnityEvent _endTimerEvent;//Событие окончания таймера

    private float _timeStartTimer;//Время начала работы таймера

    /// <summary>
    /// Событие окончания работы таймера
    /// </summary>
    public UnityEvent EndTimerEvent
    {
        get => _endTimerEvent;
    }

    /// <summary>
    /// Объекта вывода текста в Unity.UI
    /// </summary>
    public Text TimerText
    {
        get => _timerText;
    }

    /// <summary>
    /// СТрока выводящаяся на последней секунде таймера
    /// </summary>
    public string EndText
    {
        get => _endText;
    }

    /// <summary>
    /// Время работы таймера
    /// </summary>
    public float TimerTime
    {
        get => _timerTime;
    }

    /// <summary>
    /// Метка на изменения отсчета таймера на увеличение
    /// </summary>
    public bool IsIncrease
    {
        get => _isIncrease;
        set => _isIncrease = value;
    }

    /// <summary>
    /// Метка начала работы таймера
    /// </summary>
    public bool IsStartTimer
    {
        get => _isStartTimer;
        set => _isStartTimer = value;
    }

    /// <summary>
    /// Максимальный размер шрифта таймера
    /// </summary>
    public int FontMaxSize
    {
        get => _fontMaxSize;
    }

    /// <summary>
    /// Минимальный размер шрифта таймера
    /// </summary>
    public int FontMinSize
    {
        get => _fontMinSize;
    }

    /// <summary>
    /// Время начала работы таймера
    /// </summary>
    public float TimeStartTimer
    {
        get => _timeStartTimer;
        set => _timeStartTimer = value;
    }
}