import { useMutation, useQueryClient } from "@tanstack/react-query";
import toast from "react-hot-toast";
import { postCharacter } from "../../../services/apiCharacters";
import { CharacterInsertDto } from "../../../models/character";

export function useCreateCharacter(onSuccess: () => void) {
  const { mutate: createCharacter, isPending } = useMutation({
    mutationFn: (character: CharacterInsertDto) => postCharacter(character),
    onSuccess: () => onSuccess(),
    onError: (error) => {
      console.error(error);
      toast.error("Character creation failed");
    },
  });

  return {
    createCharacter,
    isPending,
  };
}
