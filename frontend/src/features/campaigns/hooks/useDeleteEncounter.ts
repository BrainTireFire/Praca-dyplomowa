import { useMutation, useQueryClient } from "@tanstack/react-query";
import toast from "react-hot-toast";
import { deleteEncounter as deleteEncounterApi } from "../../../services/apiEncounter";

function useDeleteEncounter(encounterId: number) {
  const queryClient = useQueryClient();
  const { mutate: deleteEncounter, isPending: isDeleting } = useMutation({
    mutationFn: () => deleteEncounterApi(encounterId),
    onSuccess: () => {
      queryClient.refetchQueries({
        queryKey: ["encounter", encounterId],
      });
      queryClient.refetchQueries({
        queryKey: ["encounters"],
      });
    },
    onError: (err) => toast.error(err.message),
  });

  return { deleteEncounter, isDeleting };
}

export default useDeleteEncounter;
