using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Entities.Items;

namespace pracadyplomowa.Models.DTOs
{
    public class CoinPurseDto
    {
        public int GoldPieces { get; set; }
        public int SilverPieces { get; set; }
        public int CopperPieces { get; set; }
        public CoinPurseDto(CoinSack coinSack)
        {
            GoldPieces = coinSack.GoldPieces;
            SilverPieces = coinSack.SilverPieces;
            CopperPieces = coinSack.CopperPieces;
        }
    }
}