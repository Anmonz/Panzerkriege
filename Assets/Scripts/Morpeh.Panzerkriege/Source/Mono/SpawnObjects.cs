using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Создает объекты через инспектор в нужной точке
/// </summary>
public class SpawnObjects : MonoBehaviour
{
    [SerializeField] private GameObject smallExplosionFX; //Префаб маленького взрыва
    [SerializeField] private GameObject bigExplosionFX; //Префаб большого взрыва

    /// <summary>
    /// Создает объект маленького взырыва в позиции трансформа
    /// </summary>
    /// <param name="transform"></param>
    public void SpawnSmallExplosion(Transform transform)
    {
        Instantiate(smallExplosionFX, transform.position, transform.rotation);
    }

    /// <summary>
    /// Создает объект большого взырыва в позиции трансформа
    /// </summary>
    /// <param name="transform"></param>
    public void SpawnBigExplosion(Transform transform)
    {
        Instantiate(bigExplosionFX, transform.position, transform.rotation);
    }
}
