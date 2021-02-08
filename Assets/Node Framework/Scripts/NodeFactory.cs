using UnityEngine;

using System.Collections.Generic;

namespace NodeFramework
{
    public class NodeFactory : MonoBehaviour
    {
        private static NodeFactory factory = null;

        private static Node nodePrefab;

        public static void Setup(GameObject _gameObject)
        {
            if(factory != null)
                return;

            factory = _gameObject.GetComponent<NodeFactory>();
            if(factory == null)
                factory = _gameObject.AddComponent<NodeFactory>();

            nodePrefab = Resources.Load<Node>("Node Prefab");
        }

        public static Node Create(int _number)
        {
            Node node = Object.Instantiate(nodePrefab, factory.transform);
            node.gameObject.name = string.Format("Node [{0}]", _number.ToString());
            node.Value = _number;

            return node;
        }
    }
}