using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestSaber
{
    partial class ListRandom
    {
        public ListNode Head;
        public ListNode Tail;
        public int Count;
        public Stream Stream;

        public void Add(string data)
        {
            if (Head == null)
            {
                Head = new ListNode() { Data = data, Next = Tail };
                Tail = Head;
            }
            else
            {
                var newNode = new ListNode() { Data = data };
                Tail.Next = newNode;
                Tail = newNode;
            }
            Count++;
        }  

        public void Add(string data, ListNode random)
        {
            if (Head == null)
            {
                Head = new ListNode() { Data = data, Next = Tail, Random = random };
                Tail = Head;
            }
            else
            {
                var newNode = new ListNode() { Previous = Tail, Data = data, Random = random };
                Tail.Next = newNode;
                Tail = newNode;
            }
        }

        public void Serialize(Stream s)
        {
            if (Count == 0)
                throw new Exception("The object of class ListRandom does not have any elements");

            Stream = new MemoryStream();
            var serialazerHeandler = new SerializerHelper();
            var nodes = serialazerHeandler.GetListOfNodes(Head);

            using var streamWriter = new StreamWriter(s);

            foreach (var node in nodes)
                streamWriter.WriteLine(node ?? string.Empty);

            s.Seek(0, SeekOrigin.Begin);       
            s.CopyTo(Stream);
            Stream.Seek(0, SeekOrigin.Begin);
        }

        public void Deserialize(Stream s)
        {
            if (s == null)
                throw new NullReferenceException("Please specify the input stream");

            var stringNodes = new List<string>();
            using var streamReader = new StreamReader(s);

            while (!streamReader.EndOfStream)
            {
                stringNodes.Add(streamReader.ReadLine() ?? "");
            }

            var deserializerHandler = new DeserializerHelper();
            deserializerHandler.MapNodes(stringNodes);

            Head = deserializerHandler.Head;
            Tail = deserializerHandler.Tail;
            Count = deserializerHandler.Count;
        }

        public class ListNode
        {
            public ListNode Previous;
            public ListNode Next;
            public ListNode Random; // произвольный элемент внутри списка
            public string Data;
        }
    }
}   
