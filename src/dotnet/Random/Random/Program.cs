
var rand = new Random(2);
while (true)
{
    Console.Write($"{rand.Next(1,10)} {rand.Next(30, 100)}");
    Console.ReadLine();
}