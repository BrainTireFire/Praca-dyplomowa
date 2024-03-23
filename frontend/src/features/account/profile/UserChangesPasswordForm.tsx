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

export default function UserChangesPasswordForm() {
  const [password, setPassword] = useState("");
  const [confirmPassword, setConfirmPassword] = useState("");

  function handleSubmit() {}

  return (
    <Form onSubmit={handleSubmit}>
      <StyledFormRow>
        <FormRowVertical label="Change Password">
          <Input
            type="password"
            id="password"
            placeholder="Enter your password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
          />
        </FormRowVertical>
      </StyledFormRow>
      <StyledFormRow>
        <FormRowVertical>
          <Input
            type="password"
            id="confirmPassword"
            placeholder="Enter your confirm password"
            value={confirmPassword}
            onChange={(e) => setConfirmPassword(e.target.value)}
          />
        </FormRowVertical>
        <Button size="medium" variation="primary">
          Change Password
        </Button>
      </StyledFormRow>
    </Form>
  );
}
