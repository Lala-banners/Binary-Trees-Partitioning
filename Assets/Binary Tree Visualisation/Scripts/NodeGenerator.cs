using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeFramework;

public class NodeGenerator : MonoBehaviour
{
    [SerializeField, Range(1, 10), Tooltip("How many nodes there are.")]
    private int nodeCount = 5;

    [SerializeField, Tooltip("The range of the nodes.")]
    private Vector2Int nodeRange = new Vector2Int(10, 100);

    //Reference to binary tree, create a binary tree first
    public static BinaryTree Tree = new BinaryTree();

    /// <summary>
    /// Whenever a variable is changed, this runs. Modifies the variables as we change them.
    /// </summary>
    private void OnValidate()
    {
        //A way to confine the x and y values 
        //Prevents the x range from going above the y range
        nodeRange.x = Mathf.Clamp(nodeRange.x, 10, Mathf.Max(nodeRange.y - 1, 10));

        //Prevents the y range from going above the x range
        nodeRange.y = Mathf.Clamp(nodeRange.y, Mathf.Max(nodeRange.x + 1, 11), 100);
    }


    // Start is called before the first frame update
    void Start() 
    {
        //Setup the node factory for this gameobject and generate a list of numbers
        NodeFactory.Setup(gameObject); 
        List<int> numbers = GenerateNumbers();

        //Loop until we have no items left
        while (numbers.Count > 0)
        {
            //Create a new node using the last item in the list
            Node newNode = NodeFactory.Create(numbers[numbers.Count - 1]);

            //Insert the new node into the tree. Reset Root every time
            Tree.Root = Tree.Insert(Tree.Root, newNode);

            //Remove the last item in the list
            numbers.RemoveAt(numbers.Count - 1);
        }
    }

    /// <summary>
    /// Generates a list of numbers randomly selected from all possible range values. 
    /// No repeating numbers because each time a node is removed when chosen. 
    /// EG random damage, lootboxes etc.
    /// </summary>
    private List<int> GenerateNumbers()
    {
        //All values contains every possible number in the range
        List<int> allValues = new List<int>();

        //Generated is the randomly selected numbers from allValues
        List<int> generated = new List<int>();

        //Populate the allValues list with the numbers from the range. P.S don't need curly brackets if only one line.
        for (int i = nodeRange.x; i <= nodeRange.y; i++)
            allValues.Add(i);

        //Generate the nodeCount amount of numbers
        for (int i = 0; i < nodeCount; i++)
        {
            //Select an index within the whole size of the list
            int index = Random.Range(0, allValues.Count);

            //Copy the number at the index from allValues into generated
            generated.Add(allValues[index]);
            allValues.RemoveAt(index);
        }

        return generated;
    }

}
