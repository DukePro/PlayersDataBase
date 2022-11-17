using System.Collections.Generic;
using System.Numerics;

namespace MyProgramm
{
    class Programm
    {
        static void Main()
        {
            Menu menu = new Menu();
            menu.ShowMenu();
        }
    }

    class Menu
    {
        const string AddPlayer = "1";
        const string RemovePlayer = "2";
        const string ShowAllPlayers = "3";
        const string BanPlayer = "4";
        const string UnBanPlayer = "5";
        const string Exit = "6";
        bool isExit = false;
        string userInput;
        DataBase dataBase = new DataBase();

        public void ShowMenu()
        {
            while (isExit == false)
            {
                Console.WriteLine("\nМеню:");
                Console.WriteLine(AddPlayer + " - Добавить игрока");
                Console.WriteLine(RemovePlayer + " - Удалить игрока");
                Console.WriteLine(ShowAllPlayers + " - Показать всех игроков");
                Console.WriteLine(BanPlayer + " - Забанить игрока");
                Console.WriteLine(UnBanPlayer + " - Разбанить игрока");
                Console.WriteLine(Exit + " - Выход");
                userInput = Console.ReadLine();

                switch (userInput)
                {
                    case AddPlayer:
                        dataBase.AddPlayer();
                        break;
                    case RemovePlayer:
                        dataBase.RemovePlayer(dataBase.FindPlayerById());
                        break;
                    case ShowAllPlayers:
                        dataBase.ShowAllRecords();
                        break;
                    case BanPlayer:
                        dataBase.BanPlayer(dataBase.FindPlayerById());
                        break;
                    case UnBanPlayer:
                        dataBase.UnBanPlayer(dataBase.FindPlayerById());
                        break;
                    case Exit:
                        isExit = true;
                        break;
                }
            }
        }

    }

    class Player
    {
        public int PlayerId { get; private set; }
        public string PlayerName { get; private set; }
        public int PlayerLevel { get; private set; }
        public bool IsPlayerBanned { get; private set; }

        public Player(int playerId, string playerName, int playerLevel = 0, bool isPlayerBanned = false)
        {
            PlayerId = playerId;
            PlayerName = playerName;
            PlayerLevel = playerLevel;
            IsPlayerBanned = isPlayerBanned;
        }

        public Player()
        {
            PlayerId = 0;
            PlayerName = null;
            PlayerLevel = 0;
            IsPlayerBanned = false;
        }

        public void BanFieldTrue()
        {
            IsPlayerBanned = true;
            Console.WriteLine("Игрок забанен");
        }

        public void BanFieldFalse()
        {
            IsPlayerBanned = false;
            Console.WriteLine("Игрок разбанен");
        }
    }

    class ShowFields
    {
        public void PlayerInfo(Player player)
        {
            Console.WriteLine($"Id: {player.PlayerId} | Name: {player.PlayerName} | Level: {player.PlayerLevel} | Ban status: {player.IsPlayerBanned}");
        }

    }

    class DataBase
    {
        private List<Player> _players = new List<Player>();
        ShowFields fields = new ShowFields();
        public static int IdCounter = 0;

        public void AddPlayer()
        {
            int id = IdCounter++;
            Console.WriteLine("Введите имя игрока");
            string name = Console.ReadLine();
            _players.Add(new Player(id, name));
        }

        static int ParseNumber()
        {
            int parsedNumber = 0;
            bool isParsed = false;

            while (isParsed == false)
            {
                string userInput = Console.ReadLine();
                isParsed = int.TryParse(userInput, out parsedNumber);

                if (isParsed == false)
                {
                    Console.WriteLine("Введите целое число:");
                }
            }

            return parsedNumber;
        }

        public void RemovePlayer(Player playerStatus)
        {
            if (_players.Remove(playerStatus))
            {
                Console.WriteLine("Игрок удалён");
            }
        }

        public void ShowAllRecords()
        {
            foreach (var playerField in _players)
            {
                fields.PlayerInfo(playerField);
            }
        }

        public Player FindPlayerById()
        {
            Console.WriteLine("Введите ID игрока:");
            int id = ParseNumber();
            bool isFound = false;
            Player play = new Player();
            var playerStatus = play;

            for (int i = 0; i < _players.Count; i++)
            {
                if (_players[i].PlayerId == id)
                {
                    playerStatus = _players[i];
                    _players[i] = playerStatus;
                    isFound = true;
                }
            }
            if (isFound == true)
            {
                Console.WriteLine("Игрок найден");
            }
            else
            {
                Console.WriteLine("Игрока с таким ID не найдено");
            }

            return playerStatus;
        }

        public void BanPlayer(Player playerStatus)
        {
            playerStatus.BanFieldTrue();
        }

        public void UnBanPlayer(Player playerStatus)
        {
            playerStatus.BanFieldFalse();
        }
    }
}