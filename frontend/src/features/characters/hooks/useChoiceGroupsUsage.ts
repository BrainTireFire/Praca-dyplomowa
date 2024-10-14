import { useMutation, useQueryClient } from "@tanstack/react-query";
import toast from "react-hot-toast";
import { makeUseCharacterChoiceGroups } from "../../../services/apiCharacters";
import { ChoiceGroupUse } from "../../../services/apiCharacters";

export function useChoiceGroupUsage(onSuccess: () => void) {
  const queryClient = useQueryClient();
  const { mutate: generateChoiceGroupUsage, isPending } = useMutation({
    mutationFn: ({
      characterId,
      choiceGroupUsage,
    }: {
      characterId: number;
      choiceGroupUsage: ChoiceGroupUse[];
    }) => makeUseCharacterChoiceGroups(characterId, choiceGroupUsage),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["character"] });
      onSuccess();
    },
    onError: (error) => {
      console.error(error);
      toast.error("Choice selection failed!");
    },
  });

  return {
    generateChoiceGroupUsage,
    isPending,
  };
}
