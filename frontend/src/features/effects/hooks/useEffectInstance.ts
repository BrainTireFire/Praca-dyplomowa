import { useQuery } from "@tanstack/react-query";
import { getEffectInstance } from "../../../services/apiEffectInstances";

export function useEffectInstance(effectId: number | null) {
  const {
    isLoading,
    data: effectInstance,
    error,
  } = useQuery({
    queryKey: ["effectInstance", effectId],
    queryFn: () => {
      if (effectId) {
        return getEffectInstance(effectId);
      }
      return Promise.reject(new Error("Effect Instance ID is undefined"));
    },
    retry: false,
    enabled: !!effectId, // Only run query if characterId is defined
  });

  return { isLoading, effectInstance, error };
}
