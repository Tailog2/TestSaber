// See https://aka.ms/new-console-template for more information
using System.Text;
using TestSaber;
using static TestSaber.ListRandom;
Console.WriteLine("Hello, World!");

// Для увеличения читаемости кода я добавил несколько публичных методов в класс ListRandom
// Add(string data) - метод для добавления нода в коллекцию
// Add(string data, ListNode random) - и перегруженный метод для добавления нода и случайного нода Random

// Большая часть логики была вынесена в отдельные классы SerializerHelper и DeserializerHelper

// Работа метода Serialize() основана на создание текстового списка всех нода с присваиванием им порядкового номера
// Все ссылки на последующие ноды в списке замещаться их порядковыми номерами
// В результате полученная строка выглядит следующим образом  0]:[Some data][1][0][#

// Работа метода Deserialize() на разделении строк полученных в результате работа метода Serialize()
// Всем цифрам в строках прописываются ссылки на соответствующие ноды

var listRandom = new ListRandom();

listRandom.Add("Cat");
listRandom.Add("Dog", listRandom.Head);
listRandom.Add("Fish", new ListNode() { Data = "Bug" });
listRandom.Add("Mamont");

using (var memoryStream = new MemoryStream())
{
    listRandom.Serialize(memoryStream);
    listRandom.Deserialize(listRandom.Stream);
}



