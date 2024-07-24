import React, { useState } from "react";
import Form from "../../ui/forms/Form";
import Heading from "../../ui/text/Heading";
import FormRowVertical from "../../ui/forms/FormRowVertical";
import Input from "../../ui/forms/Input";
import Button from "../../ui/interactive/Button";
import { useForm } from "react-hook-form";
import SpinnerMini from "../../ui/interactive/SpinnerMini";
import { useSignup } from "./useSignup";
import { useTranslation } from "react-i18next";

type RegisterFormProps = {
  username: string;
  email: string;
  password: string;
  confirmPassword?: string;
};

export default function RegisterForm() {
  const { t } = useTranslation();
  const { signup, isLoading } = useSignup();
  const { register, formState, getValues, handleSubmit, reset } =
    useForm<RegisterFormProps>();
  const { errors } = formState;

  function onSubmit({ username, email, password }: RegisterFormProps) {
    signup(
      {
        username,
        email,
        password,
      },
      {
        onSettled: () => reset(),
      }
    );
  }

  return (
    <Form onSubmit={handleSubmit(onSubmit)}>
      <Heading as="h3">{t("account.forms.register.header")}</Heading>
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
        label={t("account.forms.login.email.input.label")}
        error={errors?.email?.message}
      >
        <Input
          type="email"
          id="email"
          disabled={isLoading}
          placeholder={t("account.forms.login.email.input.placeholder")}
          {...register("email", {
            required: t("account.form.validation.error.required"),
            pattern: {
              value: /\S+@\S+\.\S+/,
              message: t("account.form.validation.error.email.valid"),
            },
          })}
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
      <FormRowVertical
        label={t("account.forms.login.confirm.password.input.label")}
        error={errors?.confirmPassword?.message}
      >
        <Input
          type="password"
          id="confirmPassword"
          placeholder={t(
            "account.forms.login.confirm.password.input.placeholder"
          )}
          disabled={isLoading}
          {...register("confirmPassword", {
            required: t("account.form.validation.error.required"),
            validate: (value) =>
              value === getValues().password ||
              t("account.form.validation.error.password.match"),
          })}
        />
      </FormRowVertical>
      <FormRowVertical>
        <Button size="large" variation="primary">
          {!isLoading ? t("account.forms.register.button") : <SpinnerMini />}
        </Button>
      </FormRowVertical>
    </Form>
  );
}
