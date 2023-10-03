using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour
{

    float incubation_time = 0f;
    Genes genes;
    NeuralNetwork nn;
    Rigidbody2D rigid_body;

    private void Awake()
    {
        rigid_body = GetComponent<Rigidbody2D>();
    }
    public void SpawnEgg(Genes _genes, NeuralNetwork _nn)
    {
        genes = _genes.Copy();
        nn = _nn.Copy();

        nn.GeneticAlgorithm();
        genes.GeneticAlgorithm();
    }

    private void Update()
    {
        incubation_time += Time.deltaTime;
        if (incubation_time > genes.incubation_period)
        {
            GameObject go = GameController.instance.InstantiateOrganism(transform.position);
            go.GetComponent<Organism>().NewBorn(genes, nn);
            Destroy(gameObject);
        }
        Vector2 velocity = rigid_body.velocity;
        if (transform.position.x < -200f)
        {
            rigid_body.MovePosition(new Vector2(GameController.instance.width, transform.position.y));
        }
        if (transform.position.x > 200f)
        {
            rigid_body.MovePosition(new Vector2(-GameController.instance.width, transform.position.y));
        }
        if (transform.position.y < -200f)
        {
            rigid_body.MovePosition(new Vector2(transform.position.x, GameController.instance.height));
        }
        if (transform.position.y > 200f)
        {
            rigid_body.MovePosition(new Vector2(transform.position.x, -GameController.instance.height));
        }
        rigid_body.velocity = velocity;
        rigid_body.velocity = Vector2.ClampMagnitude(rigid_body.velocity, 4);
    }

    public float GetEnergy()
    {
        return genes.size_of_newborn + 15 + (150 / 20);
    }
}
