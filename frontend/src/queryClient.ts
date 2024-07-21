import { QueryCache, QueryClient } from "@tanstack/react-query";
import { toast } from "react-hot-toast";

const queryClient = new QueryClient({
  //   queryCache: new QueryCache({
  //     onError: (error, query) => {
  //       if (error.message === "Unauthorized") {
  //         window.location.replace("/");
  //       } else {
  //         toast.error(error.message || "An error occurred");
  //       }
  //     },
  //   }),
  defaultOptions: {
    queries: {
      staleTime: 0,
    },
    // mutations: {
    //   onError: (error: any) => {
    //     if (error.message === "Unauthorized") {
    //       window.location.replace("/");
    //     } else {
    //       toast.error(error.message || "An error occurred");
    //     }
    //   },
    // },
  },
});

export default queryClient;
