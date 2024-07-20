import React, { useState } from "react";
import FormRowVertical from "../../../ui/forms/FormRowVertical";
import Input from "../../../ui/forms/Input";
import Button from "../../../ui/interactive/Button";
import Form from "../../../ui/forms/Form";
import styled from "styled-components";
import { useTranslation } from "react-i18next";

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

  function handleSubmit() {}

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
          />
        </FormRowVertical>
        <Button size="medium" variation="primary">
          {t("account.profile.change.user.username")}
        </Button>
      </StyledFormRow>
    </Form>
  );
}
