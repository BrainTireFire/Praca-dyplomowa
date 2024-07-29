import { useQuery } from "@tanstack/react-query";
import { getDice, getDiceById } from "../../services/apiDice";
import { useParams } from "react-router-dom";

export function useDice() {
  const { diceId } = useParams<{ diceId: string }>();

  const {
    isLoading,
    data: dice,
    error,
  } = useQuery({
    queryKey: ["dice", diceId],
    queryFn: () => {
      if (diceId) return getDiceById(diceId);
      else return Promise.reject(new Error("Dice ID is undefined"));
    },
    retry: false,
    enabled: !!diceId,
  });

  return { isLoading, dice, error };
}

export function useAllDice() {
  const {
    isLoading,
    data: dice,
    error,
  } = useQuery({
    queryKey: ["allDice"],
    queryFn: getDice,
  });

  return { isLoading, dice, error };
}
