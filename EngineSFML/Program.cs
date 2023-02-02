using System;
using SFML.Window;

namespace EngineSFML
{
    class Program
    {
        static internal Engine eng = new Engine(0, 6000, 1000); // инициализация классов
        static internal Picture pic = new Picture(200);
        static void Main(string[] args)
        {
            Thread EngineThread = new Thread(eng.updateEngine) {IsBackground = true}; // инициализация фоновых потоков
            Thread PictureThread = new Thread(pic.updatePicture) { IsBackground = true};

            EngineThread.Start(); // выброс задач в фоновые потоки
            PictureThread.Start();

            //EngineThread.Join(-1); // затычка фоновых потоков

            pic.frameChange();

            //while (Console.ReadKey().Key != ConsoleKey.Escape) { }; // затычка главного потока
        }
    }
}