using System.Text;

namespace TestSaber
{
    partial class ListRandom
    {
        private class SerializerHelper: ListRandom
        {
            private List<string> _nodes;
            private List<ListNode> _nodesCache;
            private ListNode _head;

            public List<string> GetListOfNodes(ListNode head)
            {
                _head = head;
                _nodesCache = new List<ListNode>();
                _nodes = new List<string>();
                
                AddNodeToList(_head);

                return _nodes;
            }

            private int AddNodeToList(ListNode node)
            {
                var currentNodeIndex = _nodesCache.Count();
                _nodesCache.Add(node);

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
                int nodeIndex;

                if (!_nodesCache.Contains(node))
                    nodeIndex = AddNodeToList(node);
                else
                    nodeIndex = GetIndexOfAddedNode(node);            

                return nodeIndex;
            }

            private int GetIndexOfAddedNode(ListNode node)
            {
                return _nodesCache.IndexOf(node);
            }
        }
    }
}   
