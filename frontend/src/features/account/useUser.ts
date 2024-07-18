import { useQuery } from "@tanstack/react-query";
import { getCurrentUser } from "../../services/apiAuth";
import { useNavigate } from "react-router-dom";
import toast from "react-hot-toast";

export function useUser() {
  const navigate = useNavigate();

  const {
    isLoading,
    data: user,
    error,
  } = useQuery({
    queryKey: ["user"],
    queryFn: async () => {
      try {
        return await getCurrentUser();
      } catch (error: any) {
        console.log("CYCKI " + error);
        if (error.message === "Unauthorized") {
          navigate("/login");
        } else {
          toast.error(error.message || "An error occurred");
        }
        throw error;
      }
    },
  });

  return { isLoading, user, error };
}
