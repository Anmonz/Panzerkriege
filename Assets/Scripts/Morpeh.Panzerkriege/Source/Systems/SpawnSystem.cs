using Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using System.Collections.Generic;

/// <summary>
/// Система возрождения игроков через заданное время
/// </summary>
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(SpawnSystem))]
public sealed class SpawnSystem : UpdateSystem
{

    [SerializeField] private List<GameObject> _players; //Список префабов игроков
    [SerializeField] private float _timeRespawnPlayers; //Время возрождения игроков

    private Filter _filterSpawns;//Фильтр точек возрождения
    private Filter _filterPlayers;//Фильтр игроков

    private float[] _startTimeRespawn;//Массив времени начала возрождения игроков

    /// <summary>
    /// Устанавливает фильтр точек возрождения и игроков
    /// Задает массив времени как отрицательный
    /// </summary>
    public override void OnAwake()
    {
        _filterSpawns = World.Filter.With<SpawnComponent>().With<TransformComponent>();
        _filterPlayers = World.Filter.With<PlayerComponent>();
        _startTimeRespawn = new float[2] { -1f, -1f }; //***ТОЛЬКО ДЛЯ 2Х ИГРОКОВ*** Время отрицательно для определения живого игрока
    }

    /// <summary>
    /// Проверяет игроков на смерть и возрождает их
    /// </summary>
    /// <param name="deltaTime"></param>
    public override void OnUpdate(float deltaTime)
    {
        //Проверка на уменьшение игроков на поле 
        if (_filterPlayers.Length < _players.Count)
        {
            bool[] playersDead = { true, true }; //задает позиции мертвых игроков

            //Проверяет на отсуствие 1 игрока ***ПОДХОДИТ ТОЛЬКО ДЛЯ 2Х ИГРОКОВ***
            if (_filterPlayers.Length == 1)
            {
                var players = _filterPlayers.Select<PlayerComponent>();
                playersDead[players.GetComponent(0).NumberPlayer - 1] = false; //Устанавливает живого игрока
            }

            //***ТОЛЬКО ДЛЯ 2Х ИГРОКОВ***
            for (int j = 0; j < 2; j++)
            {
                //Проверка на смерть
                if (playersDead[j])
                {
                    //Провека на время начала смерти (При отрицательном времени игрок был жив)
                    if (_startTimeRespawn[j] == -1f)
                    {
                        _startTimeRespawn[j] = Time.time;
                        continue;
                    }
                    else if (_startTimeRespawn[j] + _timeRespawnPlayers > Time.time) continue; //Проверка на окончание времени возрождения

                    var spawnPoints = _filterSpawns.Select<SpawnComponent>();
                    var transforms = _filterSpawns.Select<TransformComponent>();

                    for (int i = 0, length = this._filterSpawns.Length; i < length; i++)
                    {
                        ref var spawnPoint = ref spawnPoints.GetComponent(i);

                        //Проверка на точку возрождения принадлежащую игроку
                        if ((spawnPoint.NumberPlayer - 1) == j)
                        {
                            ref var transform = ref transforms.GetComponent(i);

                            //Проверка на столкновение с коллайдерами в точке возрождения
                            RaycastHit2D hit;
                            hit = Physics2D.BoxCast(transform.transform.position, new Vector2(1.8f, 1.8f), 0, Vector2.zero); //Стандартный размер танка 2х2

                            //Провека столкновения на пустой результат
                            if (hit.collider == null)
                            {
                                //Создание игрока и добавление к нему метки безсмертия
                                ImmortalMarkProvider immortal = Instantiate(_players[j], transform.transform.position, transform.transform.rotation).AddComponent<ImmortalMarkProvider>();
                                //Установка времения начала безсмертия
                                immortal.GetData().TimeStartImmortal = Time.time;
                                //Обнуления времени начала возрождения
                                _startTimeRespawn[j] = -1f;
                                break;
                            }
                        }
                    }
                }
            }

        }
    }
}