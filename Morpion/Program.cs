using System;
using SFML.Audio;
using SFML.Graphics;
using SFML.Window;
using SFML.System;
using Discord.API;
using Discord.Commands;
using Discord.Interactions;

namespace Morpion
{
    internal class Program
    {
        public static uint dimension = 3;
        public static uint DIM_BOUTON = 200;
        public static uint MARGE = 10;
        public static uint FPS = 60;
        public static uint LARG_FENETRE = dimension * DIM_BOUTON + (dimension +
        1) * MARGE;
        public static bool aJouer = false;
        public static bool partieFinie = false;
        public static RenderWindow window;
        public static RectangleShape[,] grilleSFML;
        public static int[,] grille;
        public static Texture tCroix;
        public static Texture tRond;
        public static Texture chadTexture;
        public static Texture doigTexture;
        public static bool isPlayerOneTexture = true;
        public static int joueurEnCours = 1;

        static void Main(string[] args)
        {
            tRond = new Texture("img\\rond.jpg");
            tCroix = new Texture("img\\croix.jpg");

            chadTexture = new Texture("img\\chad.png");
            doigTexture = new Texture("img\\doig.png");

            grille = new int[dimension, dimension];

            Program.InitialiseGrilleSFML();

            //Initialisation fenetre
            window = new RenderWindow(new VideoMode(LARG_FENETRE, LARG_FENETRE), "MORPION DRIP & CHAD");
            window.Clear(Color.White);
            window.SetFramerateLimit(FPS);
            window.Closed += OnClose;
            window.MouseButtonPressed += OnMouseButtonPressed;

            // 1er dessin ici
            Program.AfficheGrilleSFML();
            Program.AfficheGrilleEnConsole();

            while (window.IsOpen)
            {
                // Process events
                window.DispatchEvents();

                // on redessine que si besoin
                if (aJouer)
                {
                    Program.AfficheGrilleSFML();
                    Program.AfficheGrilleEnConsole();
                    aJouer = false;
                }
            }

        }
        static void OnClose(object sender, EventArgs e)
        {
            window.Close();
        }
        static void OnMouseButtonPressed(object sender, MouseButtonEventArgs e)
        {
            int mousePositionX = e.X;
            int mousePositionY = e.Y;

            long quelCaseX = e.Y / (DIM_BOUTON + MARGE);
            long quelCaseY = e.X / (DIM_BOUTON + MARGE);

            Console.WriteLine(quelCaseX + ", " + quelCaseY);


            grilleSFML[quelCaseX, quelCaseY].FillColor = Color.White;

            if (isPlayerOneTexture && grille[quelCaseX, quelCaseY] != 2 && grille[quelCaseX, quelCaseY] != 1)
            {
                grilleSFML[quelCaseX, quelCaseY].Texture = chadTexture;
                isPlayerOneTexture = false;
                grille[quelCaseX, quelCaseY] = 1;
            }
            else if(!isPlayerOneTexture && grille[quelCaseX, quelCaseY] != 2 && grille[quelCaseX, quelCaseY] != 1)
            {
                grilleSFML[quelCaseX, quelCaseY].Texture = doigTexture;
                isPlayerOneTexture = true;
                grille[quelCaseX, quelCaseY] = 2;
            }



            AfficheGrilleSFML();
            AfficheGrilleEnConsole();
        }

        static void InitialiseGrilleSFML()
        {
            grilleSFML = new RectangleShape[dimension, dimension];

            for (int i = 0; i < dimension; i++)
            {
                for (int j = 0; j < dimension; j++)
                {
                    grilleSFML[i, j] = new RectangleShape(new Vector2f(DIM_BOUTON, DIM_BOUTON));

                    grilleSFML[i, j].Position = new Vector2f(MARGE + j*(DIM_BOUTON+MARGE), MARGE + i * (DIM_BOUTON + MARGE));
                    grilleSFML[i, j].FillColor = Color.Black;

                }
            }
        }

        static void AfficheGrilleSFML()
        {
            window.Clear(Color.White);

            for (int i = 0; i < dimension; i++)
            {
                for (int j = 0; j < dimension; j++)
                {
                    window.Draw(grilleSFML[i,j]);
                }
            }
            window.Display();

        }

        static void AfficheGrilleEnConsole()
        {
            Console.WriteLine("---------");

            for (int i = 0; i < dimension; i++)
            {
                for (int j = 0; j < dimension; j++)
                {
                    Console.Write(grille[i, j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("---------");
        }
    }
}
