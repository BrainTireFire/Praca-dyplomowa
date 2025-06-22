import { useQuery } from "@tanstack/react-query";
import { getActionLogsByEncounterId } from "../../../services/apiActionLog";

export function useActionLogs(encounterId: number) {
  const {
    isLoading,
    data: actionLogs,
    error,
  } = useQuery({
    queryKey: ["actionLogs", encounterId],
    queryFn: () => getActionLogsByEncounterId(encounterId),
  });

  return { isLoading, actionLogs, error };
}
