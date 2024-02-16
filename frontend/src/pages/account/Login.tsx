import React from "react";
import styled from "styled-components";
import LoginForm from "../../features/account/LoginForm";
import Heading from "../../ui/text/Heading";
import FormContainer from "../../ui/forms/FormContainer";
import LinkContainer from "../../ui/containers/LinkContainer";
import Link from "../../ui/links/Link";

const LoginLayout = styled.main`
  min-height: 100vh;
  display: grid;
  grid-template-columns: 48rem;
  align-content: center;
  justify-content: center;
  gap: 3.2rem;
  background-color: var(--color-main-background);
`;

export default function Login() {
  return (
    <LoginLayout>
      {/* <Logo /> */}
      <Link to="/home">
        <Heading as="h4">Better then beyond</Heading>
      </Link>

      <FormContainer>
        <LoginForm />
        <LinkContainer variation="center">
          New around here? <Link to="/register">Sign up</Link>
        </LinkContainer>
      </FormContainer>
    </LoginLayout>
  );
}
