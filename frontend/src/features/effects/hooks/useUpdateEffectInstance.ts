import { useMutation, useQueryClient } from "@tanstack/react-query";
import toast from "react-hot-toast";
import { EffectBlueprint } from "../EffectBlueprintForm";
import { updateEffectInstance as updateEffectInstanceApi } from "../../../services/apiEffectInstances";
import { EffectParentObjectIdContextTypeObjectType } from "../../../context/EffectParentObjectIdContext";

export function useUpdateEffectInstance(
  onSuccess: () => void,
  effectId: number,
  objectId: number,
  objectType: EffectParentObjectIdContextTypeObjectType
) {
  const queryKey = objectType === "Item" ? "item" : "character";
  const queryClient = useQueryClient();
  const { mutate: updateEffectInstance, isPending } = useMutation({
    mutationFn: (effectBlueprint: EffectBlueprint) =>
      updateEffectInstanceApi(effectBlueprint),
    onSuccess: () => {
      queryClient.invalidateQueries({
        queryKey: ["effectInstance", effectId],
      });
      console.log(queryKey);
      console.log(objectId);
      queryClient.invalidateQueries({ queryKey: [queryKey, objectId] });
      onSuccess();
    },
    onError: (error) => {
      console.error(error);
      toast.error("Effect Instance update failed");
    },
  });

  return {
    updateEffectInstance,
    isPending,
  };
}
