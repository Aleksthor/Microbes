using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.VR;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    [SerializeField] GameObject food_prefab;
    [SerializeField] GameObject organism_prefab;
    [SerializeField] GameObject waste_prefab;
    List<GameObject> organisms = new List<GameObject>();

    [SerializeField] int max_capasity = 65;
    [SerializeField] int number_organisms = 0;

    public float width = 50f;
    public float height = 50f;
    public ObjectPool object_pool = new ObjectPool();   


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 500; i++)
        {
            GameObject go = Instantiate(waste_prefab,Vector2.zero,Quaternion.identity);
            object_pool.DestroyWaste(go);
        }
        for (int i = 0; i < 2500; i++)
        {
            GameObject go = Instantiate(food_prefab, Vector2.zero, Quaternion.identity);
            object_pool.DestroyFood(go);
        }


        for (int i = 0; i < 50;  i++)
        {
            Vector2 random_pos = new Vector2(Random.Range(-width, width), Random.Range(-height, height));
            organisms.Add(Instantiate(organism_prefab, random_pos, Quaternion.identity));
        }
        for (int i = 0; i < 750; i++)
        {
            int r = Random.Range(1, 4);
            Vector2 random_pos = new Vector2(Random.Range(-width, width), Random.Range(-height, height));
            for (int j = 0; j < r; j++)
            {              
                GameObject go = object_pool.InstantiateFood(random_pos + (rotate(Vector2.up,Random.Range(0f,360f)) * Random.Range(0.5f,3f)));
                float random = Random.Range(3f, 15f);
                go.GetComponent<Food>().Setup(random);

            }

        }
        number_organisms = organisms.Count;
    }
    private void Update()
    {

        for (int i = organisms.Count - 1; i >= 0; i--)
        {
            if (organisms[i] != null)
            {
                if (organisms[i].GetComponent<Organism>().dead)
                {
                    GameObject go = organisms[i];
                    organisms.RemoveAt(i);
                    Destroy(go);
                }
            }
            else
            {
                organisms.RemoveAt(i);
            }
        }
        number_organisms = organisms.Count;
    }

    public float CheckTotalEnergy()
    {
        Collider2D[] all_colliders = Physics2D.OverlapCircleAll(Vector2.zero, 75);
        float result = 0;
        foreach (Collider2D collider in all_colliders)
        {
            switch(collider.tag)
            {
                case "Food":
                    result += collider.GetComponent<Food>().energy;
                    break;
                case "Organism":
                    result += collider.GetComponent<Organism>().GetEnergy();
                    break;
                case "Egg":
                    result += collider.GetComponent<Egg>().GetEnergy();
                    break;
                case "Waste":
                    result += collider.GetComponent<Waste>().GetEnergy();
                    break;
                default: 
                    break;
            }
        }

        return result;
    }

    public GameObject InstantiateOrganism(Vector3 pos)
    {
        GameObject go = Instantiate(organism_prefab, pos, Quaternion.identity);
        organisms.Add(go);
        return go;
    }

    public bool CanBreedMoreOrganisms()
    {
        return organisms.Count < max_capasity;
    }
    public static Vector2 rotate(Vector2 v, float delta)
    {
        float rad = Mathf.Deg2Rad * delta;
        return new Vector2(
            v.x * Mathf.Cos(rad) - v.y * Mathf.Sin(rad),
            v.x * Mathf.Sin(rad) + v.y * Mathf.Cos(rad)
        );
    }

    public GameObject GetWaste()
    {
        return Instantiate(waste_prefab, Vector2.zero, Quaternion.identity);
    }
    public GameObject GetFood()
    {
        return Instantiate(food_prefab, Vector2.zero, Quaternion.identity);
    }
}


[System.Serializable]
public class ObjectPool
{
    Vector2 object_pool_pos = new Vector2(750, 0);
    public List<GameObject> disabled_waste = new List<GameObject>();
    public List<GameObject> disabled_food = new List<GameObject>();

    public void DestroyWaste(GameObject obj)
    {
        disabled_waste.Add(obj);
        obj.SetActive(false);
        obj.transform.position = object_pool_pos;
    }
    public void DestroyFood(GameObject obj)
    {
        disabled_food.Add(obj);
        obj.SetActive(false);
        obj.transform.position = object_pool_pos;
    }

    public GameObject InstantiateWaste(Vector3 pos)
    {
        if (disabled_waste.Count > 0)
        {
            GameObject go = disabled_waste[disabled_waste.Count - 1];
            disabled_waste.RemoveAt(disabled_waste.Count - 1);
            go.SetActive(true);
            go.transform.position = pos;
            go.GetComponent<Rigidbody2D>().position = pos;
            return go;
        }
        else
        {
            for (int i = 0; i < 20; i++)
            {
                GameObject obj = GameController.instance.GetWaste();
                obj.transform.position = object_pool_pos;
                obj.SetActive(false);
                disabled_waste.Add(obj);             
            }
            GameObject go = disabled_waste[disabled_waste.Count - 1];
            go.SetActive(true);
            go.transform.position = pos;
            go.GetComponent<Rigidbody2D>().position = pos;
            disabled_waste.RemoveAt(disabled_waste.Count - 1);
            return go;
        }
    }
    public GameObject InstantiateFood(Vector3 pos)
    {
        if (disabled_food.Count > 0)
        {
            GameObject go = disabled_food[disabled_food.Count - 1];
            go.SetActive(true);
            go.transform.position = pos;
            go.GetComponent<Rigidbody2D>().position = pos;
            disabled_food.RemoveAt(disabled_food.Count - 1);
            return go;
        }
        else
        {
            for (int i = 0; i < 20; i++)
            {
                GameObject obj = GameController.instance.GetFood();
                obj.transform.position = object_pool_pos;
                obj.SetActive(false);
                disabled_food.Add(obj);
            }
            GameObject go = disabled_food[disabled_food.Count - 1];
            go.SetActive(true);
            go.transform.position = pos;
            go.GetComponent<Rigidbody2D>().position = pos;
            disabled_food.RemoveAt(disabled_food.Count - 1);
            return go;
        }
    }
}