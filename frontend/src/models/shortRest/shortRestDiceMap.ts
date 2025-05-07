import { DiceSet } from "../diceset";

export type ShortRestDiceMap = {
    campaignId: number;
    hitDiceMap: Record<number, DiceSet>; // Equivalent to Dictionary<int, DiceSetDto>
}