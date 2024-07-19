import React, { useState } from "react";
import Form from "../../ui/forms/Form";
import FormRowVertical from "../../ui/forms/FormRowVertical";
import Input from "../../ui/forms/Input";
import Heading from "../../ui/text/Heading";
import Button from "../../ui/interactive/Button";
import LinkContainer from "../../ui/containers/LinkContainer";
import Link from "../../ui/links/Link";
import { useLogin } from "./useLogin";
import SpinnerMini from "../../ui/interactive/SpinnerMini";

export default function LoginForm() {
  const [username, setUsername] = useState("Bob");
  const [password, setPassword] = useState("Drewno1234");
  const { login, isLoading } = useLogin();

  function handleSubmit(e) {
    e.preventDefault();

    if (!username || !password) {
      return;
    }

    login(
      { username, password },
      {
        onSettled: () => {
          setUsername("");
          setPassword("");
        },
      }
    );
  }

  return (
    <Form onSubmit={handleSubmit}>
      <Heading as="h3">Sign in</Heading>
      <FormRowVertical label="Email address">
        <Input
          type="text"
          id="username"
          placeholder="Enter your username or email address"
          autoComplete="username"
          value={username}
          onChange={(e) => setUsername(e.target.value)}
          disabled={isLoading}
        />
      </FormRowVertical>
      <FormRowVertical label="Password">
        <Input
          type="password"
          id="password"
          placeholder="Enter your password"
          autoComplete="current-password"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
          disabled={isLoading}
        />
      </FormRowVertical>
      <LinkContainer>
        <Link to="/forgotPassword">Forgot Password?</Link>
      </LinkContainer>
      <FormRowVertical>
        <Button size="large" variation="primary" disabled={isLoading}>
          {!isLoading ? "Login" : <SpinnerMini />}
        </Button>
      </FormRowVertical>
    </Form>
  );
}
