using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MoveController
{


    private Animator _animator;
    private Transform _target;
    private bool _skipMove;
    int _playerDamage = 10;
    // Start is called before the first frame update
    protected override void Start()
    {
        GameManager._instance.AddEnemyToList(this);
        _animator = GetComponent<Animator>();
        _target = GameObject.FindGameObjectWithTag("Player").transform;
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    protected override void AttemptMove<T>(int xDir, int yDir)
    {
        if(_skipMove)
        {
            _skipMove = false;
            return;
        }

        base.AttemptMove<T>(xDir, yDir);
        _skipMove = true;
    }

    public void MoveEnemy()
    {
        int xDir = 0;
        int yDir = 0;
        if(Mathf.Abs(_target.position.x - transform.position.x) < float.Epsilon)
            yDir = _target.position.x > transform.position.x ? 1 : -1;
        else
            xDir = _target.position.y > transform.position.y ? 1 : -1;
        AttemptMove <PlayerController> (xDir,yDir);

    }
    protected override void OnCantMove<T>(T component)
    {
        PlayerController hitPlayer = component as PlayerController;
        _animator.SetTrigger("enemtIsAttacking");
        hitPlayer.LoseFood(_playerDamage);
    }
}
