import { useQuery } from "@tanstack/react-query";
import { getPower } from "../../../services/apiPowers";

export function usePower(powerId: number | null) {
  const {
    isLoading,
    data: power,
    error,
  } = useQuery({
    queryKey: ["power", powerId],
    queryFn: () => {
      if (powerId) {
        console.log("Load: " + powerId);
        return getPower(powerId);
      }
      return Promise.reject(new Error("Power ID is undefined"));
    },
    retry: false,
    enabled: !!powerId, // Only run query if powerId is defined
  });

  return { isLoading, power, error };
}
