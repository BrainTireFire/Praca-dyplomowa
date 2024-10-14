import { useMutation, useQueryClient } from "@tanstack/react-query";
import toast from "react-hot-toast";
import { selectNextClassLevel } from "../../../services/apiCharacters";

export function useSelectNextClassLevel(onSuccess: () => void) {
  const queryClient = useQueryClient();
  const { mutate: saveNextClassLevel, isPending } = useMutation({
    mutationFn: ({
      characterId,
      classLevelId,
    }: {
      characterId: number;
      classLevelId: number;
    }) => selectNextClassLevel(characterId, classLevelId),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["character"] });
      onSuccess();
    },
    onError: (error) => {
      console.error(error);
      toast.error("Class selection failed!");
    },
  });

  return {
    saveNextClassLevel,
    isPending,
  };
}
