namespace Snake_v1._0_Facit
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int xPosition = 35; // spelarea = 70 bred
            int yPosition = 20; // spelarea = 40 hög
            int gameSpeed = 150;

            bool isGameOn = true;
            bool isWallHit = false;

            // Visa snake på skrämen
            Console.SetCursorPosition(xPosition, yPosition);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("ö");

            // Rita border
            BuildWall();

            // Flytta på snake
            ConsoleKey command = Console.ReadKey().Key;

            do
            {
                switch (command)
                {
                    case ConsoleKey.LeftArrow:
                        Console.SetCursorPosition(xPosition, yPosition);
                        Console.Write(" ");
                        xPosition--;
                        break;
                    case ConsoleKey.UpArrow:
                        Console.SetCursorPosition(xPosition, yPosition);
                        Console.Write(" ");
                        yPosition--;
                        break;
                    case ConsoleKey.RightArrow:
                        Console.SetCursorPosition(xPosition, yPosition);
                        Console.Write(" ");
                        xPosition++;
                        break;
                    case ConsoleKey.DownArrow:
                        Console.SetCursorPosition(xPosition, yPosition);
                        Console.Write(" ");
                        yPosition++;
                        break;
                }

                // Visa snake på skrämen
                Console.SetCursorPosition(xPosition, yPosition);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("ö");

                // Känner av när snake träffa väggen
                // Slow game down
                isWallHit = DidSnakeHitWall(xPosition, yPosition);

                if (isWallHit)
                {
                    isGameOn = false;
                    Console.SetCursorPosition(32, 20);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Game Over");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.SetCursorPosition(1, 42);
                }

                if (Console.KeyAvailable) command = Console.ReadKey().Key;
                System.Threading.Thread.Sleep(gameSpeed);    
            } while (isGameOn);




            // Placera äpple random ställe på skärmen

            // Känner av när äpplet har ätits
            // Gör snake snabbare
            // Gör snake längre
            // Lägg antalet uppätna äpplen i en variabel (score)

            // Gör en välkomstskärm (meny)

            // Låt spelaren läser instruktionerna om han vill

            // Visa final score

            // Låt spelaren välja att spela igen
        }

        private static bool DidSnakeHitWall(int xPosition, int yPosition)
        {
            if (xPosition == 1 || xPosition == 70 || yPosition == 1 || yPosition == 40)
            {
                return true;
            }
            return false;
        }

        private static void BuildWall()
        {
            // OBS: Måste se till att ditt console fönster är rätt storlek
            // Min är satt till 120 * 60
            // Högerklick på console fönstret -> settings
            // Under Startup -> Launch Size
            for (int i = 1; i <= 40; i++)
            {
                // Bygg lodrätta väggar
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(1, i);
                Console.Write("#");
                Console.SetCursorPosition(70, i);
                Console.Write("#");
            }
            // Bygg vågrätta väggar
            for (int i = 1; i <= 70; i++)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(i, 1);
                Console.Write("#");
                Console.SetCursorPosition(i, 40);
                Console.Write("#");
            }
        }
    }
}