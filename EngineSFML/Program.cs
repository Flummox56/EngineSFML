using System;
using SFML.Window;

namespace EngineSFML
{
    class Program
    {
        public static Engine eng = new Engine(0, 7000, 1000); // инициализация классов
        public static Picture pic = new Picture(200);

        public static Thread EngineThread = new Thread(eng.updateEngine) {IsBackground = true}; //инициализация фоновых потоков
        public static Thread PictureThread = new Thread(pic.updatePicture) {IsBackground = true};

        static void Main(string[] args)
        {
            pic.makeSprites();

            EngineThread.Start(); // выброс задач в фоновые потоки
            PictureThread.Start();

            //EngineThread.Join(-1); // затычка фоновых потоков

            pic.frameChange(); // независимая рисовалка

            //while (Console.ReadKey().Key != ConsoleKey.Escape) { }; // затычка главного потока
        }
    }
}