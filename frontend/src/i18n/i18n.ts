import i18n from "i18next";
import { initReactI18next } from "react-i18next";

import enTranslation from "./en/en.json";
import deTranslation from "./de/de.json";

const DEFAULT_LANGUAGE = "en";

i18n.use(initReactI18next).init({
  resources: {
    en: { translation: enTranslation },
    de: { translation: deTranslation },
  },
  lng: DEFAULT_LANGUAGE,
  fallbackLng: DEFAULT_LANGUAGE,
  interpolation: {
    escapeValue: false,
  },
});

export default i18n;
