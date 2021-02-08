using NodeFramework;

public class BinaryTree 
{
    //Needs a root/top node
    //EG numbers: 9, 12, 5, 3, 10
    //Root node is 10 (far right)
    //As we travel down the tree, the root will change depending on the next number.
    //EG 10 is root for 3 and 12. 3 root for 2 and 5 etc.
    public Node Root;

    public Node Insert(Node _root, Node _inserting)
    {
        //Detect if root node is not set, make the root node the inserting one.
        if(_root == null)
        {
            _root = _inserting;
            _root.Value = _inserting.Value;
            _root.Setup(null, NodeType.Root); //Set up the root as if it were a root node, even if it isn't
        }
        else if(_inserting < _root) //Should this be the left node?
        {
            //It should so set up the node
            _root.Left = Insert(_root.Left, _inserting);
            _root.Left.Setup(_root, NodeType.Left);
        }
        else
        {
            //Else it must be the right node, so set up right node
            _root.Right = Insert(_root.Right, _inserting); 
            _root.Right.Setup(_root, NodeType.Right);
        }

        //Root node has been set so return the node
        return _root;
    }
}