using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Genes
{
    [SerializeField] public float growth_rate = 1;
    [SerializeField] public float mutation_chance = 45f;
    [SerializeField] public float mutation_factor = 0.25f;
    [SerializeField] public float max_move_speed = 3;
    [SerializeField] public float max_turn_speed = 5;
    [SerializeField] public float mature_age = 0.15f;
    [SerializeField] public float pregnancy_period = 25;
    [SerializeField] public float incubation_period = 15;
    [SerializeField] public float size_of_newborn = 0.5f;
    [SerializeField] public float view_distance = 4;
    [SerializeField] public float view_angle = 30;
    [SerializeField] public Color body_color = Color.white;
    [SerializeField] public Color eye_color = Color.white;
    [SerializeField] public float strength = 1;

    public void New()
    {
        growth_rate = Random.Range(0.8f,1.2f);
        mutation_chance = Random.Range(40f, 80f);
        mutation_factor = Random.Range(0.1f, 0.5f);
        max_move_speed = Random.Range(2f, 5f);
        max_turn_speed = Random.Range(3f, 6f);
        mature_age = Random.Range(0.05f, 0.2f);
        pregnancy_period = Random.Range(30f, 60f);
        incubation_period = Random.Range(10f, 20f);
        size_of_newborn = Random.Range(0.4f, 0.6f);
        view_distance = Random.Range(3f, 12f);
        view_angle = Random.Range(20f, 60f);
        body_color = new Color(Random.Range(0.5f,1f), Random.Range(0.5f, 1f), Random.Range(0.5f, 1f));
        eye_color = new Color(Random.Range(0.5f, 1f), Random.Range(0.5f, 1f), Random.Range(0.5f, 1f));
        strength = Random.Range(0.8f, 1.33f);
    }
    public Genes Copy()
    {
        Genes result =  new Genes();
        result.growth_rate = growth_rate;
        result.mutation_chance = mutation_chance;
        result.mutation_factor = mutation_factor;
        result.max_move_speed = max_move_speed ;
        result.max_turn_speed = max_turn_speed ;
        result.mature_age = mature_age;
        result.pregnancy_period = pregnancy_period;
        result.incubation_period = incubation_period;
        result.size_of_newborn = size_of_newborn;
        result.view_distance = view_distance;
        result.view_angle = view_angle;
        result.body_color = body_color;
        result.eye_color = eye_color;
        result.strength = strength;

        return result;
    }
    public void GeneticAlgorithm()
    {
        int random = 0;
        int random_chance = Random.Range(0, 101);
        if (random_chance < mutation_chance)
        {
            int what_to_change = Random.Range(0, 12);
            switch (what_to_change)
            {
                case 0:
                    growth_rate += Random.Range(-1f, 1f) * 0.1f * mutation_factor;
                    growth_rate = Mathf.Clamp(growth_rate, 0.1f, 2f);
                    break;
                case 1:
                    strength += Random.Range(-1f, 1f) * 0.1f * mutation_factor;
                    strength = Mathf.Clamp(strength, 0.33f, 1.5f);
                    break;
                case 2:
                    mutation_chance += Random.Range(-1f, 1f) * mutation_factor;
                    mutation_chance = Mathf.Clamp(mutation_chance, 5f, 95f);
                    break;
                case 3:
                    mutation_factor += Random.Range(-1f, 1f) * 0.1f * mutation_factor;
                    mutation_factor = Mathf.Clamp(growth_rate, 0.01f, 2f);
                    break;
                case 4:
                    max_move_speed += Random.Range(-1f, 1f) * mutation_factor;
                    max_move_speed = Mathf.Clamp(max_move_speed, 1.5f, 6.5f);
                    break;
                case 5:
                    max_turn_speed += Random.Range(-1f, 1f) * mutation_factor;
                    max_turn_speed = Mathf.Clamp(max_turn_speed, 1.33f, 6f);
                    break;
                case 6:
                    mature_age += Random.Range(-1f, 1f) * 0.1f * mutation_factor;
                    mature_age = Mathf.Clamp(mature_age, 0.1f, 0.3f);
                    break;
                case 7:
                    pregnancy_period += Random.Range(-1f, 1f) * mutation_factor;
                    pregnancy_period = Mathf.Clamp(pregnancy_period, 7f, 40f);
                    break;
                case 8:
                    incubation_period += Random.Range(-1f, 1f) * mutation_factor;
                    incubation_period = Mathf.Clamp(incubation_period, 5f, 20f);
                    break;
                case 9:
                    size_of_newborn += Random.Range(-1f, 1f) * 0.1f * mutation_factor;
                    size_of_newborn = Mathf.Clamp(size_of_newborn, 0.1f, 1f);
                    break;
                case 10:
                    view_distance += Random.Range(-1f, 1f) * mutation_factor;
                    view_distance = Mathf.Clamp(view_distance, 1, 15f);
                    break;
                case 11:
                    view_angle += Random.Range(-1f, 1f) * mutation_factor;
                    view_angle = Mathf.Clamp(view_angle, 10, 300f);
                    break;
                default:
                    break;
            }


            what_to_change = Random.Range(0, 2);
            switch (what_to_change)
            {
                case 0:
                    random = Random.Range(0, 3);
                    switch (random)
                    {
                        case 0:
                            body_color += new Color(Random.Range(-1f, 1f), 0, 0) * 0.1f * mutation_factor;
                            break;
                        case 1:
                            body_color += new Color(0, Random.Range(-1f, 1f), 0) * 0.1f * mutation_factor;
                            break;
                        case 2:
                            body_color += new Color(0, 0, Random.Range(-1f, 1f)) * 0.1f * mutation_factor;
                            break;
                    }

                    body_color = new Color(Mathf.Clamp(body_color.r, 0f, 1f), Mathf.Clamp(body_color.g, 0f, 1f), Mathf.Clamp(body_color.b, 0f, 1f));
                    break;
                case 1:
                    random = Random.Range(0, 3);
                    switch (random)
                    {
                        case 0:
                            eye_color += new Color(Random.Range(-1f, 1f), 0, 0) * 0.1f * mutation_factor;
                            break;
                        case 1:
                            eye_color += new Color(0, Random.Range(-1f, 1f), 0) * 0.1f * mutation_factor;
                            break;
                        case 2:
                            eye_color += new Color(0, 0, Random.Range(-1f, 1f)) * 0.1f * mutation_factor;
                            break;
                    }
                    eye_color = new Color(Mathf.Clamp(eye_color.r, 0f, 1f), Mathf.Clamp(eye_color.g, 0f, 1f), Mathf.Clamp(eye_color.b, 0f, 1f));
                    break;
            }
        }
    }
}


public class Organism : MonoBehaviour
{
    [SerializeField] Rigidbody2D rigid_body;
    [SerializeField] GameObject egg;
    [SerializeField] GameObject waste;

    [Header("Genes")]
    [SerializeField] Genes genes = new Genes();

    [Header("Live Stats")]
    [SerializeField] public float life_time = 600;
    [SerializeField] public float max_energy_reserve = 40;
    [SerializeField] float age = 0;
    [SerializeField] float maturity = 0;
    [SerializeField] float energy_reserve = 40;
    [SerializeField] float gathered_waste = 0;
    [SerializeField] float mass = 1;

    //LOGIC
    SpriteRenderer body_renderer;
    SpriteRenderer vision_renderer;
    SpriteRenderer strength_renderer;
    SpriteRenderer turn_renderer;
    SpriteRenderer speed_renderer;

    float hungryness = 0;
    bool pregnant = false;
    float pregnant_timer = 0f;
    float energy_collected_for_egg = 0f;
    float energy_needed_for_egg = 0.5f + 15 + (150 / 20);
    float next_bowel_treshhold = 5f;
    public bool dead = false;


    [SerializeField] NeuralNetwork nn = new NeuralNetwork();


    [SerializeField] List<Sprite> strength_sprites = new List<Sprite>();
    [SerializeField] List<Sprite> speed_sprites = new List<Sprite>();
    [SerializeField] List<Sprite> vision_sprites = new List<Sprite>();
    [SerializeField] List<Sprite> turn_sprites = new List<Sprite>();
    private void Awake()
    {
        life_time = 600;
        max_energy_reserve = 40;
        age = 0;
        maturity = 0;
        energy_reserve = 40;
        gathered_waste = 0;

        //LOGIC

        hungryness = 0;
        pregnant = false;
        pregnant_timer = 0f;
        energy_collected_for_egg = 0f;
        energy_needed_for_egg = genes.size_of_newborn + 15;
        next_bowel_treshhold = Random.Range(2.5f, 50f);
        dead = false;


        genes.New();
        nn.Setup(6, new List<int>() { 8 }, 2);
        body_renderer = transform.Find("Body").GetComponent<SpriteRenderer>();
        vision_renderer = transform.Find("Vision").GetComponent<SpriteRenderer>();
        strength_renderer = transform.Find("Strength").GetComponent<SpriteRenderer>();
        turn_renderer = transform.Find("Turn").GetComponent<SpriteRenderer>();
        speed_renderer = transform.Find("Speed").GetComponent<SpriteRenderer>();
        SetSprites();
        mass = genes.size_of_newborn;
        energy_needed_for_egg = genes.size_of_newborn + 15;
    }



    public void NewBorn(Genes _genes, NeuralNetwork _nn)
    {
        genes = _genes;
        nn = _nn;
        mass = genes.size_of_newborn;
        energy_needed_for_egg = genes.size_of_newborn + 15;
        energy_reserve = 15;
        next_bowel_treshhold = Random.Range(2.5f, 50f);
        dead = false;
        SetSprites();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (dead) return;
        age += Time.fixedDeltaTime;
        maturity = age / life_time;
        float static_energy_cost = Time.fixedDeltaTime;
        energy_reserve -= static_energy_cost * 0.5f;
        gathered_waste += static_energy_cost * 0.5f;
        hungryness = energy_reserve / max_energy_reserve;
        if (age > life_time)
        {
            GameObject go = GameController.instance.object_pool.InstantiateWaste(transform.position - (transform.up * 1.5f));
            go.GetComponent<Waste>().Setup(energy_reserve + mass + energy_collected_for_egg);

            GameObject go2 = GameController.instance.object_pool.InstantiateWaste(transform.position - (transform.up * 1.5f));
            go2.GetComponent<Waste>().Setup(gathered_waste);
            dead = true;
            return;

        }
        if (energy_reserve < 0)
        {
            GameObject go = GameController.instance.object_pool.InstantiateWaste(transform.position - (transform.up * 1.5f));
            go.GetComponent<Waste>().Setup(mass + energy_collected_for_egg);
            GameObject go2 = GameController.instance.object_pool.InstantiateWaste(transform.position - (transform.up * 1.5f));
            go2.GetComponent<Waste>().Setup(gathered_waste);
            dead = true;
            return;
        }


        // EATING

        Collider2D[] food_colliders = Physics2D.OverlapCircleAll(transform.position + (transform.up * 0.4f) * transform.localScale.x, 0.4f * transform.localScale.x);

        foreach (Collider2D col in food_colliders)
        {
            if (col.gameObject != gameObject)
            {
                if (hungryness > 0.975f)
                {
                    break;
                }
                if (col.tag == "Food")
                {
                    float energy_consumed = col.GetComponent<Food>().Bite(genes.strength * mass * Time.fixedDeltaTime * 10f);
                    energy_reserve += energy_consumed;
                }
            }
        }

        // RELEASE WASTE

        if (gathered_waste > next_bowel_treshhold)
        {
            GameObject go = GameController.instance.object_pool.InstantiateWaste(transform.position - (transform.up * 1.5f));
            go.GetComponent<Waste>().Setup(gathered_waste);
            gathered_waste = 0;
            next_bowel_treshhold = Random.Range(3f, 50f);
        }


        // GROWING

        if (hungryness > 0.8f)
        {

            float gained_mass = mass * 0.01f * genes.growth_rate * Time.fixedDeltaTime;
            mass += gained_mass;
            energy_reserve -= gained_mass;

        }

        // PREGNANCY

        if (maturity > genes.mature_age && !pregnant && GameController.instance.CanBreedMoreOrganisms())
        {
            if (hungryness > 0.85f)
            {
                pregnant = true;
            }
        }
        if (pregnant)
        {
            pregnant_timer += Time.deltaTime;
            if (energy_collected_for_egg < energy_needed_for_egg)
            {
                float val = energy_needed_for_egg / genes.pregnancy_period * Time.deltaTime;
                energy_collected_for_egg += val;
                energy_reserve -= val;
            }

            if (pregnant_timer > genes.pregnancy_period && energy_collected_for_egg >= energy_needed_for_egg)
            {
                pregnant = false;
                pregnant_timer = 0f;
                energy_collected_for_egg = 0f;
                GameObject go = Instantiate(egg, transform.position - (transform.up * 1.5f), Quaternion.identity);
                go.GetComponent<Egg>().SpawnEgg(genes, nn);
            }
        }

        // VISION

        Collider2D[] all_colliders = Physics2D.OverlapCircleAll(transform.position, genes.view_distance);

        List<Collider2D> colliders = new List<Collider2D>();
        int num_organism = 0;
        int num_food = 0;
        foreach (Collider2D col in all_colliders)
        {
            if (col.gameObject != gameObject)
            {
                Vector2 direction = (col.transform.position - transform.position).normalized;
                if (Vector2.Angle(transform.up, direction) <= genes.view_angle / 2)
                {
                    if (col.tag == "Food")
                    {
                        num_food++;
                    }
                    if (col.tag == "Organism")
                    {
                        num_organism++;
                    }
                    colliders.Add(col);
                }
            }
        }


        // Find closest organism
        float distance_to_closest_organism = genes.view_distance;
        float angle_to_closest_organism = genes.view_angle;
        foreach (Collider2D col in colliders)
        {
            if (col.tag == "Organism")
            {
                if (Vector2.Distance(col.transform.position, transform.position) < distance_to_closest_organism)
                {
                    distance_to_closest_organism = Vector2.Distance(col.transform.position, transform.position);
                    Vector2 direction = (Vector2)(col.transform.position - transform.position).normalized;
                    angle_to_closest_organism = Vector2.SignedAngle(transform.up, direction);
                }
            }
        }
        // Find closest food
        float distance_to_closest_food = genes.view_distance;
        float angle_to_closest_food = genes.view_angle;
        foreach (Collider2D col in colliders)
        {
            if (col.tag == "Food")
            {
                if (Vector2.Distance(col.transform.position, transform.position) < distance_to_closest_food)
                {
                    distance_to_closest_food = Vector2.Distance(col.transform.position, transform.position);
                    Vector2 direction = (col.transform.position - transform.position).normalized;
                    angle_to_closest_food = Vector2.SignedAngle(transform.up, direction);
                }
            }
        }


        List<float> inputs = new List<float>() { distance_to_closest_organism, angle_to_closest_organism,
                                                 distance_to_closest_food, angle_to_closest_food, num_organism, num_food};

        List<float> outputs = nn.FeedForward(inputs);



        if (outputs[0] >= 0.5f)
        {
            rigid_body.AddForce(transform.up * Map(outputs[0], 0.5f, 1f, 0f, 1f) * 20f);
        }
        else if (rigid_body.velocity.magnitude > 2.5f)
        {
            rigid_body.AddForce(-1f * transform.up * Map(outputs[0], 0.5f, 0f, 0f, 1f) * 3f);
        }

        if (outputs[1] >= 0.5f)
        {
            rigid_body.AddForce(transform.right * Map(outputs[1], 0.5f, 1f, 0f, 1f) * genes.max_turn_speed);
        }
        else
        {
            rigid_body.AddForce(-1f * transform.right * Map(outputs[1], 0.5f, 0f, 0f, 1f) * genes.max_turn_speed);
        }

        rigid_body.velocity = Vector2.ClampMagnitude(rigid_body.velocity, genes.max_move_speed);
        float v = (rigid_body.velocity * rigid_body.velocity).magnitude * mass * 0.05f * Time.fixedDeltaTime;
        energy_reserve -= v;
        gathered_waste += v;
        transform.localScale = new Vector3(CubeRoot(mass), CubeRoot(mass), CubeRoot(mass));
        transform.up = rigid_body.velocity.normalized;
        body_renderer.color = genes.body_color;
        vision_renderer.color = genes.eye_color;
        strength_renderer.color = genes.body_color;
        turn_renderer.color = genes.body_color;
        speed_renderer.color = genes.body_color;

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
    }


    void SetSprites()
    {
        switch(genes.view_distance)
        {
            case float i when i < 3f:
                break;
            case float i when i >= 3f && i < 6:
                vision_renderer.sprite = vision_sprites[0];
                break;
            case float i when i >= 6f && i < 9.5f:
                vision_renderer.sprite = vision_sprites[1];
                break;
            case float i when i >= 9.5f && i <= 15:
                vision_renderer.sprite = vision_sprites[2];
                break;
        }
        switch (genes.strength)
        {
            case float i when i < 1.1f:
                break;
            case float i when i >= 1.1f && i < 1.15f:
                strength_renderer.sprite = strength_sprites[0];
                break;
            case float i when i >= 1.15f && i < 1.3f:
                strength_renderer.sprite = strength_sprites[1];
                break;
            case float i when i >= 1.3f && i <= 1.5f:
                strength_renderer.sprite = strength_sprites[2];
                break;
        }
        switch (genes.max_move_speed)
        {
            case float i when i < 3f:
                speed_renderer.sprite = speed_sprites[0];
                break;
            case float i when i >= 3f && i < 3.5f:
                speed_renderer.sprite = speed_sprites[1];
                break;
            case float i when i >= 3.5f && i <= 4.2f:
                speed_renderer.sprite = speed_sprites[2];
                break;
            case float i when i >= 4.2f && i <= 5f:
                speed_renderer.sprite = speed_sprites[3];
                break;
            case float i when i >= 5f && i <= 6f:
                speed_renderer.sprite = speed_sprites[4];
                break;
        }
        switch (genes.max_turn_speed)
        {
            case float i when i < 3f:
                break;
            case float i when i >= 3f && i < 4f:
                turn_renderer.sprite = turn_sprites[0];
                break;
            case float i when i >= 4f && i < 6f:
                turn_renderer.sprite = turn_sprites[1];
                break;
        }
        turn_renderer = transform.Find("Turn").GetComponent<SpriteRenderer>();
    }
    

    public static Vector2 rotate(Vector2 v, float delta)
    {
        float rad = Mathf.Deg2Rad * delta;
        return new Vector2(
            v.x * Mathf.Cos(rad) - v.y * Mathf.Sin(rad),
            v.x * Mathf.Sin(rad) + v.y * Mathf.Cos(rad)
        );
    }
    public static float Map(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
    public Color GetColor()
    {
        return genes.body_color;
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

    public float GetEnergy()
    {
        float result = 0;
        result += energy_reserve;
        result += mass;
        result += gathered_waste;
        result += energy_collected_for_egg;
        return result;
    }
}
