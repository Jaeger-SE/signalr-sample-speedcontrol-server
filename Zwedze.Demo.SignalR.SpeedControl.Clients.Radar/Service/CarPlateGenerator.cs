using System;

namespace Zwedze.Demo.SignalR.SpeedControl.Clients.Radar.Service
{
    public class CarPlateGenerator
    {
        private readonly Random _random = new(0x07031988);

        public string Generate()
        {
            return
                $"{GetNumber()}-{GetLetter()}{GetLetter()}{GetLetter()}-{GetNumber()}{GetNumber()}{GetNumber()}";

            int GetNumber()
            {
                return _random.Next(10);
            }

            char GetLetter()
            {
                return (char) _random.Next('A', 'Z');
            }
        }
    }
}