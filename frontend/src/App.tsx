import { I18nextProvider } from "react-i18next";
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import { ReactQueryDevtools } from "@tanstack/react-query-devtools";
import { Toaster } from "react-hot-toast";

import "./App.css";
import GlobalStyles from "./styles/GlobalStyles";
import Router from "./Router";
import i18n from "./i18n/i18n";
import queryClient from "./queryClient";
import { DarkModeProvider } from "./context/DarkModeContext";
import styled from "styled-components";

function App() {
  return (
    <DarkModeProvider>
      <QueryClientProvider client={queryClient}>
        <ReactQueryDevtools initialIsOpen={false} />
        <I18nextProvider i18n={i18n}>
          <GlobalStyles />
          <Router />

          <Toaster
            position="top-center"
            gutter={12}
            containerStyle={{ margin: "8px" }}
            toastOptions={{
              success: {
                duration: 3000,
              },
              error: {
                duration: 5000,
              },
              style: {
                fontSize: "16px",
                maxWidth: "500px",
                padding: "16px 24px",
                backgroundColor: "var(--color-navbar)",
                color: "var(--color-secondary-text)",
              },
            }}
          />
          {/* <div
            id="draggable"
            style={{
              position: "absolute",
              pointerEvents: "none",
              height: "100%",
              width: "100%",
            }}
          ></div> */}
        </I18nextProvider>
      </QueryClientProvider>
    </DarkModeProvider>
  );
}

export default App;
