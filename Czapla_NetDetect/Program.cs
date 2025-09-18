using System;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

class NetDetect
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("NetDetect - Sprawdzanie dostępnych komputerów w sieci");
        Console.Write("Podaj podsieć do przeskanowania (przykładowo: 192.168.1.): ");
        string subnet = Console.ReadLine();
        int startRange = 1;
        int endRange = 255;

        for (int i = startRange; i <= endRange; i++)
        {
            string ip = subnet + i;
            if (await PingHost(ip))
            {
                Console.WriteLine($"Host aktywny: {ip}");
            }
        }
        Console.WriteLine("Skanowanie zakończone.");
    }

    static async Task<bool> PingHost(string host, int timeout = 100)
    {
        using (Ping pinger = new Ping())
        {
            try
            {
                PingReply reply = await pinger.SendPingAsync(host, timeout);
                return reply.Status == IPStatus.Success;
            }
            catch (PingException)
            {
                return false;
            }
        }
    }
}