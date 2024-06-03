import { I18nextProvider } from "react-i18next";

import "./App.css";
import GlobalStyles from "./styles/GlobalStyles";
import Router from "./Router";
import i18n from "./i18n/i18n";

function App() {
  return (
    <>
      <I18nextProvider i18n={i18n}>
        <GlobalStyles />
        <Router />
      </I18nextProvider>
    </>
  );
}

export default App;
