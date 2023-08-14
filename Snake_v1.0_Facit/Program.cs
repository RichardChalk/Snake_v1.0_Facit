namespace Snake_v1._0_Facit
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // spelarea = 70 bred
            // spelarea = 40 hög

            // start position av snake är mitt i spelarenan! 70/2 & 40/2
            int[] xPosition = new int[50];
            xPosition[0] = 35;
            int[] yPosition = new int[50];
            yPosition[0] = 20;
                        
            int xPositionApple = 10;
            int yPositionApple = 10;
            int applesEaten = 0;
            Random random = new Random();

            decimal gameSpeed = 150m;

            bool isGameOn = true;
            bool isWallHit = false;
            bool isAppleEaten = false;

            // Visa snake på skrämen
            Console.SetCursorPosition(xPosition[0], yPosition[0]);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("ö");

            // Placera FÖRSTA äpplet random ställe på skärmen
            // out - ändrar de globala variabler också (inte bara de lokala inom metoden)
            SetApplePositionOnScreen(random, out xPositionApple, out yPositionApple);
            PaintApple(xPositionApple, yPositionApple);

            // Rita border
            BuildWall();

            // Flytta på snake
            ConsoleKey command = Console.ReadKey().Key;

            do
            {
                switch (command)
                {
                    case ConsoleKey.LeftArrow:
                        Console.SetCursorPosition(xPosition[0], yPosition[0]);
                        Console.Write(" ");
                        xPosition[0]--;
                        break;
                    case ConsoleKey.UpArrow:
                        Console.SetCursorPosition(xPosition[0], yPosition[0]);
                        Console.Write(" ");
                        yPosition[0]--;
                        break;
                    case ConsoleKey.RightArrow:
                        Console.SetCursorPosition(xPosition[0], yPosition[0]);
                        Console.Write(" ");
                        xPosition[0]++;
                        break;
                    case ConsoleKey.DownArrow:
                        Console.SetCursorPosition(xPosition[0], yPosition[0]);
                        Console.Write(" ");
                        yPosition[0]++;
                        break;
                }

                // Visa snake på skrämen
                Console.SetCursorPosition(xPosition, yPosition);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("ö");

                // Känner av när snake (head) träffa väggen
                isWallHit = DidSnakeHitWall(xPosition[0], yPosition[0]);

                if (isWallHit)
                {
                    isGameOn = false;
                    Console.SetCursorPosition(32, 20);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Game Over");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.SetCursorPosition(1, 42);
                }

                // Känner av om äpplet har ätits upp av snake
                isAppleEaten = DetermineIfAppleIsEaten(xPosition[0], yPosition[0], xPositionApple, yPositionApple);
                
                if (isAppleEaten)
                {
                    // Placera äpple random ställe på skärmen
                    // out - ändrar de globala variabler också (inte bara de lokala inom metoden)
                    SetApplePositionOnScreen(random, out xPositionApple, out yPositionApple);
                    PaintApple(xPositionApple, yPositionApple);

                    // Lägg antalet uppätna äpplen i en variabel (score)
                    applesEaten++;
                    // Gör snake snabbare
                    gameSpeed *= .925m;
                }

                if (Console.KeyAvailable) command = Console.ReadKey().Key;

                // Slow game down
                System.Threading.Thread.Sleep(Convert.ToInt32(gameSpeed));
            } while (isGameOn);






            
            
            // Gör snake längre
            

            // Gör en välkomstskärm (meny)

            // Låt spelaren läser instruktionerna om han vill

            // Visa final score

            // Låt spelaren välja att spela igen
        }

        private static bool DetermineIfAppleIsEaten(int xPosition, int yPosition, int xPositionApple, int yPositionApple)
        {
            // Om snake huvud är på samma position som ett äpple... ät det!
            if (xPosition == xPositionApple && yPosition == yPositionApple) return true; return false;
        }

        private static void PaintApple(int xPositionApple, int yPositionApple)
        {
            Console.SetCursorPosition(xPositionApple, yPositionApple);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("@");
        }

        private static void SetApplePositionOnScreen(Random random, out int xPositionApple, out int yPositionApple)
        {
            // +2 -2 används för att äpplet ska skapas INOM spelarenan!
            xPositionApple = random.Next(0 + 2, 70 - 2);
            yPositionApple = random.Next(0 + 2, 40 - 2);
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