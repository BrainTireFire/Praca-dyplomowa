import { useMutation, useQueryClient } from "@tanstack/react-query";
import toast from "react-hot-toast";
import { setActiveTurn as setActiveTurnApi } from "../../../services/apiEncounter";

function useSetActiveTurn(encounterId: number, onSuccess: () => void) {
  const queryClient = useQueryClient();
  const { mutate: setActiveTurn, isPending } = useMutation({
    mutationFn: (characterId: number) =>
      setActiveTurnApi(encounterId, characterId),
    onSuccess: () => {
      queryClient.invalidateQueries({
        queryKey: ["initiativeQueue", encounterId],
      });
      queryClient.refetchQueries({
        queryKey: ["initiativeQueue", encounterId],
      });
      toast.success("Changed active character");
      onSuccess();
    },
    onError: (err) => toast.error(err.message),
  });

  return { setActiveTurn, isPending };
}

export default useSetActiveTurn;
