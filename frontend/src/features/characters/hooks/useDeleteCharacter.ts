import { useMutation, useQueryClient } from "@tanstack/react-query";
import toast from "react-hot-toast";
import { deleteCharacter as deleteCharacterApi } from "../../../services/apiCharacters";

export function useDeleteCharacter(onSuccess: () => void) {
  const queryClient = useQueryClient();
  const { mutate: deleteCharacter, isPending } = useMutation({
    mutationFn: (characterId: number) => deleteCharacterApi(characterId),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["characters"] });
      queryClient.invalidateQueries({ queryKey: ["npcCharacters"] });
      toast.success("Character deleted");
      onSuccess();
    },
    onError: (error) => {
      console.error(error);
      toast.error("Character deletion failed");
    },
  });

  return {
    deleteCharacter,
    isPending,
  };
}
