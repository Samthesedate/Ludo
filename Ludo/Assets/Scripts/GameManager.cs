using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject LevelCreator;
    [SerializeField]
    private GameObject PlayerCreator;

    void Awake()
    {
        Instantiate(LevelCreator, new Vector2(0, 0), Quaternion.identity);
    }

    void Start()
    {
        Instantiate(PlayerCreator, new Vector3(0, 0, 1), Quaternion.identity);
    }

}
