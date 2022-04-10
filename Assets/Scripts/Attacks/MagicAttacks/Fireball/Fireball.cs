using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float speed = 110f;
    public Rigidbody2D theRB;
    [SerializeField] private GameObject sprite;
    private bool isAlive;
    [SerializeField] private ParticleSystem ps;
    // Start is called before the first frame update
    void Start()
    {
        
        ps.Stop();
        isAlive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(isAlive==true)
            theRB.velocity = transform.right * speed;       
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            BaseEnemy enemy;
            enemy = other.gameObject.GetComponent<BaseEnemy>();
            enemy.takeDamage(10);
            isAlive = false;
            theRB.velocity = Vector2.zero;
            ps.Play();
            sprite.SetActive(false);
            Destroy(gameObject,0.3f);

        }
        if (other.tag == "NonDestroyableObject")
        {
            Destroy(gameObject);

        }

    }
}
