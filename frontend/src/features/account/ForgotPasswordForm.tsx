import React, { useState } from "react";
import Form from "../../ui/forms/Form";
import FormRowVertical from "../../ui/forms/FormRowVertical";
import Input from "../../ui/forms/Input";
import Heading from "../../ui/text/Heading";
import Button from "../../ui/interactive/Button";

export default function ForgotPasswordForm() {
  const [email, setEmail] = useState("");

  function handleSubmit() {}

  return (
    <Form onSubmit={handleSubmit}>
      <Heading as="h3">Password recovery</Heading>
      <FormRowVertical label="Email address">
        <Input
          type="email"
          id="email"
          placeholder="Enter your email address"
          autoComplete="username"
          value={email}
          onChange={(e) => setEmail(e.target.value)}
        />
      </FormRowVertical>
      <FormRowVertical>
        <Button size="large" variation="primary">
          Send password recovery link
        </Button>
      </FormRowVertical>
    </Form>
  );
}
