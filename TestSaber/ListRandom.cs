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
                var newNode = new ListNode() { Previous = Tail, Data = data};
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
             
            var serialazerHeandler = new SerializerHelper();
            s = serialazerHeandler.FillStream(Head, s);

            Stream = new MemoryStream();
            s.CopyTo(Stream);      
            Stream.Seek(0, SeekOrigin.Begin);
        }

        public void Deserialize(Stream s)
        {
            if (s == null)
                throw new NullReferenceException("Please specify the input stream");

            var deserializerHandler = new DeserializerHelper(new SerializerHelper());

            Head = deserializerHandler.GetHead(s);
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
