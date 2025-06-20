import { useMutation, useQueryClient } from "@tanstack/react-query";
import toast from "react-hot-toast";
import { postCharacter } from "../../../services/apiCharacters";
import { CharacterInsertDto } from "../../../models/character";

export function useCreateCharacter(onSuccess: () => void) {
  const queryClient = useQueryClient();
  const { mutate: createCharacter, isPending } = useMutation({
    mutationFn: (character: CharacterInsertDto) => postCharacter(character),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["characters"] });
      onSuccess();
    },
    onError: (error) => {
      toast.error("Character creation failed");
    },
  });

  return {
    createCharacter,
    isPending,
  };
}
