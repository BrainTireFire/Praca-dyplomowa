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

        public int GetValueInCopperPieces(){
            return GoldPieces * 100 + SilverPieces * 10 + CopperPieces;
        }

        public static bool operator > (CoinSack x, CoinSack y) => x.GetValueInCopperPieces() > y.GetValueInCopperPieces();
        public static bool operator < (CoinSack x, CoinSack y) => x.GetValueInCopperPieces() < y.GetValueInCopperPieces();
        public static bool operator >= (CoinSack x, CoinSack y) => x.GetValueInCopperPieces() >= y.GetValueInCopperPieces();
        public static bool operator <= (CoinSack x, CoinSack y) => x.GetValueInCopperPieces() <= y.GetValueInCopperPieces();
        public static bool operator == (CoinSack x, CoinSack y) => x.GetValueInCopperPieces() == y.GetValueInCopperPieces();
        public static bool operator != (CoinSack x, CoinSack y) => x.GetValueInCopperPieces() != y.GetValueInCopperPieces();
    }
}