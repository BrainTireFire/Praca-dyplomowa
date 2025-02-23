import React, { useState } from "react";
import FormRowVertical from "../../../ui/forms/FormRowVertical";
import Input from "../../../ui/forms/Input";
import Button from "../../../ui/interactive/Button";
import Form from "../../../ui/forms/Form";
import styled from "styled-components";
import { useTranslation } from "react-i18next";
import { useUpdatePassword } from "./hooks/useUpdatePassword";

const StyledFormRow = styled.div`
  display: grid;
  align-items: center;
  grid-template-columns: 1fr 1fr;
  gap: 3rem;
`;

const ErrorText = styled.p`
  color: var(--color-button-danger);
  font-size: 14px;
  margin-top: 5px;
`;

export default function UserChangesPasswordForm() {
  const { t } = useTranslation();
  const [password, setPassword] = useState("");
  const [confirmPassword, setConfirmPassword] = useState("");
  const [error, setError] = useState("");
  const { updatePassword, isUpdating } = useUpdatePassword();

  function handleSubmit(e) {
    e.preventDefault();

    if (password !== confirmPassword) {
      setError("Passwords do not match.");
      return;
    }

    setError("");
    updatePassword({ newPassword: password });
  }

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
            disabled={isUpdating}
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
            disabled={isUpdating}
          />
          {error && <ErrorText>{error}</ErrorText>}
        </FormRowVertical>
        <Button
          size="medium"
          variation="primary"
          onClick={handleSubmit}
          disabled={isUpdating}
        >
          {t("account.profile.change.user.password")}
        </Button>
      </StyledFormRow>
    </Form>
  );
}
