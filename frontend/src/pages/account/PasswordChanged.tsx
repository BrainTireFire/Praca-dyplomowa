import React from "react";
import styled from "styled-components";
import Heading from "../../ui/text/Heading";
import FormContainer from "../../ui/forms/FormContainer";
import LinkContainer from "../../ui/containers/LinkContainer";
import ForgotPasswordForm from "../../features/account/ForgotPasswordForm";
import Link from "../../ui/links/Link";
import PasswordChangedForm from "../../features/account/PasswordChangedForm";

const PasswordChangedLayout = styled.main`
  min-height: 100vh;
  display: grid;
  grid-template-columns: 48rem;
  align-content: center;
  justify-content: center;
  gap: 3.2rem;
  background-color: var(--color-main-background);
`;

export default function PasswordChanged() {
  return (
    <PasswordChangedLayout>
      <Link to="/home">
        <Heading as="h4">Better then beyond</Heading>
      </Link>
      <FormContainer>
        <PasswordChangedForm />
      </FormContainer>
    </PasswordChangedLayout>
  );
}
