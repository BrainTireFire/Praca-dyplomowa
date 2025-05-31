import { useMutation, useQueryClient } from "@tanstack/react-query";
import toast from "react-hot-toast";
import { giveItemApi } from "../../../services/apiItems";

export default function useGiveItem() {
  const queryClient = useQueryClient();
  const { mutate: giveItem, isPending } = useMutation({
    mutationFn: ({
      itemId,
      characterId,
    }: {
      itemId: number;
      characterId: number;
    }) => giveItemApi(itemId, characterId),
    onSuccess: () => {
      queryClient.invalidateQueries({
        queryKey: ["items"],
      });
      queryClient.invalidateQueries({ queryKey: ["characters"] });
      toast.success("Item transfer successful");
    },
    onError: (err) => toast.error(err.message),
  });

  return {
    giveItem,
    isPending,
  };
}
