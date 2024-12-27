import { useMutation, useQueryClient } from "@tanstack/react-query";
import toast from "react-hot-toast";
import { postCharacter } from "../../../services/apiCharacters";
import { CharacterInsertDto } from "../../../models/character";

export function useCreateNpcCharacter(onSuccess: () => void) {
  const queryClient = useQueryClient();
  const { mutate: createNpcCharacter, isPending } = useMutation({
    mutationFn: (npcCharacter: CharacterInsertDto) =>
      postCharacter(npcCharacter),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["npcCharacters"] });
      onSuccess();
    },
    onError: (error) => {
      console.error(error);
      toast.error("NpcCharacter creation failed");
    },
  });
  return {
    createNpcCharacter,
    isPending,
  };
}
