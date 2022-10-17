using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour
{
    [Serializable]
    public class Count
    {
        public int _max;
        public int _min;
        public Count (int max, int min)
        {
            _max = max;
            _min = min;
        }
    }

    public int _columns = 8;
    public int _rows = 8;
    public Count _wallCount = new Count(5,9);
    public Count _foodCount = new Count(5,9);
    public GameObject _exit;
    public GameObject[] _prefabs;
    public GameObject[] _floorTiles;
    public GameObject[] _wallTiles;
    public GameObject[] _enemyTiles;
    public GameObject[] _foodTiles;
    public GameObject[] _outerWallTiles;

    /// <summary>
    /// Serve a tenere pulita la nostra gerarchia affinchè tutti diventino suoi figli e non vada ad incasiare tutto 
    /// </summary>
    private Transform _boardHolder;
    private List<Vector3> _gridPosition = new List<Vector3>();

    void InitialList()
    {
        _gridPosition.Clear();

        for (int i = 1; i < _columns -1; i++)
        {
            for (int j = 1; j < _rows -1; j++)
            {
                _gridPosition.Add(new Vector3(i,j,0f)); 
            }
        }
    }


    void BoardSetup()
    {
        _boardHolder = new GameObject("Board").transform;
        for (int i = -1; i < _columns + 1; i++)
        {
            for (int j = -1; j < _rows + 1; j++)
            {
                GameObject toInstantiate = _floorTiles[0];
                if(i == -1 || i == _columns || j == -1|| j == _rows)
                    toInstantiate = _outerWallTiles[0];
                GameObject instante = Instantiate(toInstantiate,new Vector3(i,j,0f),Quaternion.identity) as GameObject;
                instante.transform.SetParent(_boardHolder);
            }
        }    
    }

    Vector3 RandomPosition()
    {
        int randomIndex = Random.Range(0,_gridPosition.Count);
        Vector3 randomPosition = _gridPosition[randomIndex];
        _gridPosition.RemoveAt(randomIndex);
        return randomPosition;
    }

    void LayoutObjectRandomAt(GameObject[]array,int minium, int maxium)
    {
        int objectCount = Random.Range(minium,maxium + 1);
        for (int i = 0; i < objectCount; i++)
        {
            Vector3 randomPosition = RandomPosition();
            GameObject tileChoice = array[Random.Range(0,array.Length)];
            GameObject instante = Instantiate(tileChoice,randomPosition,Quaternion.identity);
            instante.transform.SetParent(_boardHolder);

        }
    }


    public void SetUpScene(float level)
    {
        BoardSetup();
        InitialList();
        LayoutObjectRandomAt(_wallTiles,_wallCount._min,_wallCount._max);
        LayoutObjectRandomAt(_foodTiles,_foodCount._min,_foodCount._max);
        int enemyCount = (int)Mathf.Log(level,2f);
        LayoutObjectRandomAt(_enemyTiles,enemyCount,enemyCount);
        Instantiate(_exit,new Vector3(_columns - 1, _rows -1, 0f),Quaternion.identity);
    }

}
