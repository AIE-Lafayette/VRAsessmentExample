using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnerBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject _spawnObject;
    [SerializeField]
    private float _spawnDelay = 1;
    [SerializeField]
    private UnityEvent _onSpawn;
    private bool _spawningObject;

    private IEnumerator SpawnObject()
    {
        _spawningObject = true;
        yield return new WaitForSeconds(_spawnDelay);
        Instantiate(_spawnObject, transform.position, transform.rotation);
        _spawningObject = false;
        _onSpawn?.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_spawningObject)
            StartCoroutine(SpawnObject());
    }
}
