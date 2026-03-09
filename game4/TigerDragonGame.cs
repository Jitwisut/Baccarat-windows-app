using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game4
{
    internal class TigerDragonGame
    {
        private Random rand;
        public cardTigerDragon TigerCard { get; private set; }
        public cardTigerDragon DragonCard { get; private set; }

        private static readonly string[] suits = { "Spades", "Hearts", "Diamonds", "Clubs" };
        private static readonly string[] values = { "Ace", "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King" };

        public TigerDragonGame()
        {
            rand = new Random();
        }

        // ฟังก์ชันในการเล่นเกม
        public string PlayGame(out int tigerScore, out int dragonScore)
        {
            // แจกไพ่ให้ Tiger และ Dragon (ไพ่ใบเดียว)
            TigerCard = DealCard();
            DragonCard = DealCard();

            // คำนวณคะแนน
            tigerScore = TigerCard.GetCardValue();
            dragonScore = DragonCard.GetCardValue();

            // ตรวจสอบผลการเล่น
            if (tigerScore > dragonScore)
                return "Tiger wins!";
            else if (tigerScore < dragonScore)
                return "Dragon wins!";
            else
                return "It's a tie!";
        }

        // สุ่มแจกไพ่
        private cardTigerDragon DealCard()
        {
            string suit = suits[rand.Next(suits.Length)];
            string value = values[rand.Next(values.Length)];
            return new cardTigerDragon(suit, value);
        }
    }
}

