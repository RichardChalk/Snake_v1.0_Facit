namespace Snake_v1._0_Facit
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int xPosition = 35; // spelarea = 70 bred
            int yPosition = 20; // spelarea = 40 hög

            // Visa snake på skrämen
            Console.SetCursorPosition(xPosition, yPosition);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("ö");

            // Rita border
            BuildWall();

            // Flytta på snake

            // Känner av när snake träffa väggen

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

        private static void BuildWall()
        {
            // OBS: Måste se till att ditt console förnster är rätt storlek
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