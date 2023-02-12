using System;
using SFML.Window;

namespace EngineSFML
{
    class Program
    {
        static internal Engine eng = new Engine(0, 8000, 1000); // инициализация классов
        static internal Picture pic = new Picture(1000);
        static void Main(string[] args)
        {
            pic.makeSprites();

            Thread EngineThread = new Thread(eng.updateEngine) {IsBackground = true}; // инициализация фоновых потоков
            Thread PictureThread = new Thread(pic.updatePicture) {IsBackground = true};
            Thread SpriteThread = new Thread(pic.updatePiston) {IsBackground = true};

            EngineThread.Start(); // выброс задач в фоновые потоки
            PictureThread.Start();
            SpriteThread.Start();

            //EngineThread.Join(-1); // затычка фоновых потоков

            pic.frameChange(); // независимая рисовалка

            //while (Console.ReadKey().Key != ConsoleKey.Escape) { }; // затычка главного потока
        }
    }
}