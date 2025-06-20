import { useMutation, useQueryClient } from "@tanstack/react-query";
import toast from "react-hot-toast";
import { deleteEffectInstance as deleteEffectInstanceApi } from "../../../services/apiEffectInstances";

export function useDeleteEffectInstance(onSuccess: () => void, itemId: number) {
  const queryClient = useQueryClient();
  const { mutate: deleteEffectInstance, isPending } = useMutation({
    mutationFn: (id: number) => deleteEffectInstanceApi(id),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["item", itemId] });
      // Explicitly refetch the query after invalidation
      queryClient.refetchQueries({ queryKey: ["item", itemId] });
      toast.success("Effect deleted");
      onSuccess();
    },
    onError: (error) => {
      toast.error("Effect blueprint deletion failed");
    },
  });

  return {
    deleteEffectInstance,
    isPending,
  };
}
