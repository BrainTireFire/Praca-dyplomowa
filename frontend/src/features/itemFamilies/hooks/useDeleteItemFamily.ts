import { useMutation, useQueryClient } from "@tanstack/react-query";
import toast from "react-hot-toast";
import { deleteItemFamily as deleteItemFamilyApi } from "../../../services/apiItemFamilies";

export function useDeleteItemFamily(onSuccess: () => void) {
  const queryClient = useQueryClient();
  const { mutate: deleteItemFamily, isPending } = useMutation({
    mutationFn: (itemFamilyId: number) => deleteItemFamilyApi(itemFamilyId),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["itemFamilies"] });
      // Explicitly refetch the query after invalidation
      queryClient.refetchQueries({ queryKey: ["itemFamilies"] });
      onSuccess();
    },
    onError: (error) => {
      console.error(error);
      toast.error(error.message);
    },
  });

  return {
    deleteItemFamily,
    isPending,
  };
}
