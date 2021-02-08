using UnityEngine;

using TextMeshPro = TMPro.TextMeshPro;

namespace NodeFramework
{
    public enum NodeType
    {
        Root,
        Left,
        Right
    }

    public class Node : MonoBehaviour
    {
        public int Value 
        {
            get => val;
            set
            {
                val = value;
                text.text = val.ToString();
            }
        }

        public Node Left { get; set; } = null;
        public Node Right { get; set; } = null;
        public NodeType Type { get; private set; } = NodeType.Root;
        public LineRenderer LeftConnector => leftConnector; //Visual
        public LineRenderer RightConnector => rightConnector; //Visual

        [SerializeField]
        private TextMeshPro text;
        [SerializeField]
        private LineRenderer leftConnector;
        [SerializeField]
        private LineRenderer rightConnector;
        [SerializeField]
        private MeshRenderer meshRenderer;
        [SerializeField]
        private Color activeColor = Color.magenta;

        private float separation = 0;
        private int val = 0;

        #region Operator Overloads - compares nodes 
        public static bool operator >(Node _a, Node _b)
        {
            return _a.val > _b.val;
        }

        public static bool operator <(Node _a, Node _b)
        {
            return _a.val < _b.val;
        }

        public static bool operator >=(Node _a, Node _b)
        {
            return _a.val >= _b.val;
        }

        public static bool operator <=(Node _a, Node _b)
        {
            return _a.val <= _b.val;
        }

        public static bool operator >(Node _a, int _b)
        {
            return _a.val > _b;
        }

        public static bool operator <(Node _a, int _b)
        {
            return _a.val < _b;
        }

        public static bool operator >=(Node _a, int _b)
        {
            return _a.val >= _b;
        }

        public static bool operator <=(Node _a, int _b)
        {
            return _a.val <= _b;
        }

        public static bool operator >(int _a, Node _b)
        {
            return _a > _b.val;
        }

        public static bool operator <(int _a, Node _b)
        {
            return _a < _b.val;
        }

        public static bool operator >=(int _a, Node _b)
        {
            return _a >= _b.val;
        }

        public static bool operator <=(int _a, Node _b)
        {
            return _a <= _b.val;
        }

        public override bool Equals(object _other)
        {
            if(!(_other is Node))
                return false;

            Node otherNode = _other as Node;

            return otherNode.val == val;
        }

        public override int GetHashCode()
        {
            return val.GetHashCode();
        }
        #endregion

        private void OnMouseUpAsButton()
        {
            //Highlight the path to this node HERE
            NodeGenerator.Tree.Traverse(NodeGenerator.Tree.Root, this);
        }

        public void Activate()
        {
            meshRenderer.material.color = activeColor;
        }

        public void Setup(Node _parent, NodeType _type) //Takes parent node and moves it
        {
            Type = _type;
            transform.parent = _parent != null ? _parent.transform : transform.parent;
            separation = _parent != null ? _parent.separation * 0.5f : 10;
            transform.localPosition = (transform.right * CalculateSeparation()) - CalculateVerticalOffset();
        }

        private float CalculateSeparation() //Determines if left or right and creates tree format
        {
            switch(Type)
            {
                case NodeType.Right:
                    return separation * 0.5f;
                case NodeType.Left:
                    return -(separation * 0.5f);
            }

            return 0;
        }

        private Vector3 CalculateVerticalOffset() //Move down by 2 or not move at all
        {
            switch (Type) //Fall through case, how to have multiple cases do one thing
            {
                case NodeType.Right:
                case NodeType.Left:
                    return transform.up * 2;
            }

            return Vector3.zero;
        }
    }
}