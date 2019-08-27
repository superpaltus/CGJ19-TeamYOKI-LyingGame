using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Grenade : MonoBehaviour
{
    public float grenadeLifeTime = 1f;

    public float flyingTimer = 1f;

    public Vector2 targetDir;

    void Update()
    {
        if (flyingTimer > 0)
        {
            GetComponent<Rigidbody2D>().AddForce(targetDir * 30 * flyingTimer, ForceMode2D.Impulse);
            flyingTimer -= Time.deltaTime;
        }
        else
        {
            GrenadeLifeTimeDecrease();
        }
    }

    private void GrenadeLifeTimeDecrease()
    {
        grenadeLifeTime -= Time.deltaTime;
        if (grenadeLifeTime <= 0f)
        {
            // BOOM! example
            var psEmitter = GetComponent<ParticleSystem>().emission;
            psEmitter.enabled = true;
            StartCoroutine (Destroying());
        }
    }

    IEnumerator Destroying()
    {
        yield return new WaitForSeconds(.2f);
        Destroy(gameObject);
    }

    //void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.layer == LayerMask.NameToLayer("Obstacles"))
    //    {
    //        Destroy(gameObject);
    //    }
    //}
}
