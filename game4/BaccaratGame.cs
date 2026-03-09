using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game4
{
        internal class BaccaratGame
        {
            private Random rand;
            public List<Card> PlayerCards { get; private set; }
            public List<Card> BankerCards { get; private set; }

            private static readonly string[] suits = { "Spades", "Hearts", "Diamonds", "Clubs" };
            private static readonly string[] values = { "Ace", "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King" };

        public BaccaratGame()
        {
            rand = new Random();
            PlayerCards = new List<Card>();
            BankerCards = new List<Card>();
        }

        public string PlayGame()
        {
            // แจกไพ่ให้ Player และ Banker
            PlayerCards.Clear();
            BankerCards.Clear();

            // แจกไพ่ให้ Player
            PlayerCards.Add(DealCard());
            PlayerCards.Add(DealCard());

            // แจกไพ่ให้ Banker
            BankerCards.Add(DealCard());
            BankerCards.Add(DealCard());

            // คำนวณคะแนน
            int playerScore = GetPlayerScore();
            int bankerScore = GetBankerScore();

            // ตรวจสอบผลการเล่น
            if (playerScore > bankerScore)
                return "Player wins!";
            else if (playerScore < bankerScore)
                return "Banker wins!";
            else
                return "It's a tie!";
        }

        public int GetPlayerScore()
        {
            // คำนวณคะแนนของ Player โดยใช้ค่าของไพ่
            int score = (PlayerCards[0].GetCardValue() + PlayerCards[1].GetCardValue()) % 10;
            return score;
        }

        public int GetBankerScore()
        {
            // คำนวณคะแนนของ Banker โดยใช้ค่าของไพ่
            int score = (BankerCards[0].GetCardValue() + BankerCards[1].GetCardValue()) % 10;
            return score;
        }

        private Card DealCard()
        {
            // สุ่มเลือกชุดและค่าไพ่
            string suit = suits[rand.Next(suits.Length)];
            string value = values[rand.Next(values.Length)];

            return new Card(suit, value);
        }
    }
}
