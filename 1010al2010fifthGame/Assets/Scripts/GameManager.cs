using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public float _turnDelay = .1f;

    public static GameManager _instance = null;
    public BoardManager _boardScript;
    public int _playerfoodPoints = 100;
    public float _level = 3f;
    [HideInInspector] public bool _playerTurn = true;
    private List<EnemyController> _enemies = new List<EnemyController>();
    private bool _enemiesTurn;
    // Start is called before the first frame update
    void Awake()
    {
        if(_instance == null)
            _instance = this;
        else if(_instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        _boardScript = GetComponent<BoardManager>();
        InitGame();
    }
    private void Update()
    {
        if(_playerTurn || _enemiesTurn)
            return;
        StartCoroutine(MoveEnemies());
    }

    public void AddEnemyToList(EnemyController script)
    {
        _enemies.Add(script);
    }
    public void GameOver()
    {
        enabled = false;
    }

    void InitGame()
    {
        _enemies.Clear();
        _boardScript.SetUpScene(_level);

    }
    // Update is called once per frame
    IEnumerator MoveEnemies()
    {
        _enemiesTurn = true;
        yield return new WaitForSeconds(_turnDelay);
        if(_enemies.Count == 0)
        {
            yield return new WaitForSeconds(_turnDelay);
        }
        foreach (var item in _enemies)
        {
            item.MoveEnemy();
            yield return new WaitForSeconds(item.moveTime);
        }
        _playerTurn = true;
        _enemiesTurn = false;
    }
}
