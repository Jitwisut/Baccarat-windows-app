using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game4
{
    internal class Card
    {
        public string Suit { get; set; } // ชุดไพ่ (Spades, Hearts, Diamonds, Clubs)
        public string Value { get; set; } // ค่าของไพ่ (Ace, 2-10, Jack, Queen, King)

        public Card(string suit, string value)
        {
            Suit = suit;
            Value = value;
        }

        public int GetCardValue()
        {
            // กำหนดค่าของไพ่ (Ace = 1, 2-10 ตามเลข, Jack/Queen/King = 10)
            if (Value == "Ace")
                return 1;
            else if (Value == "Jack" || Value == "Queen" || Value == "King")
                return 10;
            else
                return int.Parse(Value);
        }

        public System.Drawing.Image GetCardImage()
        {
            // ใช้ชื่อไพ่ในการโหลดภาพจากไฟล์
            return System.Drawing.Image.FromFile($"cards/{Value}_of_{Suit}.png");
        }
    }
}
