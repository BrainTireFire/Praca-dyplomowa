export type DiceSet = {
  d100: number;
  d20: number;
  d12: number;
  d10: number;
  d8: number;
  d6: number;
  d4: number;
  flat: number;
};

export function DiceSetString(diceSet: DiceSet): string {
  if (!diceSet) {
    return "none";
  }
  const parts: string[] = [];

  // Add dice values to parts array, ignoring 0s
  if (diceSet.d100 > 0) parts.push(`${diceSet.d100}d100`);
  if (diceSet.d20 > 0) parts.push(`${diceSet.d20}d20`);
  if (diceSet.d12 > 0) parts.push(`${diceSet.d12}d12`);
  if (diceSet.d10 > 0) parts.push(`${diceSet.d10}d10`);
  if (diceSet.d8 > 0) parts.push(`${diceSet.d8}d8`);
  if (diceSet.d6 > 0) parts.push(`${diceSet.d6}d6`);
  if (diceSet.d4 > 0) parts.push(`${diceSet.d4}d4`);

  // Add flat value if greater than 0
  if (diceSet.flat !== 0) parts.push(`${diceSet.flat}`);
  if (parts.length === 0) parts.push("none");
  // Join parts with a + separator
  return parts.join("+");
}
