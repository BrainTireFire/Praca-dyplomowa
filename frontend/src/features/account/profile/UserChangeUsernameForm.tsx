import React, { useState } from "react";
import FormRowVertical from "../../../ui/forms/FormRowVertical";
import Input from "../../../ui/forms/Input";
import Button from "../../../ui/interactive/Button";
import Form from "../../../ui/forms/Form";
import styled from "styled-components";

const StyledFormRow = styled.div`
  display: grid;
  align-items: center;
  justify-content: center;
  grid-template-columns: 1fr 1fr;
  gap: 3rem;
`;

export default function UserChangeUsernameForm() {
  const [username, setUsername] = useState("");

  function handleSubmit() {}

  return (
    <Form onSubmit={handleSubmit}>
      <StyledFormRow>
        <FormRowVertical label="Change Username">
          <Input
            type="text"
            id="username"
            placeholder="Enter your username"
            value={username}
            onChange={(e) => setUsername(e.target.value)}
          />
        </FormRowVertical>
        <Button size="medium" variation="primary">
          Change Username
        </Button>
      </StyledFormRow>
    </Form>
  );
}
