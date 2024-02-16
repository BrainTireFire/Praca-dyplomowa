import { Route } from "react-router-dom";
import "./App.css";
import LoginForm from "./features/account/LoginForm";
import Login from "./pages/Login";
import Register from "./pages/Register";
import GlobalStyles from "./styles/GlobalStyles";
import Input from "./ui/forms/Input";
import Router from "./Router";

function App() {
  return (
    <>
      <GlobalStyles />
      <Router />
    </>
  );
}

export default App;
