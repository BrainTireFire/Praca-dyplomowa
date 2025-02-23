import { useQuery } from "@tanstack/react-query";
import { getItemFamilies as getItemFamiliesForBlueprint } from "../../../services/apiEffectBlueprints";
import { getItemFamilies as getItemFamiliesForInstance } from "../../../services/apiEffectInstances";
import { EffectContextType } from "../contexts/BlueprintOrInstanceContext";

export function useItemFamiliesForEffect(effect: EffectContextType) {
  const {
    isLoading,
    data: itemFamilies,
    error,
  } = useQuery({
    queryKey: ["itemFamilies", effect.effectId, effect.effect],
    queryFn: () =>
      effect.effect === "Blueprint"
        ? getItemFamiliesForBlueprint(effect.effectId)
        : getItemFamiliesForInstance(effect.effectId),
  });

  return { isLoading, itemFamilies, error };
}
