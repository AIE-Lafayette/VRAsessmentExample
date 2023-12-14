using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject _player;

    private static GameManagerBehaviour _instance;

    public static GameManagerBehaviour Instance
    {
        get
        {
            return _instance;
        }
        set
        {
            if (_instance)
                return;

            _instance = value;
        }
    }

    public GameObject Player
    {
        get
        {
            return _player;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

}
