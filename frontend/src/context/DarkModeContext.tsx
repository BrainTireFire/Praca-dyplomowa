import { createContext, useContext, useEffect } from "react";
import { useLocalStorageState } from "../hooks/useLocalStorageState";

const DarkModeContext = createContext();

function DarkModeProvider({ children }: { children: React.ReactNode }) {
  // const [isDarkMode, setIsDarkMode] = useLocalStorageState(
  //   window.matchMedia("(prefers-color-scheme: dark)").matches,
  //   "isDarkMode"
  // );
  const [isDarkMode, setIsDarkMode] = useLocalStorageState(true, "isDarkMode");

  useEffect(
    function name() {
      if (isDarkMode) {
        document.documentElement.classList.add("dark-mode");
        document.documentElement.classList.remove("light-mode");
      } else {
        document.documentElement.classList.add("light-mode");
        document.documentElement.classList.remove("dark-mode");
      }
    },
    [isDarkMode]
  );

  function toggleDarkMode() {
    setIsDarkMode((isDark) => !isDark);
  }

  return (
    <DarkModeContext.Provider value={{ isDarkMode, toggleDarkMode }}>
      {children}
    </DarkModeContext.Provider>
  );
}

function useDarkMode() {
  const context = useContext(DarkModeContext);
  if (context === undefined) {
    throw new Error("DarkModeContext was used outside of its provider");
  }

  return context;
}

export { DarkModeProvider, useDarkMode };
