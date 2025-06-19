using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace pracadyplomowa.Models.Entities.Items
{
    [Owned]
    public class CoinSack
    {
        public int GoldPieces { get; set; }
        public int SilverPieces { get; set; }
        public int CopperPieces { get; set; }

        public int GetValueInCopperPieces()
        {
            return GoldPieces * 100 + SilverPieces * 10 + CopperPieces;
        }

        public static CoinSack operator +(CoinSack x, CoinSack y)
        {

            int totalCopper = x.GetValueInCopperPieces() + y.GetValueInCopperPieces();

            int gold = totalCopper / 100;
            totalCopper %= 100;
            int silver = totalCopper / 10;
            int copper = totalCopper % 10;

            return new CoinSack
            {
                GoldPieces = gold,
                SilverPieces = silver,
                CopperPieces = copper
            };
        }

        public static CoinSack operator -(CoinSack x, CoinSack y)
        {
            int totalCopper = x.GetValueInCopperPieces() - y.GetValueInCopperPieces();
            bool negative = totalCopper < 0;
            totalCopper = Math.Abs(totalCopper);

            int gold = totalCopper / 100;
            totalCopper %= 100;
            int silver = totalCopper / 10;
            int copper = totalCopper % 10;

            if (negative)
            {
                gold = -gold;
                silver = -silver;
                copper = -copper;
            }

            return new CoinSack
            {
                GoldPieces = gold,
                SilverPieces = silver,
                CopperPieces = copper
            };
        }

        public void Add(CoinSack other)
        {
            var sum = this + other;
            this.GoldPieces = sum.GoldPieces;
            this.SilverPieces = sum.SilverPieces;
            this.CopperPieces = sum.CopperPieces;
        }

        public void Subtract(CoinSack other)
        {
            var diff = this - other;
            this.GoldPieces = diff.GoldPieces;
            this.SilverPieces = diff.SilverPieces;
            this.CopperPieces = diff.CopperPieces;
        }

        public static bool operator >(CoinSack x, CoinSack y) => x.GetValueInCopperPieces() > y.GetValueInCopperPieces();
        public static bool operator <(CoinSack x, CoinSack y) => x.GetValueInCopperPieces() < y.GetValueInCopperPieces();
        public static bool operator >=(CoinSack x, CoinSack y) => x.GetValueInCopperPieces() >= y.GetValueInCopperPieces();
        public static bool operator <=(CoinSack x, CoinSack y) => x.GetValueInCopperPieces() <= y.GetValueInCopperPieces();
        public static bool operator ==(CoinSack x, CoinSack y) => x.GetValueInCopperPieces() == y.GetValueInCopperPieces();
        public static bool operator !=(CoinSack x, CoinSack y) => x.GetValueInCopperPieces() != y.GetValueInCopperPieces();
    }
}