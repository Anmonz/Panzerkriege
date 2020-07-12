using Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using UnityEngine.UI;
using UnityEngine.Events;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[System.Serializable]
public struct FadeComponent : IComponent {
    [SerializeField] private Image _fadeImage;  //Объекта Image Unity.UI к которому применяется выцветания
    [SerializeField] private float _timeFade;   //Время работы выцветания
    [SerializeField] private bool _isDarken;    //Метка изменения работы на затемнение
    [SerializeField] private bool _isStartFade; //Метка начала работы выцветания
    [SerializeField] private UnityEvent _endFadeEvent;//Событие окончания работы выцветания

    private float _timeStartFade;//Время начала работы выцветания

    /// <summary>
    /// Событие окончания работы выцветания
    /// </summary>
    public UnityEvent EndFadeEvent
    {
        get => _endFadeEvent;
    }

    /// <summary>
    /// Объекта Image Unity.UI к которому применяется выцветания
    /// </summary>
    public Image FadeImage
    {
        get => _fadeImage;
    }

    /// <summary>
    /// ВВремя работы выцветания
    /// </summary>
    public float TimeFade
    {
        get => _timeFade;
    }

    /// <summary>
    /// Метка изменения работы на затемнение
    /// </summary>
    public bool IsDarken
    {
        get => _isDarken;
        set => _isDarken = value;
    }

    /// <summary>
    /// Метка начала работы выцветания
    /// </summary>
    public bool IsStartFade
    {
        get => _isStartFade;
        set => _isStartFade = value;
    }

    /// <summary>
    /// Время начала работы выцветания
    /// </summary>
    public float TimeStartFade
    {
        get => _timeStartFade;
        set => _timeStartFade = value;
    }
}