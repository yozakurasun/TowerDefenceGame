using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject[] _enemy;
    [SerializeField] private Transform _enemyParent;
    [SerializeField] private float _spawnDelay = 2f;
    [SerializeField] private GameObject _winText;
    [SerializeField] private GameObject _loseText;
    [SerializeField] private GameObject _enemyBase;
    [SerializeField] private GameObject _playerBase;
    private bool _isFirstSpawn = false;
    private float _firstSpawnDlay = 3f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (_firstSpawnDlay <= 0)
        {
            _isFirstSpawn = true;
        }
        if (_firstSpawnDlay >= 0) _firstSpawnDlay -= Time.deltaTime;

        if (!_isFirstSpawn) return;

        SpawnEnemy();

        if (_enemyBase == null) 
        {
            YouWin();
        }
        if(_playerBase == null)
        {
            YouLose();
        }
    }

    private void SpawnEnemy()
    {
        _spawnDelay -= Time.deltaTime;
        if (_spawnDelay <= 0)
        {
            int enemyIndex = Random.Range(0, _enemy.Length);
            float y = Random.Range(-2.9f, -3.1f);
            Instantiate(_enemy[enemyIndex], new Vector3(6, y, 0), Quaternion.identity, _enemyParent);
            _spawnDelay += Random.Range(1f, 5f);
        }
    }

    private void YouWin()
    {
        _winText.SetActive(true);
        Time.timeScale = 0;
    }

    private void YouLose()
    {
        _loseText.SetActive(true);
        Time.timeScale = 0;
    }
}
