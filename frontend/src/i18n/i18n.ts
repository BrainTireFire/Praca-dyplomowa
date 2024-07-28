import i18n from "i18next";
import { initReactI18next } from "react-i18next";

import enTranslation from "./en/en.json";
import deTranslation from "./de/de.json";
import esTranslation from "./es/es.json";
import frTranslation from "./fr/fr.json";
import jpTranslation from "./jp/jp.json";

const DEFAULT_LANGUAGE = "en";

i18n.use(initReactI18next).init({
  resources: {
    en: { translation: enTranslation },
    de: { translation: deTranslation },
    es: { translation: esTranslation },
    fr: { translation: frTranslation },
    jp: { translation: jpTranslation },
  },
  lng: DEFAULT_LANGUAGE,
  fallbackLng: DEFAULT_LANGUAGE,
  interpolation: {
    escapeValue: false,
  },
});

export default i18n;
