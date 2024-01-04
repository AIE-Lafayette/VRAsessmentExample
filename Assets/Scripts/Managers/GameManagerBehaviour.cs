using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.XR.Management;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject _player;
    [SerializeField, Tooltip("Game objects that should only be enabled when the game has started.")]
    private GameObject[] _gameActiveObjects;
    [SerializeField]
    private bool _gameStarted;
    private List<EnemyBehaviour> _activeEnemies = new List<EnemyBehaviour>();
    private int _score;
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

    public int Score { get => _score; private set => _score = value; }

    private void Awake()
    {
        _instance = this;
    }

    public void IncreaseScore(int amount)
    {
        Score += amount;
    }

    public void AddEnemy(EnemyBehaviour enemy)
    {
        _activeEnemies.Add(enemy);
    }

    public void RemoveEnemy(EnemyBehaviour enemy)
    {
        _activeEnemies.Remove(enemy);
    }

    public void StartGame()
    {
        _gameStarted = true;

        foreach (GameObject item in _gameActiveObjects)
        {
            item.SetActive(true);
        }
    }

    public void StopGame()
    {
        _gameStarted = false;

        foreach (GameObject item in _gameActiveObjects)
        {
          item.SetActive(false);
        }

        foreach (EnemyBehaviour enemy in _activeEnemies)
        {
            Destroy(enemy);
        }
    }

    public void StopGame(float delay)
    {
        StartCoroutine(DelayedStop(delay));
    }

    private IEnumerator DelayedStop(float delay)
    {
        yield return new WaitForSeconds(delay);
        StopGame();
    }

    public void ResetGame(float delay)
    {
        StartCoroutine(DelayedReset(delay));
    }

    private IEnumerator DelayedReset(float delay)
    {
        yield return new WaitForSeconds(delay);
        ResetGame();
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(0);
    }
}
