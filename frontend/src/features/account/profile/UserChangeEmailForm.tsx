import React, { useState } from "react";
import FormRowVertical from "../../../ui/forms/FormRowVertical";
import Input from "../../../ui/forms/Input";
import Button from "../../../ui/interactive/Button";
import Form from "../../../ui/forms/Form";
import styled from "styled-components";

const StyledFormRow = styled.div`
  display: grid;
  align-items: center;
  grid-template-columns: 1fr 1fr;
  gap: 3rem;
`;

export default function UserChangeEmailForm() {
  const [email, setEmail] = useState("");
  const [confirmEmail, setConfirmEmail] = useState("");

  function handleSubmit() {}

  return (
    <Form onSubmit={handleSubmit}>
      <StyledFormRow>
        <FormRowVertical label="Change Email">
          <Input
            type="email"
            id="email"
            placeholder="Enter your email"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
          />
        </FormRowVertical>
      </StyledFormRow>
      <StyledFormRow>
        <FormRowVertical>
          <Input
            type="email"
            id="confirmPassword"
            placeholder="Enter your confirm email"
            value={confirmEmail}
            onChange={(e) => setConfirmEmail(e.target.value)}
          />
        </FormRowVertical>
        <Button size="medium" variation="primary">
          Change E-mail
        </Button>
      </StyledFormRow>
    </Form>
  );
}
