
using System;

namespace Game
{
    class Program
    {
        static void Main(string[] args)
        {
            // Welcome Message
            Console.WriteLine("Welcome to the Fantasy Adventure Game!");

            // Player Character Creation
            Console.WriteLine("Create your character:");
            Console.Write("Name: ");
            string name = Console.ReadLine();
            Console.Write("Health: ");
            int health = Convert.ToInt32(Console.ReadLine());
            Console.Write("Mana: ");
            int mana = Convert.ToInt32(Console.ReadLine());
            int experience = 0;

            // Game Start
            Console.WriteLine("Welcome to the dark and mysterious forest, {0}!", name);
            Console.WriteLine("You have been chosen to protect the forest from evil forces. Good luck!");

            // Game Loop
            while (health > 0)
            {
                // Enemy Encounter
                Console.WriteLine("Oh no! You've encountered an enemy!");
                Console.WriteLine("What do you do? (Fight/Flee)");
                string action = Console.ReadLine();

                if (action == "Fight")
                {
                    // Player chooses to fight
                    Console.WriteLine("You choose to fight!");
                    Console.WriteLine("You use your magical powers and defeat the enemy!");
                    Console.WriteLine("You gain experience and health points!");
                    // Increase health and experience points
                    health += 10;
                    experience += 10;
                }
                else if (action == "Flee")
                {
                    // Player chooses to flee
                    Console.WriteLine("You choose to flee!");
                    Console.WriteLine("You run away from the enemy!");
                    // Decrease health
                    health -= 5;
                }

            }

            // Game Over
            Console.WriteLine("Oh no! You have been defeated. Game over!");
        }
    }
}