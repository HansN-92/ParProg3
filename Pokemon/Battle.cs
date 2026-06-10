namespace Pokemon
{
    internal class Battle
    {
        private readonly Random _rng = new();

        public void Start(Trainer trainer, Pokemon wild)
        {
            Console.WriteLine($"\n  En vill {wild.Name} dukket opp!");
            Pokemon? active = trainer.Party.FirstOrDefault(p => !p.IsFainted);
            if (active == null) { Console.WriteLine("Du har ingen pokemon som kan kjempe!"); return; }

            Console.WriteLine($"Går ut: {active.Name}");
            bool inBattle = true;

            while (inBattle && !active.IsFainted && !wild.IsFainted)
            {
                Console.WriteLine();
                active.PrintStatus();
                wild.PrintStatus();
                Console.WriteLine("1) Angrip");
                Console.WriteLine("2) Bruk Potion");
                Console.WriteLine("3) Kast Pokeball");
                Console.WriteLine("4) Løp");
                Console.Write("> ");

                ConsoleKeyInfo combatChoice = Console.ReadKey(true);
                switch (combatChoice.Key)
                {
                    case ConsoleKey.D1:
                        PlayerAttacks(active, wild);
                        if (!wild.IsFainted) EnemyAttacks(wild, active);
                        break;

                    case ConsoleKey.D2:
                        var potion = trainer.TakeItem(ItemType.Potion);
                        if (potion != null)
                        {
                            active.Heal(potion.Value);
                            Console.WriteLine($"Healet {active.Name} med {potion.Value} HP!");
                            EnemyAttacks(wild, active);
                        }
                        else Console.WriteLine("Ingen potions igjen!");
                        break;

                    case ConsoleKey.D3:
                        var ball = trainer.TakeItem(ItemType.Pokeball);
                        if (ball != null)
                        {
                            int catchChance = 40 + ball.Value + (100 - wild.Hp * 100 / wild.MaxHp) / 2;
                            if (_rng.Next(100) < catchChance)
                            {
                                Console.WriteLine($"Du fanget {wild.Name}!");
                                trainer.Party.Add(wild);
                                inBattle = false;
                            }
                            else
                            {
                                Console.WriteLine($"{wild.Name} brøt fri!");
                                EnemyAttacks(wild, active);
                            }
                        }
                        else Console.WriteLine("Ingen pokeballs igjen!");
                        break;

                    case ConsoleKey.D4:
                        if (_rng.Next(100) < 60)
                        {
                            Console.WriteLine("Du rømte!");
                            inBattle = false;
                        }
                        else Console.WriteLine($"{wild.Name} blokkerte flukten!");
                        break;

                    default:
                        Console.WriteLine("Ugyldig valg.");
                        break;
                }
            }

            if (wild.IsFainted)
            {
                int reward = _rng.Next(20, 60);
                Console.WriteLine($"{wild.Name} besvimte! Du fikk {reward}$");
                trainer.EarnMoney(reward);
            }
            if (active.IsFainted)
                Console.WriteLine($"{active.Name} har besvimt...");
        }

        private void PlayerAttacks(Pokemon attacker, Pokemon target)
        {
            if (attacker.Moves.Count == 0) return;
            var move = attacker.Moves[_rng.Next(attacker.Moves.Count)];

            if (_rng.Next(100) < move.Accuracy)
            {
                int dmg = Math.Max(1, attacker.Attack + move.Power - target.Defense);
                target.TakeDamage(dmg);
                Console.WriteLine($"{attacker.Name} brukte {move.Name}  {dmg} skade!");
            }
            else Console.WriteLine($"{attacker.Name} bommet!");
        }

        private void EnemyAttacks(Pokemon enemy, Pokemon target)
        {
            if (enemy.Moves.Count == 0) return;
            var move = enemy.Moves[new Random().Next(enemy.Moves.Count)];
            int dmg = Math.Max(1, enemy.Attack + move.Power - target.Defense);
            target.TakeDamage(dmg);
            Console.WriteLine($"{enemy.Name} angriper med {move.Name}  {dmg} skade!");
        }
    }
}
