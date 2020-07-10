using Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Morpeh.Globals;

/// <summary>
/// Определяет управление игроком
/// </summary>
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(PlayerSystem))]
public sealed class PlayerSystem : UpdateSystem {

    [SerializeField] private GlobalEventInt keyCodesMoveEvent;//Событие нажатия кнопки управления
    [SerializeField] private GlobalEvent keyGunFireEvent;//Событие нажатия кнопки огня
    [SerializeField] private GlobalEvent startGameEvent;//Событие старта игры
    [SerializeField] private GlobalEvent endGameEvent;//Событие окончания игры

    private Filter _filterPlayers; //Фильтер сущностей игроков
    private ControlsComponent _control; //Компонент управления
    private bool _isStartedGame = false; //Метка начала игры

    /// <summary>
    /// Устанавливает фильтер игроков
    /// Задает компонент управления
    /// </summary>
    public override void OnAwake() {
        _filterPlayers = World.Filter.With<PlayerComponent>().With<GunComponent>().With<MovementComponent>().With<TransformComponent>();

        ref var controls = ref World.Filter.With<ControlsComponent>().Select<ControlsComponent>();
        _control = controls.GetComponent(0);
    }

    /// <summary>
    /// Проверяет начало и окончание игры
    /// Запускает стрельбу и передвижение игроков
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
                OnFireInput();
                OnMoveInput();
        }
    }

    /// <summary>
    /// Запускает стрельбу игрока
    /// </summary>
    private void OnFireInput()
    {
        if(keyGunFireEvent.IsPublished) //Проверка события
        {
            var players = this._filterPlayers.Select<PlayerComponent>();
            var guns = this._filterPlayers.Select<GunComponent>();

            //Перебор сущностей
            for (int i = 0, length = this._filterPlayers.Length; i < length; i++)
            {
                ref var player = ref players.GetComponent(i);

                //ПРоверка сетевого игрока
                if (player.NumberPlayer == WebManager.Instance.NumberPlayer)
                {
                    ref var gun = ref guns.GetComponent(i);
                    //Провекра на перезарядку орудия
                    if (!gun.IsReloadGun) 
                        gun.IsShoot = true;//Установка метки стрельбы на оружие
                }
            }
        }
    }

    /// <summary>
    /// Запускает передвижение игрока
    /// </summary>
    private void OnMoveInput()
    {
        //ПРоверка события нажатия кнопки передвижения
        if (keyCodesMoveEvent.IsPublished)
        {
            var movers = this._filterPlayers.Select<MovementComponent>();
            var transforms = this._filterPlayers.Select<TransformComponent>();
            var players = this._filterPlayers.Select<PlayerComponent>();

            //Перебор сущностей
            for (int i = 0, length = this._filterPlayers.Length; i < length; i++)
            {
                ref var player = ref players.GetComponent(i);

                //ПРоверка сетевого игрока
                if (player.NumberPlayer == WebManager.Instance.NumberPlayer)
                {
                    ref var mover = ref movers.GetComponent(i);

                    //Установка вектора передвижения игрока
                    mover.VectorMove = CreateMoveVector((KeyCode)keyCodesMoveEvent.BatchedChanges.Peek());

                    //Установка направления объекта игрока в сторону передвижения
                    if (mover.VectorMove != Vector3.zero)
                        transforms.GetComponent(i).transform.up = mover.VectorMove;
                }
            }
        }
    }

    /// <summary>
    /// Вычисляет вектор направления игрока в зависимости от нажатой кнопки
    /// </summary>
    /// <param name="keyCode">Код нажатой клавиши</param>
    /// <returns></returns>
    private Vector3 CreateMoveVector(KeyCode keyCode)
    {
        Vector3 vectorMove = Vector3.zero;

        if (keyCode == _control.keyMoveDown)
        {
            vectorMove = Vector3.down;
        }
        else if (keyCode == _control.keyMoveLeft)
        {
            vectorMove = Vector3.left;
        }
        else if (keyCode == _control.keyMoveRight)
        {
            vectorMove = Vector3.right;
        }
        else if (keyCode == _control.keyMoveUp)
        {
            vectorMove = Vector3.up;
        }

        return vectorMove;
    }
}