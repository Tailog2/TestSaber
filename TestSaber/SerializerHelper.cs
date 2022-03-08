using System.Text;

namespace TestSaber
{
    partial class ListRandom
    {
        private class SerializerHelper: ListRandom
        {
            private List<string> _nodes;
            private Dictionary<ListNode, int> _nodesCache;
            private ListNode _head;

            public List<string> GetListOfNodes(ListNode head)
            {
                _head = head;
                _nodesCache = new Dictionary<ListNode, int>();
                _nodes = new List<string>();
                
                AddNodeToList(_head);

                return _nodes;
            }

            private int AddNodeToList(ListNode node)
            {
                var currentNodeIndex = _nodesCache.Count();
                _nodesCache.Add(node, currentNodeIndex);

                var stringBuilder = new StringBuilder();
                stringBuilder.Append($"{currentNodeIndex}]:");
                stringBuilder.Append($"[{node.Data}]");


                if (node.Previous != null)
                    stringBuilder.Append($"[{GetNodeIndex(node.Previous)}]");
                else
                    stringBuilder.Append("[#]");

                if (node.Next != null)
                    stringBuilder.Append($"[{GetNodeIndex(node.Next)}]");
                else
                    stringBuilder.Append("[#]");

                if (node.Random != null)
                    stringBuilder.Append($"[{GetNodeIndex(node.Random)}");
                else
                    stringBuilder.Append("[#");

                _nodes.Add(stringBuilder.ToString());

                return currentNodeIndex;
            }

            private int GetNodeIndex(ListNode node)
            {
                return _nodesCache.ContainsKey(node) ? 
                    _nodesCache.GetValueOrDefault(node) : AddNodeToList(node);
            }
        }
    }
}   
