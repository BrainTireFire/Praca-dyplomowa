import React, { useState } from "react";
import Form from "../../ui/forms/Form";
import Heading from "../../ui/text/Heading";
import FormRowVertical from "../../ui/forms/FormRowVertical";
import Input from "../../ui/forms/Input";
import Button from "../../ui/interactive/Button";
import { useTranslation } from "react-i18next";

export default function PasswordChangedForm() {
  const { t } = useTranslation();
  const [password, setPassword] = useState("");
  const [confirmPassword, setConfirmPassword] = useState("");

  function handleSubmit() {}

  return (
    <Form onSubmit={handleSubmit}>
      <Heading as="h3">{t("account.forms.password.change.header")}</Heading>
      <FormRowVertical label={t("account.forms.login.password.input.label")}>
        <Input
          type="password"
          id="password"
          placeholder={t("account.forms.login.password.input.placeholder")}
          value={password}
          onChange={(e) => setPassword(e.target.value)}
        />
      </FormRowVertical>
      <FormRowVertical
        label={t("account.forms.login.confirm.password.input.label")}
      >
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
      <FormRowVertical>
        <Button size="large" variation="primary">
          {t("account.forms.password.change.button")}
        </Button>
      </FormRowVertical>
    </Form>
  );
}
