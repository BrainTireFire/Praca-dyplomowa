import { useMutation, useQueryClient } from "@tanstack/react-query";
import toast from "react-hot-toast";
import { updateCharacter as updateCharacterApi } from "../../../services/apiCharacters";

export function useUpdateCharacter(characterId: number, onSuccess: () => void) {
  const queryClient = useQueryClient();
  const {
    mutate: updateCharacter,
    isPending,
    error,
    isError,
  } = useMutation({
    mutationFn: ({
      name,
      description,
    }: {
      name: string;
      description: string;
    }) => updateCharacterApi(characterId, name, description),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["character", characterId] });
      queryClient.invalidateQueries({ queryKey: ["characters"] });
      onSuccess();
    },
    onError: (error) => {
      console.error(error);
      toast.error("Character update failed");
    },
  });

  return {
    updateCharacter,
    isPending,
    isError,
    error,
  };
}
