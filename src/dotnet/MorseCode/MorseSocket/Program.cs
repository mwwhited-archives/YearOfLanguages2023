

using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;

var listener = new TcpListener(IPAddress.Any, 23);
listener.Start();

while (true)
{
    Console.WriteLine("Waiting!");
    using var handler = listener.AcceptTcpClient();
    using var stream = handler.GetStream();
    while (true)
    {
        var input = stream.ReadByte();
        if (input == -1) break;

        var map = (char)(input > '_' ? input &  0b01011111: input) switch
        {
            'A' => ".-",
            'B' => "-...",
            'C' => "-.-.",
            'D' => "_..",
            'E' => ".",
            'F' => "..-.",
            'G' => "--.",
            'H' => "....",
            'I' => "..",
            'J' => ".---",
            'K' => "-.-",
            'L' => ".-..",
            'M' => "--",
            'N' => "-.",
            'O' => "---",
            'P' => ".--.",
            'Q' => "--.-",
            'R' => ".-.",
            'S' => "...",
            'T' => "-",
            'U' => "..-",
            'V' => "...-",
            'W' => ".--",
            'X' => "-..-",
            'Y' => "-.--",
            'Z' => "--..",
            '1' => ".----",
            '2' => "..---",
            '3' => "...--",
            '4' => "....-",
            '5' => ".....",
            '6' => "-....",
            '7' => "--...",
            '8' => "---..",
            '9' => "----.",
            '0' => "-----",
            '\n' => Environment.NewLine,
            _ => "",
        };
        var morse = map + " ";
        var buffer = Encoding.UTF8.GetBytes(morse);
        Console.Write(morse);
        stream.Write(buffer, 0, buffer.Length);
    }
}

listener.Start();
Console.ReadLine();


