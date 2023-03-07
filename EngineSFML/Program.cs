using System;
using SFML.Window;

namespace EngineSFML
{
    class Program
    {
        public static Engine eng = new Engine(0, 7000, 1000);
        public static Picture pic = new Picture(1000);

        public static Thread EngineThread = new Thread(eng.updateEngine) {IsBackground = true};
        public static Thread PictureThread = new Thread(pic.updatePicture) {IsBackground = true};

        static void Main(string[] args)
        {
            EngineThread.Start();
            PictureThread.Start();

            pic.frameChange(); 
        }
    }
}