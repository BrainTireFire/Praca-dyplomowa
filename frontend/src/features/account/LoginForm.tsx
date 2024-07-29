import React, { useState } from "react";
import Form from "../../ui/forms/Form";
import FormRowVertical from "../../ui/forms/FormRowVertical";
import Input from "../../ui/forms/Input";
import Heading from "../../ui/text/Heading";
import Button from "../../ui/interactive/Button";
import LinkContainer from "../../ui/containers/LinkContainer";
import Link from "../../ui/links/Link";
import { useLogin } from "./useLogin";
import SpinnerMini from "../../ui/interactive/SpinnerMini";
import { useForm } from "react-hook-form";
import { useTranslation } from "react-i18next";

type LoginFormProps = {
  username: string;
  password: string;
};

export default function LoginForm() {
  const { t } = useTranslation();
  const { login, isLoading } = useLogin();
  const { register, formState, handleSubmit, reset } = useForm<LoginFormProps>({
    defaultValues: {
      username: "Bob",
      password: "Drewno1234",
    },
  });
  const { errors } = formState;

  function onSubmit({ username, password }: LoginFormProps) {
    login(
      {
        username,
        password,
      },
      {
        onSettled: () => {
          reset();
        },
      }
    );
  }

  return (
    <Form onSubmit={handleSubmit(onSubmit)}>
      <Heading as="h3">{t("account.forms.login.header")}</Heading>
      <FormRowVertical
        label={t("account.forms.login.username.input.label")}
        error={errors?.username?.message}
      >
        <Input
          type="text"
          id="username"
          placeholder={t("account.forms.login.username.input.placeholder")}
          {...register("username", {
            required: t("account.form.validation.error.required"),
          })}
          disabled={isLoading}
        />
      </FormRowVertical>
      <FormRowVertical
        label={t("account.forms.login.password.input.label")}
        error={errors?.password?.message}
      >
        <Input
          type="password"
          id="password"
          placeholder={t("account.forms.login.password.input.placeholder")}
          disabled={isLoading}
          {...register("password", {
            required: t("account.form.validation.error.required"),
            minLength: {
              value: 8,
              message: t(
                "account.form.validation.error.password.characters.long"
              ),
            },
          })}
        />
      </FormRowVertical>
      <LinkContainer>
        <Link to="/forgotPassword">
          {t("account.forms.login.forgot.password")}
        </Link>
      </LinkContainer>
      <FormRowVertical>
        <Button size="large" variation="primary" disabled={isLoading}>
          {!isLoading ? t("account.forms.login.button") : <SpinnerMini />}
        </Button>
      </FormRowVertical>
    </Form>
  );
}
