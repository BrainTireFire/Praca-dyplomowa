import { useMutation, useQueryClient } from "@tanstack/react-query";
import toast from "react-hot-toast";
import {
  updateCoinSack as updateCoinSackApi,
} from "../../../services/apiCharacters";
import { CoinPurse } from "../../items/models/coinPurse";

export function useUpdateCharacterCoinSack(characterId: number, onSuccess: () => void) {
  const queryClient = useQueryClient();
  const {
    mutate: updateCoinSack,
    isPending,
    error,
    isError,
  } = useMutation({
    mutationFn: ({
      coinSack
    }: {
      coinSack: CoinPurse
    }) => updateCoinSackApi(characterId, coinSack),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["character", characterId] });
      toast.success("Coins updated");
      onSuccess();
    },
    onError: (error) => {
      console.error(error);
      toast.error("Coins update failed");
    },
  });

  return {
    updateCoinSack,
    isPending,
    isError,
    error,
  };
}
