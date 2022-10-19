using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MoveController
{


    public int _wallDamage;
    public int _pointFood = 10;
    public int _pointSoda = 20;
    public float restartLevelDay = 1f;
    
    private Animator _animator;
    private int _food;


    protected override void Start()
    {
        _animator = GetComponent<Animator>();
        _food = GameManager._instance._playerfoodPoints;
        base.Start();

    }
    private void OnDisable()
    {
        GameManager._instance._playerfoodPoints = _food;
    }
    // Update is called once per frame
    void Update()
    {
        if(!GameManager._instance._playerTurn)
            return;
        Debug.Log("Mi MUOVO");
        int horizontal = 0;
        int vertical = 0;
        //prendo gli input
        horizontal = (int)Input.GetAxisRaw("Horizontal");
        vertical = (int)Input.GetAxisRaw("Vertical");
        Debug.Log(horizontal + " " + vertical);
        if(horizontal!=0)
            vertical = 0;
        if(horizontal!=0 || vertical!=0)
            AttemptMove <Wall> (horizontal,vertical);


    }
    private void CheckIfGameOver()
    {
        if(_food <=0)
        {
            GameManager._instance.GameOver();
        }
    }

    protected override void AttemptMove<T>(int xDir, int yDir)
    {
        _food--;
        base.AttemptMove<T>(xDir, yDir);
        //RaycastHit2D hit2D;
        CheckIfGameOver();
        GameManager._instance._playerTurn = false;;
    }

    protected override void OnCantMove<T>(T component)
    {
        Wall hitWall = component as Wall;
        hitWall.DamageWall (_wallDamage);
        //_animator.SetTrigger("isAttacking");
    }

    private void Restart()
    {
        //Application.LoadLevel(Application.loadedLevel);
        SceneManager.LoadScene("Scene");
    }

    public void LoseFood(int loss)
    {
        //_animator.SetTrigger("isGettingHit");
        _food -= loss;
        CheckIfGameOver();
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Exit")
        {
            Invoke("Restart",restartLevelDay);
            enabled = false;
        }
        else if (other.tag == "Food")
        {
            _food += _pointFood;
            other.gameObject.SetActive(false);
        }
    }
}
