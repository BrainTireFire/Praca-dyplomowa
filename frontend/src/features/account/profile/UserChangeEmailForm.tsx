import React, { useState } from "react";
import FormRowVertical from "../../../ui/forms/FormRowVertical";
import Input from "../../../ui/forms/Input";
import Button from "../../../ui/interactive/Button";
import Form from "../../../ui/forms/Form";
import styled from "styled-components";
import { useTranslation } from "react-i18next";
import { useUpdateEmail } from "./hooks/useUpdateEmail";

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

export default function UserChangeEmailForm() {
  const { t } = useTranslation();
  const [email, setEmail] = useState("");
  const [confirmEmail, setConfirmEmail] = useState("");
  const [error, setError] = useState("");
  const { updateEmail, isUpdating } = useUpdateEmail();

  function handleSubmit(e) {
    e.preventDefault();

    if (email !== confirmEmail) {
      setError("Emails do not match.");
      return;
    }

    setError("");
    updateEmail({ newEmail: email });
  }

  return (
    <Form onSubmit={handleSubmit}>
      <StyledFormRow>
        <FormRowVertical label={t("account.profile.change.user.email")}>
          <Input
            type="email"
            id="email"
            placeholder={t("account.forms.login.email.input.placeholder")}
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            disabled={isUpdating}
          />
        </FormRowVertical>
      </StyledFormRow>
      <StyledFormRow>
        <FormRowVertical>
          <Input
            type="email"
            id="confirmPassword"
            placeholder={t(
              "account.forms.login.confirm.email.input.placeholder"
            )}
            value={confirmEmail}
            onChange={(e) => setConfirmEmail(e.target.value)}
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
          {t("account.profile.change.user.email")}
        </Button>
      </StyledFormRow>
    </Form>
  );
}
