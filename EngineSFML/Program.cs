using System;
using SFML.Window;

namespace EngineSFML
{
    class Program
    {
        static internal Engine e = new Engine(0, 6000, 1000); // инициализация классов
        static internal Picture p = new Picture(200);
        static void Main(string[] args)
        {
            Thread EngineThread = new Thread(e.updateEngine) {IsBackground = true}; // инициализация фоновых потоков
            Picture p = new Picture(200);

            EngineThread.Start(); // выброс задач в фоновые потоки

            //EngineThread.Join(-1); // затычка фоновых потоков

            p.updatePicture();

            //while (Console.ReadKey().Key != ConsoleKey.Escape) { }; // затычка главного потока
        }
    }
}