export type CoinPurse = {
  goldPieces: number;
  silverPieces: number;
  copperPieces: number;
};

export const coinPursePrint = (money: CoinPurse) => {
  return (
    money.goldPieces +
    "GP " +
    money.silverPieces +
    "SP " +
    money.copperPieces +
    "CP "
  );
};
