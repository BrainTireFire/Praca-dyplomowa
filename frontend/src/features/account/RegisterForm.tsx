import React, { useState } from "react";
import Form from "../../ui/forms/Form";
import Heading from "../../ui/text/Heading";
import FormRowVertical from "../../ui/forms/FormRowVertical";
import Input from "../../ui/forms/Input";
import Button from "../../ui/interactive/Button";
import { useForm } from "react-hook-form";
import SpinnerMini from "../../ui/interactive/SpinnerMini";
import { useSignup } from "./useSignup";

type RegisterFormProps = {
  username: string;
  email: string;
  password: string;
};

export default function RegisterForm() {
  const { signup, isLoading } = useSignup();
  const { register, formState, getValues, handleSubmit, reset } = useForm();
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
      <Heading as="h3">Sign in</Heading>
      <FormRowVertical label="Username" error={errors?.username?.message}>
        <Input
          type="text"
          id="username"
          placeholder="Enter your username"
          {...register("username", { required: "This field is requierd" })}
          disabled={isLoading}
        />
      </FormRowVertical>
      <FormRowVertical label="Email address" error={errors?.email?.message}>
        <Input
          type="email"
          id="email"
          disabled={isLoading}
          placeholder="Enter your username or email address"
          {...register(
            "email",
            { required: "This field is requierd" },
            {
              pattern: {
                value: /\S+@\S+\.\S+/,
                message: "Please provide a valid email address ",
              },
            }
          )}
        />
      </FormRowVertical>
      <FormRowVertical label="Password" error={errors?.password?.message}>
        <Input
          type="password"
          id="password"
          placeholder="Enter your password"
          disabled={isLoading}
          {...register("password", {
            required: "This field is requierd",
            minLength: {
              value: 8,
              message: "Password must be at least 8 characters long",
            },
          })}
        />
      </FormRowVertical>
      <FormRowVertical
        label="Confirm Password"
        error={errors?.confirmPassword?.message}
      >
        <Input
          type="password"
          id="confirmPassword"
          placeholder="Enter your confirm password"
          disabled={isLoading}
          {...register("confirmPassword", {
            required: "This field is requierd",
            validate: (value) =>
              value === getValues().password || "Password need to match",
          })}
        />
      </FormRowVertical>
      <FormRowVertical>
        <Button size="large" variation="primary">
          {!isLoading ? "Sign up" : <SpinnerMini />}
        </Button>
      </FormRowVertical>
    </Form>
  );
}
