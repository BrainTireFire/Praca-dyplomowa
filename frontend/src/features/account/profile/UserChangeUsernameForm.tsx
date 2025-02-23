import React, { useState } from "react";
import FormRowVertical from "../../../ui/forms/FormRowVertical";
import Input from "../../../ui/forms/Input";
import Button from "../../../ui/interactive/Button";
import Form from "../../../ui/forms/Form";
import styled from "styled-components";
import { useTranslation } from "react-i18next";
import { useUpdateUsername } from "./hooks/useUpdateUsername";

const StyledFormRow = styled.div`
  display: grid;
  align-items: center;
  justify-content: center;
  grid-template-columns: 1fr 1fr;
  gap: 3rem;
`;

export default function UserChangeUsernameForm() {
  const { t } = useTranslation();
  const [username, setUsername] = useState("");
  const { updateUsername, isUpdating } = useUpdateUsername();

  function handleSubmit(e) {
    e.preventDefault();
    if (!username) {
      return;
    }

    updateUsername({ newUsername: username });
  }

  return (
    <Form onSubmit={handleSubmit}>
      <StyledFormRow>
        <FormRowVertical label={t("account.profile.change.user.username")}>
          <Input
            type="text"
            id="username"
            placeholder={t("account.forms.login.username.input.placeholder")}
            value={username}
            onChange={(e) => setUsername(e.target.value)}
            disabled={isUpdating}
          />
        </FormRowVertical>
        <Button
          size="medium"
          variation="primary"
          onClick={handleSubmit}
          disabled={isUpdating}
        >
          {t("account.profile.change.user.username")}
        </Button>
      </StyledFormRow>
    </Form>
  );
}
