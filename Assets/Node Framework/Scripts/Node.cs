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
        public LineRenderer LeftConnector => leftConnector;
        public LineRenderer RightConnector => rightConnector;

        [SerializeField]
        private TextMeshPro text;
        [SerializeField]
        private LineRenderer leftConnector;
        [SerializeField]
        private LineRenderer rightConnector;

        private float separation = 0;
        private int val = 0;

        #region Operator Overloads
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

        public void Setup(Node _parent, NodeType _type)
        {
            Type = _type;
            transform.parent = _parent != null ? _parent.transform : transform.parent;
            separation = _parent != null ? _parent.separation * 0.5f : 10;
            transform.localPosition = (transform.right * CalculateSeparation()) - CalculateVerticalOffset();
        }

        private float CalculateSeparation()
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

        private Vector3 CalculateVerticalOffset()
        {
            switch (Type)
            {
                case NodeType.Right:
                case NodeType.Left:
                    return transform.up * 2;
            }

            return Vector3.zero;
        }
    }
}