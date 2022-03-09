using System.Text.RegularExpressions;

namespace TestSaber
{
    partial class ListRandom
    {
        private class DeserializerHelper : ListRandom
        {
            private Dictionary<string, ListNode> _nodesCache;
            private Dictionary<string, string> _nodesAll;
            private string _divider;

            public DeserializerHelper(SerializerHelper serializerHelper)
            {
                _divider = serializerHelper.Divider;
            }

            public ListNode GetHead(Stream s)
            {
                var stringNodes = new List<string>();
                using var streamReader = new StreamReader(s);

                while (!streamReader.EndOfStream)
                {
                    stringNodes.Add(streamReader.ReadLine() ?? "");
                }

                if (stringNodes.Count == 0)
                    throw new Exception("The input stream is empty");

                MapNodes(stringNodes);

                return Head;
            }

            private void MapNodes(List<string> nodesList)
            {
                _nodesAll = NodesListToDictionary(nodesList);
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
                    var slittedString = CutString(nodeString);
                    nodes.Add(slittedString[0], slittedString[1]);
                }

                return nodes;
            }

            private string[] CutString(string nodeString)
            {
                var outputString = new string[2];

                var dividerIndex = nodeString.IndexOf(_divider);
                outputString[0] = nodeString.Substring(0, dividerIndex);
                outputString[1] = nodeString.Substring(dividerIndex + _divider.Length);

                return outputString;
            }

            private ListNode FetchNextNodes(string nodeIndex)
            {
                var newNode = new ListNode();

                var linksString = _nodesAll.GetValueOrDefault(nodeIndex);

                var linksArray = SliteLinksString(linksString);             

                if (!_nodesCache.ContainsKey(nodeIndex))
                    _nodesCache.Add(nodeIndex, newNode);

                newNode.Previous = GetLink(linksArray, 0);
                newNode.Next = GetLink(linksArray, 1);
                newNode.Random = GetLink(linksArray, 2);

                newNode.Data = linksArray[3];

                return newNode;
            }

            private string[] SliteLinksString(string linksString)
            {
                var output = new string[4];
                var dataIndex = output.Length - 1;
                var dividedString = CutString(linksString);
                output[0] = dividedString[0];

                for (int i = 1; i < dataIndex; i++)
                {
                    dividedString = CutString(dividedString[1]);
                    output[i] = dividedString[0];
                }

                output[dataIndex] = dividedString[1];

                return output;
            }

            private ListNode? GetLink(string[] linksArray, byte position)
            {
                if (linksArray[position] != "#")
                    return GetNode(linksArray, position);
                else
                    return null;
            }

            private ListNode? GetNode(string[] linksArray, byte position)
            {
                return _nodesCache.ContainsKey(linksArray[position]) ?
                    _nodesCache.GetValueOrDefault(linksArray[position]) : FetchNextNodes(linksArray[position]);
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
