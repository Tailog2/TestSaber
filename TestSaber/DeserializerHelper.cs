namespace TestSaber
{
    partial class ListRandom
    {
        private class DeserializerHelper : ListRandom
        {
            private Dictionary<string, ListNode> _nodesCache;
            private Dictionary<string, string> _nodes;

            public void MapNodes(List<string> nodesList)
            {
                _nodes = NodesListToDictionary(nodesList);
                _nodesCache = new Dictionary<string, ListNode>();

                Head = FetchNextNodes("0");                       
                Tail = GetTail();
                Count = GetCount();
            }
         
            private Dictionary<string, string> NodesListToDictionary(List<string> nodesList)
            {
                Dictionary<string, string> nodes = new Dictionary<string, string>();

                foreach (var nodeString in nodesList)
                {
                    var slittedString = nodeString.Split("]:[");
                    nodes.Add(slittedString[0], slittedString[1]);     
                }

                return nodes;
            }

            private ListNode FetchNextNodes(string nodeIndex)
            {
                string[] slittedString = _nodes.GetValueOrDefault(nodeIndex).Split("][");

                ListNode newNode = new ListNode();
                newNode.Data = slittedString[0];

                if (_nodesCache.ContainsKey(nodeIndex) is false)
                    _nodesCache.Add(nodeIndex, newNode);             

                if (slittedString[1] != "#")
                    newNode.Previous = GetNode(slittedString, 1);
                if (slittedString[2] != "#")
                    newNode.Next = GetNode(slittedString, 2);
                if (slittedString[3] != "#")
                    newNode.Random = GetNode(slittedString, 3);      

                return newNode;
            }

            private ListNode? GetNode(string[] slittedString, byte position)
            {
                return _nodesCache.ContainsKey(slittedString[position]) ?
                    _nodesCache.GetValueOrDefault(slittedString[position]) : FetchNextNodes(slittedString[position]);
            }

            private ListNode GetTail()
            {
                var currentNode = Head;
                while (currentNode != null)
                {
                    currentNode = currentNode.Next;
                }
                return currentNode;
            }

            private int GetCount()
            {
                var currentNode = Head;
                var count = 1;
                while (currentNode != null)
                {
                    currentNode = currentNode.Next;
                    count++;
                }
                return count;
            }
        }
    }
}   
