import React, { useState } from "react";
import Form from "../../ui/forms/Form";
import Heading from "../../ui/text/Heading";
import FormRowVertical from "../../ui/forms/FormRowVertical";
import Input from "../../ui/forms/Input";
import Button from "../../ui/Button";

export default function RegisterForm() {
  const [username, setUsername] = useState("");
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [confirmPassword, setConfirmPassword] = useState("");

  function handleSubmit() {}

  return (
    <Form onSubmit={handleSubmit}>
      <Heading as="h3">Sign in</Heading>
      <FormRowVertical label="Username">
        <Input
          type="text"
          id="username"
          placeholder="Enter your username"
          autoComplete="username"
          value={username}
          onChange={(e) => setUsername(e.target.value)}
        />
      </FormRowVertical>
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
          value={password}
          onChange={(e) => setPassword(e.target.value)}
        />
      </FormRowVertical>
      <FormRowVertical label="Confirm Password">
        <Input
          type="password"
          id="confirmPassword"
          placeholder="Enter your confirm password"
          value={confirmPassword}
          onChange={(e) => setConfirmPassword(e.target.value)}
        />
      </FormRowVertical>
      <FormRowVertical>
        <Button size="large" variation="secondary">
          Sign up
        </Button>
      </FormRowVertical>
    </Form>
  );
}
