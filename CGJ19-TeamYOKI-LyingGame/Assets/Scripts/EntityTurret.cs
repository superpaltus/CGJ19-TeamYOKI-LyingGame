using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityTurret : EntityHurtable
{
    protected override void Die()
    {
        ParticleManager.instance.Create("poof", new Vector2(transform.position.x, transform.position.y + 0.25f), Quaternion.identity);
        Destroy(transform.parent.gameObject);
    }
    public override void OnEntityHit()
    {
        health--;

        flashSpeed = 4f;
        FlashWhite();
    }
}
