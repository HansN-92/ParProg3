namespace Pokemon
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Hva heter du, trener? ");
            string name = Console.ReadLine()?.Trim() ?? "Ash";

            Console.WriteLine("\nVelg startpokemon:");
            Console.WriteLine("1) Charmander  [flamme]");
            Console.WriteLine("2) Bulbasaur   [gress]");
            Console.WriteLine("3) Squirtle    [vann]");
            Console.Write("> ");

            Pokemon chosenPokemon;
            ConsoleKeyInfo starterChoice = Console.ReadKey(true);
                switch(starterChoice.Key)
            {
                case: ConsoleKey.D1:
                    chosenPokemon = new Pokemon("Charmander", "flamme", 39, 14, 7,
                        new List<Move> { new("Ember", 12, 85), new("Scratch", 6, 95) });
                    break;

                case: ConsoleKey.D2:
                    chosenPokemon => new Pokemon("Bulbasaur", "gress", 45, 12, 8,
                        new List<Move> { new("Vine Whip", 10, 85), new("Tackle", 6, 95) });
                    break;

                case: ConsoleKey.D3:
                    chosenPokemon => new Pokemon("Squirtle", "vann", 44, 11, 10,
                        new List<Move> { new("Water Gun", 10, 85), new("Tackle", 6, 95) });
                    break;

                   default  _ => null;
            };

            Console.WriteLine($"Du valgte {starter.Name}!");
            new App(new Trainer(name, starter)).Run();
        }
    }
}
