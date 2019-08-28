using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityHurtable : MonoBehaviour
{

    public int health = 5;
    public int maxHealth = 5;
    public string entityName = "null";
    public float flashSpeed = 1.0f;

    float flashingCounter;

    float randChance;

    public void ChangeHealth(int _change)
    {
        if(health + _change > maxHealth)
        {
            health = maxHealth;
        } else if(health + _change < 0)
        {
            health = 0;
        } else
        {
            health += _change;
        }
    }

    public int GetHealth()
    {
        return health;
    }

    public virtual void Start()
    {
        flashingCounter = flashSpeed * 2;
    }

    public void SetHealth(int tempHealth)
    {
        health = tempHealth;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public void SetEntityName(string _name)
    {
        entityName = _name;
    }

    public string GetEntityName()
    {
        return entityName;
    }

    public void SetMaxHealth(int tempMaxHealth)
    {
        maxHealth = tempMaxHealth;
    }

    protected virtual void Die()
    {
         
        ParticleManager.instance.Create("poof", new Vector2(transform.position.x, transform.position.y + 0.25f), Quaternion.identity);
        Destroy(gameObject);
    }

    public void CheckForDeath()
    {
        if (health <= 0)
        {
            Die();
        }
    }

    public virtual void OnEntityHit()
    {
        ChangeHealth(-Player.currentDamage);
        flashSpeed = 4f;
        FlashWhite();

    }

    protected virtual void OnEntityCollidingWith(GameObject _collision)
    {

        
    }

    protected virtual void OnEntityEnterColliderWith(GameObject _collision)
    {
        
    }

    protected virtual void OnEntityExitColliderWith(GameObject _collision)
    {

    }


    public void FlashWhite()
    {
        flashingCounter = 0;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Destroy(collision.gameObject);
            OnEntityHit();
        }

    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        OnEntityCollidingWith(collision.gameObject);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        OnEntityEnterColliderWith(collision.gameObject);
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        OnEntityExitColliderWith(collision.gameObject);
    }

    public void UpdateFlashingCounter(float speed)
    {
        if (flashingCounter < flashSpeed / 2)
        {
            flashingCounter += Time.deltaTime;
        }


        if (flashingCounter > 0 && flashingCounter < 0.5 / speed)
        {
            gameObject.GetComponent<SpriteRenderer>().material.SetFloat("_FlashAmount", 1.0f);
        }
        else if (flashingCounter > 0.5 / speed && flashingCounter < 1 / speed)
        {
            gameObject.GetComponent<SpriteRenderer>().material.SetFloat("_FlashAmount", 0.0f);
        }
    }


    public virtual void Update()
    {
        UpdateFlashingCounter(flashSpeed);
        CheckForDeath();
    }

}