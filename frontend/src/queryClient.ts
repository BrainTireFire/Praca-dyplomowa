import { QueryCache, QueryClient } from "@tanstack/react-query";
import { toast } from "react-hot-toast";

const queryClient = new QueryClient({
  // queryCache: new QueryCache({
  //   onError: (error, query) => {
  //     console.log("TESTTSATSA ", error);
  //     if (error.message === "Unauthorized") {
  //       window.location.replace("/login");
  //     } else if (error.message === "Forbidden") {
  //       window.location.replace("/forbidden");
  //     } else {
  //       toast.error(error.message || "An error occurred");
  //     }
  //   },
  // }),
  defaultOptions: {
    queries: {
      staleTime: 0,
      //   onError: (error: any) => {
      //     if (error.message === "Unauthorized") {
      //       window.location.replace("/");
      //     } else {
      //       toast.error(error.message || "An error occurred");
      //     }
      //   },
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
