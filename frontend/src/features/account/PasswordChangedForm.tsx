import React, { useState } from "react";
import Form from "../../ui/forms/Form";
import Heading from "../../ui/text/Heading";
import FormRowVertical from "../../ui/forms/FormRowVertical";
import Input from "../../ui/forms/Input";
import Button from "../../ui/Button";

export default function PasswordChangedForm() {
  const [password, setPassword] = useState("");
  const [confirmPassword, setConfirmPassword] = useState("");

  function handleSubmit() {}

  return (
    <Form onSubmit={handleSubmit}>
      <Heading as="h3">Password recovery</Heading>
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
          Reset password
        </Button>
      </FormRowVertical>
    </Form>
  );
}
