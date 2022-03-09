using System.Text;

namespace TestSaber
{
    partial class ListRandom
    {
        private class SerializerHelper : ListRandom
        {
            private List<string> _nodes;
            private Dictionary<ListNode, int> _nodesCache;
            private ListNode _head;
            public string Divider { get; private set; }

            public SerializerHelper()
            {
                Divider = "<==>";
            }

            public Stream FillStream(ListNode head, Stream s)
            {
                var streamWriter = new StreamWriter(s);
                var nodes = GetListOfNodes(head);

                foreach (var node in nodes)
                    streamWriter.WriteLine(node ?? string.Empty);

                streamWriter.Flush();
                s.Seek(0, SeekOrigin.Begin);
                
                return s;
            }

            private List<string> GetListOfNodes(ListNode head)
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

                stringBuilder.Append($"{currentNodeIndex}{Divider}");
                stringBuilder.Append(AddLink(node.Previous));
                stringBuilder.Append(AddLink(node.Next));
                stringBuilder.Append(AddLink(node.Random));
                stringBuilder.Append($"{node.Data}");

                _nodes.Add(stringBuilder.ToString());

                return currentNodeIndex;
            }

            private string AddLink(ListNode node)
            {
                return node != null ?
                    $"{GetNodeIndex(node)}{Divider}" : $"#{Divider}";
            }

            private int GetNodeIndex(ListNode node)
            {
                return _nodesCache.ContainsKey(node) ? 
                    _nodesCache.GetValueOrDefault(node) : AddNodeToList(node);
            }
        }
    }
}   
