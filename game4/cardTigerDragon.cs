using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game4
{
    internal class cardTigerDragon
    {
        public string Suit { get; set; } // ชุดไพ่ (Spades, Hearts, Diamonds, Clubs)
        public string Value { get; set; } // ค่าของไพ่ (Ace, 2-10, Jack, Queen, King)

        public cardTigerDragon(string suit, string value)
        {
            Suit = suit;
            Value = value;
        }

        // คำนวณค่าของไพ่ (Ace = 1, Jack = 10, Queen = 12, King = 13)
        public int GetCardValue()
        {
            if (Value == "Ace") return 1;
            if (Value == "Jack") return 10;
            if (Value == "Queen") return 12;
            if (Value == "King") return 13;
            return int.Parse(Value); // สำหรับไพ่ 2-10
        }

        // โหลดภาพไพ่จากไฟล์ (ขึ้นอยู่กับการตั้งชื่อไฟล์ เช่น "Ace_of_Spades.png")
        public System.Drawing.Image GetCardImage()
        {
            return System.Drawing.Image.FromFile($"cards/{Value}_of_{Suit}.png");
        }
    }
}
