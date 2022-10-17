using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance = null;
    public BoardManager _boardScript;
    public int _playerbeerPoints = 100;
    public float _level = 3f;
    [HideInInspector] public bool _playerTurn = true;
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

    public void GameOver()
    {
        enabled = false;
    }

    void InitGame()
    {
        _boardScript.SetUpScene(_level);
    }
    // Update is called once per frame

}
