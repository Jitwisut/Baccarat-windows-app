using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace game4
{
    private Random rand;
    public Card TigerCard { get; private set; }
    public Card DragonCard { get; private set; }

    private static readonly string[] suits = { "Spades", "Hearts", "Diamonds", "Clubs" };
    private static readonly string[] values = { "Ace", "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King" };

    public TigerDragonGame()
    {
        rand = new Random();
    }

    public string PlayGame()
    {
        // แจกไพ่ให้ Tiger และ Dragon (ไพ่ใบเดียว)
        TigerCard = DealCard();
        DragonCard = DealCard();

        // คำนวณคะแนน
        int tigerScore = GetCardValue(TigerCard);
        int dragonScore = GetCardValue(DragonCard);

        // ตรวจสอบผลการเล่น
        if (tigerScore > dragonScore)
            return "Tiger wins!";
        else if (tigerScore < dragonScore)
            return "Dragon wins!";
        else
            return "It's a tie!";
    }

    private int GetCardValue(Card card)
    {
        // คำนวณค่าของไพ่ (Ace = 1, 2-10 = ค่าของไพ่, Jack = 11, Queen = 12, King = 13)
        switch (card.Value)
        {
            case "Ace": return 1;
            case "Jack": return 11;
            case "Queen": return 12;
            case "King": return 13;
            default: return int.Parse(card.Value);
        }
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
