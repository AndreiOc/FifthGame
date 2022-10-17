using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MoveController
{
    protected override void OnCantMove<T>(T component)
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
