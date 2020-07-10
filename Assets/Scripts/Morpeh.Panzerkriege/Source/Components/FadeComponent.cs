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
    [SerializeField] private Image _fadeImage;
    [SerializeField] private float _timeFade;
    [SerializeField] private bool _isDarken;
    [SerializeField] private bool _isStartFade;
    [SerializeField] private UnityEvent _endFadeEvent;

    private float _timeStartFade;

    public UnityEvent EndFadeEvent
    {
        get => _endFadeEvent;
    }

    public Image FadeImage
    {
        get => _fadeImage;
    }

    public float TimeFade
    {
        get => _timeFade;
    }

    public bool IsDarken
    {
        get => _isDarken;
        set => _isDarken = value;
    }

    public bool IsStartFade
    {
        get => _isStartFade;
        set => _isStartFade = value;
    }

    public float TimeStartFade
    {
        get => _timeStartFade;
        set => _timeStartFade = value;
    }
}