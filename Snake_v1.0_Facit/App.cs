﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake_v1._0_Facit
{
    internal class App
    {
        internal void GoGame()
        {
            // Option #1
            // OBS: Måste se till att ditt console fönster är rätt storlek
            // Min är satt till 120 * 50
            // 1. Högerklick på console fönstret -> Settings
            // 2. Under Startup -> Launch Size

            // Option #2
            // ELLER... Man kan ändra storleken på konsolfönstret med C# kod (se nedan)!
            // Sedan Windows 11 måste man också se till att välja "Windows Console Host"
            // 1. Högerklick på console fönstret -> Settings
            // 2. Under Startup -> Default terminal application -> Windows Console Host
            //Console.SetBufferSize(120, 50);
            //Console.SetWindowSize(120, 50);

            // Att lägga till regions kan göra koden lättare att läsa
            // Nu får jag möjlighet att "kollapsa" denna region om jag vill
            #region Variables
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

            // Ta bort cursor... endast estetiskt
            Console.CursorVisible = false;

            string userAction = " ";

            decimal gameSpeed = 150m;

            bool isGameOn = true;
            bool isWallHit = false;
            bool isAppleEaten = false;
            bool isStayInMenu = true;
            #endregion

            #region Call Menu
            do
            {
                // Gör en välkomstskärm (meny)
                // Låt spelaren läser instruktionerna om han vill
                ShowMenu(out userAction);

                // Om spelare väljer att spela igen...
                // Reset snake
                xPosition = new int[50];
                xPosition[0] = 35;
                yPosition = new int[50];
                yPosition[0] = 20;
                // Reset även denna
                isGameOn = true;

                switch (userAction)
                {
                    case "1":
                        ShowInstructions(userAction);
                        break;
                    case "2":
                        Console.Clear();
                        #region Game Setup
                        // Visa new snake array på skrämen
                        PaintSnake(applesEaten, xPosition, yPosition, out xPosition, out yPosition);

                        // Placera FÖRSTA äpplet random ställe på skärmen
                        // out - ändrar de globala variabler också (inte bara de lokala inom metoden)
                        SetApplePositionOnScreen(random, out xPositionApple, out yPositionApple);
                        PaintApple(xPositionApple, yPositionApple);

                        // Rita border
                        BuildWall();

                        // Läs instruktion från användaren
                        ConsoleKey command = Console.ReadKey().Key;
                        #endregion

                        #region Game Loop
                        // Flytta på snake
                        do
                        {
                            #region Change Directions
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
                            #endregion

                            #region Playing Game
                            // Visa new snake array på skrämen
                            PaintSnake(applesEaten, xPosition, yPosition, out xPosition, out yPosition);

                            // Känner av när snake (head) träffa väggen
                            isWallHit = DidSnakeHitWall(xPosition[0], yPosition[0]);

                            if (isWallHit)
                            {
                                isGameOn = false;
                                Console.SetCursorPosition(32, 20);
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Game Over");

                                // Visa final score
                                Console.SetCursorPosition(30, 22);
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.Write(@"Din score är ");
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write(applesEaten);
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.SetCursorPosition(20, 24);
                                Console.WriteLine("Tryck på valfri knapp för att fortsätta");
                                applesEaten = 0;

                                // Raderar äpplet vid game over
                                Console.SetCursorPosition(xPositionApple, yPositionApple);
                                Console.Write(" ");

                                Console.ReadLine();
                                Console.Clear();

                                // Låt spelaren välja att spela igen
                                // ShowMenu(out userAction);

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

                            #endregion
                        } while (isGameOn);
                        #endregion
                        break;
                    case "3":
                        isStayInMenu = false;
                        break;
                    default:
                        Console.WriteLine("Inte ett giltigt val. Försök igen");
                        Console.ReadLine();
                        Console.Clear();
                        // ShowMenu(out userAction);
                        break;
                }
            } while (isStayInMenu);
            #endregion
        }
        #region Menu
        private static void ShowMenu(out string userAction)
        {
            Console.Clear();
            Console.WriteLine("1: How to play");
            Console.WriteLine("2: Play");
            Console.WriteLine("3: Exit");

            userAction = Console.ReadLine().ToLower();
        }

        private static void ShowInstructions(string userAction)
        {
            Console.Clear();
            BuildWall();
            Console.SetCursorPosition(12, 5);
            Console.WriteLine("Använd pilarna för att flytta ormen runt banan");
            Console.SetCursorPosition(16, 7);
            Console.WriteLine("Ormen dör om den träffar yttreväggen");
            Console.SetCursorPosition(20, 9);
            Console.WriteLine("Ät äpplen för att få poäng!");
            Console.SetCursorPosition(13, 11);
            Console.WriteLine("Varje äpple gör ormen längre och snabbare!");
            Console.SetCursorPosition(14, 13);
            Console.WriteLine("Tryck på valfri knapp för att fortsätta");
            Console.ReadLine();
            Console.Clear();
            //ShowMenu(out userAction);
        }
        #endregion

        #region Methods
        private static void PaintSnake(int applesEaten, int[] xPositionIn, int[] yPositionIn, out int[] xPositionOut, out int[] yPositionOut)
        {
            // Rita ut snake huvud
            Console.SetCursorPosition(xPositionIn[0], yPositionIn[0]);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("ö");

            // Gör snake längre
            // Rita ut snake kropp
            for (int i = 1; i < applesEaten + 1; i++)
            {
                Console.SetCursorPosition(xPositionIn[i], yPositionIn[i]);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("o");
            }

            // Radera svansen av snake
            Console.SetCursorPosition(xPositionIn[applesEaten + 1], yPositionIn[applesEaten + 1]);
            Console.WriteLine(" ");

            // Notera varje del av snake
            for (int i = applesEaten + 1; i > 0; i--)
            {
                xPositionIn[i] = xPositionIn[i - 1];
                yPositionIn[i] = yPositionIn[i - 1];
            }

            // Returnera snake nya position
            xPositionOut = xPositionIn;
            yPositionOut = yPositionIn;
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
        #endregion
    }
}
