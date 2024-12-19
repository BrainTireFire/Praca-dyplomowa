import { useMutation, useQueryClient } from "@tanstack/react-query";
import toast from "react-hot-toast";
import { EffectBlueprint } from "../../effects/EffectBlueprintForm";
import { addEffectInstance as addItemEffectInstance } from "../../../services/apiItems";
import { EffectParentObjectIdContextTypeObjectType } from "../../../context/EffectParentObjectIdContext";
import {
  addConstantEffectInstance,
  addTemporaryEffectInstance,
} from "../../../services/apiCharacters";

export function useCreateEffectInstance(
  onSuccess: (id: number) => void,
  objectId: number,
  objectType: EffectParentObjectIdContextTypeObjectType,
  isConstant: boolean
) {
  const queryKey = objectType === "Item" ? "item" : "character";
  const queryClient = useQueryClient();
  const { mutate: createEffectInstance, isPending } = useMutation({
    mutationFn: (effectInstance: EffectBlueprint) =>
      objectType === "Item"
        ? addItemEffectInstance(effectInstance, objectId)
        : isConstant
        ? addConstantEffectInstance(effectInstance, objectId)
        : addTemporaryEffectInstance(effectInstance, objectId),
    onSuccess: (id: number) => {
      queryClient.invalidateQueries({ queryKey: [queryKey, objectId] });
      // Explicitly refetch the query after invalidation
      queryClient.refetchQueries({ queryKey: [queryKey, objectId] });
      onSuccess(id);
    },
    onError: (error) => {
      console.error(error);
      toast.error("Effect instance creation failed");
    },
  });

  return {
    createEffectInstance,
    isPending,
  };
}
