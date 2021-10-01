using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace MyAdventureGame
{
    public enum Scene
    {
        START,
        NAME,
        SELECTION,
        STORY,
        BATTLE,
        RESTART
    }
    public struct Item
    {
        public string Name;
        public float StatBoost;
        public ItemType Type;
    }

    public enum ItemType
    {
        DEFENSE,
        ATTACK,
        NONE
    }

    class Game
    {
        private bool _gameOver;
        private Scene _currentScene;
        private Player _player;
        private Entity[] _enemies;
        private int _currentEnemyIndex;
        private Entity _currentEnemy;
        private string _playerName;

        // Items
        private Item[] _wizardItems;
        private Item[] _knightItems;


        /// <summary>
        /// Function that starts the main game loop
        /// </summary>
        public void Run()
        {
            Start();

            while (!_gameOver)
            {
                Update();
            }

            End();
        }

        /// <summary>
        /// Function used to initialize any starting values by default
        /// </summary>
        public void Start()
        {
            _gameOver = false;
            _currentScene = 0;

            InitializeEnemies();
            InitializeItems();
        }

        /// <summary>
        /// Initialize Player items from start. 
        /// </summary>
        public void InitializeItems()
        {
            // Wizard items
            Item fireCaster = new Item { Name = "Fire Caster", StatBoost = 14, Type = ItemType.ATTACK };
            Item forceShield = new Item { Name = "Force Shield", StatBoost = 15, Type = ItemType.DEFENSE };

            // Knight items
            Item masterSword = new Item { Name = "Master Sword", StatBoost = 18, Type = ItemType.ATTACK };
            Item shield = new Item { Name = "Shield", StatBoost = 16, Type = ItemType.DEFENSE };

            // Initialize arrays
            _wizardItems = new Item[] { fireCaster, forceShield };
            _knightItems = new Item[] { masterSword, shield };
        }

        /// <summary>
        /// Initialize Enemies from start. 
        /// </summary>
        public void InitializeEnemies()
        {
            _currentEnemyIndex = 0;

            Entity apathy = new Entity("Apathy", 13, 6, 10);

            Entity fear = new Entity("Fear", 16, 9, 10);

            Entity reget = new Entity("Regret", 18, 10, 10);

            Entity pain = new Entity("Pain", 20, 15, 10);

            Entity dispair = new Entity("Dispair", 25, 18, 14);

            _enemies = new Entity[] { apathy, fear, reget, pain, dispair };

            _currentEnemy = _enemies[_currentEnemyIndex];
        }

        /// <summary>
        /// This function is called every time the game loops.
        /// </summary>
        public void Update()
        {
            DisplayCurrentScene();
        }

        /// <summary>
        /// This function is called before the applications closes
        /// </summary>
        public void End()
        {
            Console.WriteLine("Goodbye, Adventurer.");
        }

        /// <summary>
        /// Calls the appropriate function(s) based on the current scene index
        /// </summary>
        public void DisplayCurrentScene()
        {
            switch (_currentScene)
            {
                case Scene.START:
                    DisplayStartMenu();
                    break;
                case Scene.NAME:
                    GetPlayerName();
                    break;
                case Scene.SELECTION:
                    CharacterSelection();
                    break;
                case Scene.STORY:
                    Story();
                    break;
                case Scene.BATTLE:
                    Battle();
                    CheckBattleResults();
                    break;
                case Scene.RESTART:
                    DisplayRestartMenu();
                    break;
            }
        }



        /// <summary>
        /// Displays the menu that allows the player to start the game 
        /// again or quit the game
        /// </summary>
        void DisplayRestartMenu()
        {
            int choice = GetInput("Play Again?", "Yes", "No");

            // Restart the player to the start
            if (choice == 0)
            {
                // Reset enemies values and set player the beginning of scene
                _currentScene = 0;
                InitializeEnemies();
            }

            // Application close
            else if (choice == 1)
            {
                // ...set game over to be true
                _gameOver = true;
            }
        }

        /// <summary>
        /// Show StartMenu to see what user on screen
        /// </summary>
        public void DisplayStartMenu()
        {
            // Introduction
            int choice = GetInput("Welcome to My Adventure Game! Warning: May include depression", 
                "Start New Game", "Load Game");

            if (choice == 0)
            {
                _currentScene++;
            }
            else if (choice == 1)
            {
                if (Load())
                {
                    Console.WriteLine("Load Successful!");
                    Console.ReadKey(true);
                    Console.Clear();
                    _currentScene = Scene.BATTLE;
                }
            }
            else
            {
                Console.WriteLine("Load Failed.");
                Console.ReadKey(true);
                Console.Clear();
            }
        }

        /// <summary>
        /// Displays text asking for the players name. Doesn't transition to the next section
        /// until the player decides to keep the name.
        /// </summary>
        void GetPlayerName()
        {
            // Get player name
            Console.WriteLine("Welcome! Please enter your name.");
            Console.Write("> ");
            _playerName = Console.ReadLine();

            Console.Clear();

            // If player want to decide change the name.
            int input = GetInput("You've entered " + _playerName + " are you sure you want to keep this name?",
                "Keep Name", "Rename");

            // Move along next scene
            if (input == 0)
            {
                _currentScene++;
            }
        }

        /// <summary>
        /// Gets the players choice of character. Updates player stats based on
        /// the character chosen.
        /// </summary>
        public void CharacterSelection()
        {
            int input = GetInput("Nice to meet you " + _playerName + ". Please select a character.",
                "Wizard", "Knight");

            Console.Write("> ");

            // Wizard
            if (input == 0)
            {
                // Display for Wizard Stats

                // Player Health = 60f;
                // Player AttackPower = 28f;
                // Player DefensePower = 5f;
                // Items: Fire Caster and Force Shield

                _player = new Player(_playerName, 60, 28, 5, _wizardItems, "Wizard");
                _currentScene++;
            }

            // Knight
            else if (input == 1)
            {
                // Display for Knight Stats

                // Player Health = 75f;
                // Player AttackPower = 25f;
                // Player DefensePower = 15f;
                // Items: Master Sword and Shield

                _player = new Player(_playerName, 75, 25, 15, _knightItems, "Knight");
                _currentScene++;
            }
        }

        /// <summary>
        /// Getting user to be engage into story. 
        /// </summary>
        void Story()
        {
            int input = GetInput("The world went dark, chaos reign over corrupt land, people has been slaughter.",
                "Surrender", "Fight");

            if (input == 0)
            {
                Console.WriteLine("You surrender into depression like there no hope, but enemies notice your presence you been slained." + "\n");
                _currentScene = Scene.RESTART;
            }
            else if (input == 1)
            {
                Console.WriteLine("Stand to your ground to fight take back what dear to you." + "\n" +
                    "I hope you know what to do. And now enemies face toward you. Prepare to fight! \n");
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

                Console.Write("> ");

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


        /// <summary>
        /// Create a new stream writer for player for save
        /// </summary>
        public void Save()
        {
            // Create a new stream writer
            StreamWriter writer = new StreamWriter("SaveData.txt");

            // Save current enemy index
            writer.WriteLine(_currentEnemyIndex);

            // Save player and enemy stats
            _player.Save(writer);
            _currentEnemy.Save(writer);

            // Close writer when done saving
            writer.Close();
        }

        /// <summary>
        /// Create a new stream writer for player for load
        /// </summary>
        /// <returns></returns>
        public bool Load()
        {
            bool loadSuccessful = true;

            // If the file doesn't exist...
            if (!File.Exists("SaveData.txt"))
            {
                //...return false
                loadSuccessful = false;
            }

            // Create a new reader to read from the text file
            StreamReader reader = new StreamReader("SaveData.txt");

            // If the first line can't be converted into an integer...
            if (!int.TryParse(reader.ReadLine(), out _currentEnemyIndex))
            {
                //...return false
                loadSuccessful = false;
            }

            // Load player job
            string job = reader.ReadLine();

            // Assign items based on player job
            if (job == "Wizard")
            {
                _player = new Player(_wizardItems);
            }  
            else if (job == "Knight")
            {
                _player = new Player(_knightItems);
            }
            else
            {
                loadSuccessful = false;
            }
                
            _player.Job = job;

            if (!_player.Load(reader))
            {
                loadSuccessful = false;
            }

            // Create a new instance and try to load the enemy
            _currentEnemy = new Entity();
            if (!_currentEnemy.Load(reader))
            {
                loadSuccessful = false;
            }

            // Update the array to match the current enemy stats
            _enemies[_currentEnemyIndex] = _currentEnemy;

            // Close the reader once loading is finished
            reader.Close();

            return loadSuccessful;
        }

        /// <summary>
        /// Prints a characters stats to the console.
        /// </summary>
        /// <param name="character">The character that will have its stats shown</param>
        void DisplayStats(Entity character)
        {
            Console.WriteLine("Name: " + character.Name);
            Console.WriteLine("Health: " + character.Health);
            Console.WriteLine("Attack Power: " + character.AttackPower);
            Console.WriteLine("Defense Power: " + character.DefensePower + "\n");
        }

        public void DisplayEquipItemMenu()
        {
            // Get item index
            int choice = GetInput("Select an item to equip.", _player.GetItemNames());

            // Equip item at given index
            if (!_player.TryEquipItem(choice))
            {
                Console.WriteLine("You couldn't find that item in your bag.");
            }

            // Print feedback
            Console.WriteLine("You equipped " + _player.CurrentItem.Name + "!");
        }

        /// <summary>
        /// Simulates one turn in the current monster fight
        /// </summary>
        public void Battle()
        {
            float damageDealt = 0;

            // Display Player Stats
            DisplayStats(_player);

            // Display Enemy Stats
            DisplayStats(_currentEnemy);

            int choice = GetInput("A " + _currentEnemy.Name + " stands in front of you! What will you do?", 
                "Attack", "Equip Item", "Unequip Item", "Save");

            // Attack
            if (choice == 0)
            {
                // The player attacks the enemy
                damageDealt = _player.Attack(_currentEnemy);
                Console.WriteLine("You dealt " + damageDealt + " damage!");
            }

            // Equip Item
            else if (choice == 1)
            {
                DisplayEquipItemMenu();
                Console.ReadKey(true);
                Console.Clear();
                return;
            }

            // Unequip Item
            else if (choice == 2)
            {
                if (!_player.TryRemoveCurrentItem())
                {
                    Console.WriteLine("You don't have anything equipped.");
                }
                else
                {
                    Console.WriteLine("You placed the item in your bag.");
                }
                    
                Console.ReadKey(true);
                Console.Clear();
                return;
            }

            // Save
            else if (choice == 3)
            {
                // Called the Save function 
                Save();

                // Feedback
                Console.WriteLine("Saved Game");
                Console.ReadKey(true);
                Console.Clear();
                return;
            }

            damageDealt = _currentEnemy.Attack(_player);
            Console.WriteLine("The " + _currentEnemy.Name + " dealt " + damageDealt, " damage!");

            
            Console.ReadKey(true);
            Console.Clear();
        }

        /// <summary>
        /// Checks to see if either the player or the enemy has won the current battle.
        /// Updates the game based on who won the battle..
        /// </summary>
        void CheckBattleResults()
        {
            if (_player.Health <= 0)
            {
                Console.WriteLine("You were slain...");
                Console.ReadKey(true);
                Console.Clear();
                _currentScene = Scene.RESTART;
            }
            else if (_currentEnemy.Health <= 0)
            {
                Console.WriteLine("You slayed the " + _currentEnemy.Name);
                Console.ReadKey();
                Console.Clear();

                _currentEnemyIndex++;

                if (_currentEnemyIndex >= _enemies.Length)
                {
                    _currentScene = Scene.RESTART;
                    return;
                }

                _currentEnemy = _enemies[_currentEnemyIndex];
            }
        }
    }
}
