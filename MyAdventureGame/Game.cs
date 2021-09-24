using System;
using System.Collections.Generic;
using System.Text;

namespace MyAdventureGame
{
    class Game
    {
        private bool _gameOver;
        private int _currentScene;
        private Player _player;
        private Entity[] _enemies;
        private int _currentEnemyIndex;
        private Entity _currentEnemy;
        private string _playerName;

        bool _gameOver = false;
        public void Run()
        {
            Start();

            while (!_gameOver)
            {
                Update();
            }

            End();
        }


        void Start()
        {

        }

        void Update()
        {
            DisplayCurrentScene();
        }
        void End()
        {

        }
        void DisplayCurrentScene()
        {
            switch (_currentScene)
            {

            }
        }

        /// <summary>
        /// Displays text asking for the players name. Doesn't transition to the next section
        /// until the player decides to keep the name.
        /// </summary>
        void GetPlayerName()
        {
            // Introduction and get player name
            Console.WriteLine("Welcome! Please enter your name.");
            Console.Write("> ");
            _playerName = Console.ReadLine();

            Console.Clear();

            int input = GetInput("You've entered " + _playerName + " are you sure you want to keep this name?",
                "Keep Name", "Rename");

            if (input == 0)
            {
                _currentScene++;
            }
        }

        /// <summary>
        /// Gets an input from the player based on some given decision
        /// </summary>
        /// <param name="description">The context for the input</param>
        /// <param name="options">The option the player can choose</param>
        /// <returns></returns>
        int GetInput(string description, params string[] options)
        {
            string input = "";
            int inputReceived = -1;

            while (inputReceived == -1)
            {
                // Print options
                Console.WriteLine(description);
                for (int i = 0; i < options.Length; i++)
                {
                    Console.WriteLine((i + 1) + ". " + options[i]);
                }

                Console.WriteLine("> ");

                // Get input from player
                input = Console.ReadLine();

                // If the player typed an int...
                if (int.TryParse(input, out inputReceived))
                {
                    // ...decrement the input and check if it's within the bounds of the array
                    inputReceived--;
                    if (inputReceived < 0 || inputReceived >= options.Length)
                    {
                        // Set input received to be default value
                        inputReceived = -1;

                        // Display error message
                        Console.WriteLine("Invalid Input");
                        Console.ReadKey(true);
                    }
                }

                // If the player didn't type an int
                else
                {
                    // Set input received to be the default value
                    inputReceived = -1;
                    Console.WriteLine("Invalid Input");
                    Console.ReadKey(true);
                }
                Console.Clear();
            }
            return inputReceived;
        }
    }
}
