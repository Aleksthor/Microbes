using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waste : MonoBehaviour
{
    float decompose_timer = 0;
    float decompose_time = 30;
    float energy = 0;
    [SerializeField] GameObject food_prefab;
    Rigidbody2D rigid_body;
    private void Awake()
    {
        rigid_body = GetComponent<Rigidbody2D>();
    }
    public void Setup(float _energy)
    {
        energy = _energy;
        decompose_time = Random.Range(10,40);
        decompose_timer = 0;
    }

    private void Update()
    {
        decompose_timer += Time.deltaTime;
        if (decompose_timer > decompose_time)
        {          
            do
            {
                GameObject go = GameController.instance.object_pool.InstantiateFood(transform.position + new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f)));
                float random = Random.Range(2.5f, 35f);
                if (energy > random)
                {
                    go.GetComponent<Food>().Setup(random);
                    energy -= random;
                }
                else
                {
                    go.GetComponent<Food>().Setup(energy);
                    energy = -1;
                }
            } while (energy > 0);

            GameController.instance.object_pool.DestroyWaste(gameObject);
        }
        Vector2 velocity = rigid_body.velocity;
        if (transform.position.x < -GameController.instance.width)
        {
            rigid_body.MovePosition(new Vector2(GameController.instance.width, rigid_body.position.y));
        }
        if (transform.position.x > GameController.instance.width)
        {
            rigid_body.MovePosition(new Vector2(-GameController.instance.width, rigid_body.position.y));
        }
        if (transform.position.y < -GameController.instance.height)
        {
            rigid_body.MovePosition(new Vector2(rigid_body.position.x, GameController.instance.height));
        }
        if (transform.position.y > GameController.instance.height)
        {
            rigid_body.MovePosition(new Vector2(rigid_body.position.x, -GameController.instance.height));
        }
        rigid_body.velocity = velocity; 
        rigid_body.velocity = Vector2.ClampMagnitude(rigid_body.velocity, 4);
    }

    public float GetEnergy()
    {
        return energy;
    }
}
