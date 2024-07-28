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
  grid-template-columns: 1fr 1fr;
  gap: 3rem;
`;

export default function UserChangesPasswordForm() {
  const { t } = useTranslation();
  const [password, setPassword] = useState("");
  const [confirmPassword, setConfirmPassword] = useState("");

  function handleSubmit() {}

  return (
    <Form onSubmit={handleSubmit}>
      <StyledFormRow>
        <FormRowVertical label={t("account.profile.change.user.password")}>
          <Input
            type="password"
            id="password"
            placeholder={t("account.forms.login.password.input.placeholder")}
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
            placeholder={t(
              "account.forms.login.confirm.password.input.placeholder"
            )}
            value={confirmPassword}
            onChange={(e) => setConfirmPassword(e.target.value)}
          />
        </FormRowVertical>
        <Button size="medium" variation="primary">
          {t("account.profile.change.user.password")}
        </Button>
      </StyledFormRow>
    </Form>
  );
}
