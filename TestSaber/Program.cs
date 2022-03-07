// See https://aka.ms/new-console-template for more information
using System.Text;
using TestSaber;
using static TestSaber.ListRandom;

Console.WriteLine("Hello, World!");

var listRandom = new ListRandom();

var head = new ListNode() {  };
var nodeOne  = new ListNode() { Data = "Dog" };
var nodeTwo = new ListNode() { Data = "Fish" };
var tail = new ListNode() { Data = "Mamont" };
var randomNode = new ListNode() { Data = "Bug" };

listRandom.Add("Cat");
listRandom.Add("Dog", listRandom.Head);
listRandom.Add("Fish", new ListNode() { Data = "Bug" });
listRandom.Add("Mamont");

var memoryStream = new MemoryStream();
listRandom.Serialize(memoryStream);

listRandom.Deserialize(listRandom.Stream);


