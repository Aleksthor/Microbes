using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{

    [SerializeField] float mass = 1;
    [SerializeField] float density = 150;
    [SerializeField] public float energy = 0;

    Rigidbody2D rigid_body;

    private void Awake()
    {
        density = 3;
        rigid_body = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        energy = mass * density;
        transform.localScale = new Vector3(CubeRoot(mass), CubeRoot(mass), CubeRoot(mass));
        GetComponent<Rigidbody2D>().mass = mass * 5;
        Vector2 velocity = rigid_body.velocity;
        if (transform.position.x < -GameController.instance.width)
        {
            rigid_body.MovePosition(new Vector2(GameController.instance.width, transform.position.y));
        }
        if (transform.position.x > GameController.instance.width)
        {
            rigid_body.MovePosition(new Vector2(-GameController.instance.width, transform.position.y));
        }
        if (transform.position.y < -GameController.instance.height)
        {
            rigid_body.MovePosition(new Vector2(transform.position.x, GameController.instance.height));
        }
        if (transform.position.y > GameController.instance.height)
        {
            rigid_body.MovePosition(new Vector2(transform.position.x, -GameController.instance.height));
        }
        rigid_body.velocity = velocity;
        rigid_body.velocity = Vector2.ClampMagnitude(rigid_body.velocity, 4);
    }

    public void Setup(float _energy)
    {
        mass = _energy / density;
    }

    public float Bite(float strength)
    { 

        mass -= strength;
        if (mass < 0)
        {
            GameController.instance.object_pool.DestroyFood(gameObject);
            return (strength + mass) * density;
        }
        if (mass < 0.1f)
        {
            GameController.instance.object_pool.DestroyFood(gameObject);
            return (strength + mass) * density;

        }
        return strength * density;

    }

    float CubeRoot(float d)
    {

        if (d < 0.0f)
        {

            return -Mathf.Pow(-d, 1f / 3f);
        }

        else
        {

            return Mathf.Pow(d, 1f / 3f);
        }
    }
}
