using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : EnvironmentObject
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    protected override void CollisionHandler(Collision2D collision)
    {
        base.CollisionHandler(collision);
    }
    protected override void OnDamaged(float damage)
    {
        base.OnDamaged(damage);
    }
    protected override void OnRemove()
    {
        base.OnRemove();
    }
}
