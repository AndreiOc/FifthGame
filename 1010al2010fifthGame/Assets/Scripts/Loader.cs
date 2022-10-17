using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour
{
    public GameObject _gamemanager;
    private void Awake() {
        if(GameManager._instance == null)
        {
            Instantiate(_gamemanager);
        }
    }
}
