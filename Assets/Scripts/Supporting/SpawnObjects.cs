using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Создает объекты через инспектор в нужной точке
/// </summary>
public class SpawnObjects : MonoBehaviour
{
    [SerializeField] private GameObject spawnObject; //Создающийся объект

    /// <summary>
    /// Создает объект в позиции трансформа
    /// </summary>
    /// <param name="transform"></param>
    public void SpawnSObject(Transform transform)
    {
        Instantiate(spawnObject, transform.position, transform.rotation);
    }
}
