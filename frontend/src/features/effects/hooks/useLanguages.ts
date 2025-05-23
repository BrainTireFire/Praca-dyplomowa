import { useQuery } from "@tanstack/react-query";
import { getLanguages as getLanguagesForBlueprint } from "../../../services/apiEffectBlueprints";
import { getLanguages as getLanguagesForInstance } from "../../../services/apiEffectInstances";
import { EffectContextType } from "../contexts/BlueprintOrInstanceContext";

export function useLanguages(effect: EffectContextType) {
  const {
    isLoading,
    data: languages,
    error,
  } = useQuery({
    queryKey: ["languages", effect.effectId, effect.effect],
    queryFn: () =>
      effect.effect === "Blueprint"
        ? getLanguagesForBlueprint(effect.effectId)
        : getLanguagesForInstance(effect.effectId),
  });

  return { isLoading, languages, error };
}
