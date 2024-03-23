import React, { useState } from "react";
import Form from "../../ui/forms/Form";
import FormRowVertical from "../../ui/forms/FormRowVertical";
import Input from "../../ui/forms/Input";
import Heading from "../../ui/text/Heading";
import Button from "../../ui/interactive/Button";
import LinkContainer from "../../ui/containers/LinkContainer";
import Link from "../../ui/links/Link";

export default function LoginForm() {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");

  function handleSubmit() {}

  return (
    <Form onSubmit={handleSubmit}>
      <Heading as="h3">Sign in</Heading>
      <FormRowVertical label="Email address">
        <Input
          type="email"
          id="email"
          placeholder="Enter your username or email address"
          autoComplete="username"
          value={email}
          onChange={(e) => setEmail(e.target.value)}
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
        />
      </FormRowVertical>
      <LinkContainer>
        <Link to="/forgotPassword">Forgot Password?</Link>
      </LinkContainer>
      <FormRowVertical>
        <Button size="large" variation="primary">
          Login
        </Button>
      </FormRowVertical>
    </Form>
  );
}
