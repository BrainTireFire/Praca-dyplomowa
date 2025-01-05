import { useQuery } from "@tanstack/react-query";
import { getEffectBlueprint } from "../../../services/apiEffectBlueprints";

export function useEffectBlueprint(effectId: number | null) {
  const {
    isLoading,
    data: effectBlueprint,
    error,
  } = useQuery({
    queryKey: ["effectBlueprint", effectId],
    queryFn: () => {
      if (effectId) {
        return getEffectBlueprint(effectId);
      }
      return Promise.reject(new Error("Effect Blueprint ID is undefined"));
    },
    retry: false,
    enabled: !!effectId, // Only run query if characterId is defined
  });

  return { isLoading, effectBlueprint, error };
}
