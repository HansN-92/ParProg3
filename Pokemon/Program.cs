namespace Pokemon
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Hva heter du, trener? ");
            string name = Console.ReadLine()?.Trim() ?? "Ash";

            Console.WriteLine("\nVelg startpokemon:");
            Console.WriteLine("1) Charmander  [fire]");
            Console.WriteLine("2) Bulbasaur   [grass]");
            Console.WriteLine("3) Squirtle    [water]");
            Console.Write("> ");

            Pokemon starter = Console.ReadLine()?.Trim() switch
            {
                "1" => new Pokemon("Charmander", "fire", 39, 14, 7,
                           new List<Move> { new("Ember", 12, 85), new("Scratch", 6, 95) }),
                "2" => new Pokemon("Bulbasaur", "grass", 45, 12, 8,
                           new List<Move> { new("Vine Whip", 10, 85), new("Tackle", 6, 95) }),
                _ => new Pokemon("Squirtle", "water", 44, 11, 10,
                           new List<Move> { new("Water Gun", 10, 85), new("Tackle", 6, 95) }),
            };

            Console.WriteLine($"Du valgte {starter.Name}!");
            new App(new Trainer(name, starter)).Run();
        }
    }
}
