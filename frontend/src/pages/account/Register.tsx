import React from "react";
import styled from "styled-components";
import Heading from "../../ui/text/Heading";
import FormContainer from "../../ui/forms/FormContainer";
import LinkContainer from "../../ui/containers/LinkContainer";
import Link from "../../ui/links/Link";
import RegisterForm from "../../features/account/RegisterForm";

const RegisterLayout = styled.main`
  min-height: 100vh;
  display: grid;
  grid-template-columns: 48rem;
  align-content: center;
  justify-content: center;
  gap: 3.2rem;
  background-color: var(--color-main-background);
`;

export default function Register() {
  return (
    <RegisterLayout>
      {/* <Logo /> */}
      <Link to="/home">
        <Heading as="h4">Better then beyond</Heading>
      </Link>
      <FormContainer>
        <RegisterForm />
        <LinkContainer variation="center">
          Already have an account? <Link to="/login">Log in</Link>
        </LinkContainer>
      </FormContainer>
    </RegisterLayout>
  );
}