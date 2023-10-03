using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Enum for nodes to keep a type 
public enum NodeType
{
    Sensor,
    Hidden,
    Output
}
// Neuroevoluation uses these structs to store network structure information
[System.Serializable]
public class Connection
{
    public int input;
    public int output;
    public float weight;
    public bool enabled;
    public int innovation_number;
    public bool fake;

    public Connection(int _input, int _output, float _weight, bool _enabled, int _innovation_number)
    {
        input = _input;
        output = _output;
        weight = _weight;
        enabled = _enabled;
        innovation_number = _innovation_number;
    }
    public Connection(bool _fake)
    {
        fake = _fake;
    }


    public void SetEnabled(bool input)
    {
        enabled = input;
    }
}
// A node will have connections to other nodes and will be able to activate
public class Node
{
    public float value;
    public List<float> weights = new List<float>();
    public List<Node> inputs = new List<Node>();
    public int id;
    public NodeType type;

    public Node(float _value, NodeType _type)
    {
        value = _value;
        type = _type;
    }

    public void AddInput(Node node, float weight)
    {
        inputs.Add(node);
        weights.Add(weight);
    }
    public float Activate()
    {
        if (type == NodeType.Sensor) { return value; }


        float sum = 0;
        for (int i = 0; i < inputs.Count; i++)
        {        
            sum += inputs[i].value * weights[i];
        }
        value = Sigmoid(sum);

        return value;
    }

    public void Reset()
    {
        weights = new List<float>();
        inputs = new List<Node>();
    }

    public float Sigmoid(float value)
    {
        return 1f / (1f + Mathf.Exp(-5f * value));
    }
}


[System.Serializable]
public class NeuroEvolution
{
    [SerializeField] List<Connection> connections = new List<Connection>()
    {
        new Connection(0, 3, 0.7f, true, 0),
        new Connection(1, 3, 0.5f, true, 1),
        new Connection(2, 4, 0.2f, true, 2),
        new Connection(3, 4, 0.6f, true, 3)
    };
    List<Node> nodes = new List<Node>();
    public int num_inputs = 0;
    public int num_hidden = 0;
    public int num_outputs = 0;

    public void SetupNetwork(int inputs, int hidden, int outputs)
    {
        for (int i = 0; i < inputs; i++)
        {
            nodes.Add(new Node(Random.Range(0f,1f),NodeType.Sensor));
        }
        for (int i = 0; i < hidden; i++)
        {
            nodes.Add(new Node(Random.Range(0f, 1f), NodeType.Hidden));
        }
        for (int i = 0; i < outputs; i++)
        {
            nodes.Add(new Node(Random.Range(0f, 1f), NodeType.Output));
        }
        num_inputs = inputs;
        num_hidden = hidden;
        num_outputs = outputs;
    }

    public void BuildNetwork()
    {
        for (int i = 0; i < connections.Count; i++)
        {
            Connection con = connections[i];
            if (con.enabled)
            {
                nodes[con.output].AddInput(nodes[con.input], con.weight);
                nodes[con.output].id = con.output;
                nodes[con.input].id = con.input;
            }
        }
    }

    public List<float> FeedForward(List<float> inputs)
    {
        int n = 0;
        for (int i = 0; i < nodes.Count; i++)
        {
            if (nodes[i].type == NodeType.Sensor)
            {
                nodes[i].value = inputs[n];
                n++;
            }
        }
        if (n != inputs.Count)
        {
            Debug.Log("Wrong number of inputs");
        }

        for (int i = 0; i < nodes.Count; i++)
        {
            if (nodes[i].type == NodeType.Hidden)
            {
                nodes[i].Activate();
            }
        }
        List<float> outputs = new List<float>();
        for (int i = 0; i < nodes.Count; i++)
        {
            if (nodes[i].type == NodeType.Output)
            {
                outputs.Add(nodes[i].Activate());
            }
        }
        return outputs; 

    }

    public void AddNode()
    {
        Node node = new Node(Random.Range(0f,1f), NodeType.Hidden);
        node.id = nodes.Count;

        ResetAllNodes();

        int random_connection = Random.Range(0, connections.Count);
        if (!connections[random_connection].enabled)
        {
            for (int i = 0; i < 100; i++)
            {
                random_connection = Random.Range(0, connections.Count);
                if (connections[random_connection].enabled)
                {
                    break;
                }
            }
        }
        Connection con = connections[random_connection];
        connections[random_connection].SetEnabled(false);

        connections.Add(new Connection(con.input, node.id, Random.Range(-1f, 1f), true, InnovationNumber.instance.GetNumber()));
        connections.Add(new Connection(node.id, con.output, Random.Range(-1f, 1f), true, InnovationNumber.instance.GetNumber()));
        
        nodes.Add(node);
        num_hidden++;
        BuildNetwork();
    }

    public void AddConnection()
    {
        int random_input = Random.Range(0, nodes.Count);
        int random_output = Random.Range(0, nodes.Count);
        do 
        {
            random_output = Random.Range(0, nodes.Count);
        } while (random_input == random_output || nodes[random_output].type == NodeType.Sensor);

        connections.Add(new Connection(nodes[random_input].id, nodes[random_output].id, Random.Range(-1f, 1f), true, InnovationNumber.instance.GetNumber()));
        ResetAllNodes();
        BuildNetwork();
    }

    public static NeuroEvolution GetOffspring(NeuroEvolution nn, NeuroEvolution nn2)
    {
        int inputs = nn.num_inputs > nn2.num_inputs ? nn.num_inputs : nn2.num_inputs;
        int hidden = nn.num_hidden > nn2.num_hidden ? nn.num_hidden : nn2.num_hidden;
        int outputs = nn.num_outputs > nn2.num_outputs ? nn.num_outputs : nn2.num_outputs;


        NeuroEvolution ne = new NeuroEvolution();
        ne.SetupNetwork(inputs, hidden, outputs);

        List<Connection> connections_parent1 = new List<Connection>(nn.GetConnections().ToArray());
        List<Connection> connections_parent2 = new List<Connection>(nn2.GetConnections().ToArray());

        int long1 = connections_parent1.Count;
        int long2 = connections_parent2.Count;

        int longest = long1 > long2 ? 1 : 2;

        List<Connection> result = new List<Connection>();

        if (longest == 1)
        {
            for (int i = 0; i < connections_parent1.Count; i++)
            {
                int ino = connections_parent1[i].innovation_number;
                bool hit = false;
                int index = 0;
                for (int j = 0; j < connections_parent2.Count; j++)
                {
                    if (connections_parent2[j].enabled)
                    {
                        if (connections_parent2[j].innovation_number == ino)
                        {
                            hit = true;
                            index = j;
                            break;
                        }
                    }

                }



                if (hit && connections_parent1[i].enabled)
                {

                    if (Random.Range(0, 2) > 0)
                    {
                        result.Add(connections_parent1[i]);
                    }
                    else
                    {
                        result.Add(connections_parent2[index]);
                    }

                }
                else
                {
                    if (connections_parent1[i].enabled)
                    {
                        result.Add(connections_parent1[i]);
                    }
                }
                
            }
        }
        else
        {
            for (int i = 0; i < connections_parent2.Count; i++)
            {
                int ino = connections_parent2[i].innovation_number;
                bool hit = false;
                int index = 0;
                for (int j = 0; j < connections_parent1.Count; j++)
                {
                    if (connections_parent1[j].enabled)
                    {
                        if (connections_parent1[j].innovation_number == ino)
                        {
                            hit = true;
                            index = j;
                            break;
                        }
                    }

                }
                if (hit && connections_parent2[i].enabled)
                {
                    if (Random.Range(0, 2) > 0)
                    {
                        result.Add(connections_parent2[i]);
                    }
                    else
                    {
                        result.Add(connections_parent1[index]);
                    }
                }
                else
                {
                    if (connections_parent2[i].enabled)
                    {
                        result.Add(connections_parent2[i]);
                    }
                }
            }
        }

        ne.connections = result;
        ne.BuildNetwork();

        return ne;
    }

    public List<Connection> GetConnections()
    {
        return connections;
    }

    void ResetAllNodes()
    {
        for (int i = 0; i < nodes.Count; i++)
        {
            nodes[i].Reset();
        }
    }

    public void Mutate(float mutation_factor, float mutation_chance)
    {
        float random_change_weights = Random.Range(0f, 1f);
        if (random_change_weights < mutation_chance)
        {
            for (int i = 0; i < connections.Count; i++)
            {
                float random = Random.Range(0f, 1f);

                if (random < mutation_chance)
                {
                    connections[i].weight += Random.Range(-1f, 1f) * mutation_factor;
                }
            }
            return;
        }


        float random_add_node = Random.Range(0f, 1f);
        if (random_add_node < mutation_chance)
        {
            AddNode();
            return;
        }

        float random_add_connection = Random.Range(0f, 1f);
        if (random_add_node < mutation_chance)
        {
            AddConnection();
            return;
        }

    }




    public void Print()
    {
        Debug.Log("INPUT LAYER");
            
        for (int i = 0; i < nodes.Count; i++)
        {
            if (nodes[i].type == NodeType.Sensor)
            {
                Debug.Log("Node " + i + ": " + nodes[i].value);
            }
        }
        Debug.Log("HIDDEN LAYER");

        for (int i = 0; i < nodes.Count; i++)
        {
            if (nodes[i].type == NodeType.Hidden)
            {
                Debug.Log("Node " + i + ": " + nodes[i].value);
                for (int j = 0; j < nodes[i].inputs.Count; j++)
                {
                    Debug.Log("    - Connection: " + nodes[i].inputs[j].id);
                }
            }
        }

        Debug.Log("OUTPUT LAYER");

        for (int i = 0; i < nodes.Count; i++)
        {
            if (nodes[i].type == NodeType.Output)
            {
                Debug.Log("Node " + i + ": " + nodes[i].value);
                for (int j = 0; j < nodes[i].inputs.Count; j++)
                {
                    Debug.Log("    - Connection: " + nodes[i].inputs[j].id);
                }
            }
        }
    }
    


}
