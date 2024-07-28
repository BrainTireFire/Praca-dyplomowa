import React, { useState } from "react";
import Form from "../../ui/forms/Form";
import FormRowVertical from "../../ui/forms/FormRowVertical";
import Input from "../../ui/forms/Input";
import Heading from "../../ui/text/Heading";
import Button from "../../ui/interactive/Button";
import { useTranslation } from "react-i18next";

export default function ForgotPasswordForm() {
  const { t } = useTranslation();
  const [email, setEmail] = useState("");

  function handleSubmit() {}

  return (
    <Form onSubmit={handleSubmit}>
      <Heading as="h3">{t("account.forms.forgot.password.header")}</Heading>
      <FormRowVertical label={t("account.forms.login.email.input.label")}>
        <Input
          type="email"
          id="email"
          placeholder={t("account.forms.login.email.input.placeholder")}
          autoComplete="username"
          value={email}
          onChange={(e) => setEmail(e.target.value)}
        />
      </FormRowVertical>
      <FormRowVertical>
        <Button size="large" variation="primary">
          {t("account.forms.forgot.password.button")}
        </Button>
      </FormRowVertical>
    </Form>
  );
}
