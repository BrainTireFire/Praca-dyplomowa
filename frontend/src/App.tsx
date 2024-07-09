import { I18nextProvider } from "react-i18next";
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import { ReactQueryDevtools } from "@tanstack/react-query-devtools";

import "./App.css";
import GlobalStyles from "./styles/GlobalStyles";
import Router from "./Router";
import i18n from "./i18n/i18n";

const queryClient = new QueryClient({
  defaultOptions: {
    queries: {
      staleTime: 0,
    },
  },
});

function App() {
  return (
    <>
      <QueryClientProvider client={queryClient}>
        <ReactQueryDevtools initialIsOpen={false} />
        <I18nextProvider i18n={i18n}>
          <GlobalStyles />
          <Router />
        </I18nextProvider>
      </QueryClientProvider>
    </>
  );
}

export default App;
