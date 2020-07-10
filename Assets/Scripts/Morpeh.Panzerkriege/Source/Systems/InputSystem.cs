using Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Morpeh.Globals;

/// <summary>
/// Система проверки нажатия кнопок заданных в ControlsComponent для управления игровыми танками
/// </summary>
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(InputSystem))]
public sealed class InputSystem : UpdateSystem {
    [SerializeField] private GlobalEvent gunFireKeyEvent; //Событие нажатия кнопки стрельбы
    [SerializeField] private GlobalEventInt keyCodesMoveEvent; //Событие нажатия кнопок передвижения
    [SerializeField] private GlobalEvent startGameEvent; //Событие начала игры
    [SerializeField] private GlobalEvent endGameEvent;//Событие окончания игры

    private KeyCode _currentKeyCodeDown; //Код текущей кнопки нажатой кнопки передвижения
    private ControlsComponent _control; //Компонент управления игрока
    private bool _isStartedGame = false; //Метка начала игры
    
    /// <summary>
    /// Задает компонент управления
    /// Устанавливает текущую нажатую кнопку как пустую
    /// </summary>
    public override void OnAwake()
    {
        ref var controls = ref World.Filter.With<ControlsComponent>().Select<ControlsComponent>();
        _control = controls.GetComponent(0);
        _currentKeyCodeDown = KeyCode.None;
    }

    /// <summary>
    /// Проверяет начало и окончание игры
    /// Запускает проверку нажатия кнопок на стрельбу и передвижение игроков
    /// </summary>
    /// <param name="deltaTime"></param>
    public override void OnUpdate(float deltaTime)
    {
        if (startGameEvent.IsPublished)
        {
            _isStartedGame = true;
        }

        if (endGameEvent.IsPublished)
        {
            _isStartedGame = false;
        }

        if (_isStartedGame)
        {
            CheckMoveKeysCodes();
            CheckFireKeyCode(_control.keyMoveFire);
        }
    }

    /// <summary>
    /// Проверяет нажатие кнопки передвижения при условии что кнопка все еще нажата или нажата новая кнопка
    /// </summary>
    /// <param name="keyCode">Код кнопки</param>
    private void CheckKeyCodeDown(KeyCode keyCode)
    {
        //Провека нажатия кнопки
        if (Input.GetKey(keyCode))
        {
            //Проверяет кнопку на предыдущие нажатие или новое нажатие
            if (_currentKeyCodeDown == KeyCode.None || _currentKeyCodeDown == keyCode)
            {
                //устанавливает текущей нажатую кнопку
                _currentKeyCodeDown = keyCode;
                //Запускает событие нажатия кнопки с кодом кнопки
                keyCodesMoveEvent.Publish((int)keyCode);
            }
        }
        else
        {
            //Проверка на отжатие текущей кнопки
            if (_currentKeyCodeDown == keyCode)
            {
                //Устанавливает текущую кнопку пустой
                _currentKeyCodeDown = KeyCode.None;
                //Запускает событие нажатия кнопки с пустым кодом
                keyCodesMoveEvent.Publish((int)KeyCode.None);
            }
        }
    }

    /// <summary>
    /// Метод проверяет нажатие кнопок передвижения
    /// </summary>
    private void CheckMoveKeysCodes()
    {
        CheckKeyCodeDown(_control.keyMoveDown);
        CheckKeyCodeDown(_control.keyMoveLeft);
        CheckKeyCodeDown(_control.keyMoveRight);
        CheckKeyCodeDown(_control.keyMoveUp);
    }

    /// <summary>
    /// Метод проверяет нажатие кнопки стрельбы
    /// </summary>
    /// <param name="keyCode"></param>
    private void CheckFireKeyCode(KeyCode keyCode)
    {
        if (Input.GetKeyDown(keyCode))
        {
            gunFireKeyEvent.Publish();
        }
    }
}