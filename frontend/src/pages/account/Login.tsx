import React from "react";
import styled from "styled-components";
import LoginForm from "../../features/account/LoginForm";
import Heading from "../../ui/text/Heading";
import FormContainer from "../../ui/forms/FormContainer";
import LinkContainer from "../../ui/containers/LinkContainer";
import Link from "../../ui/links/Link";
import { useTranslation } from "react-i18next";

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
  const { t } = useTranslation();

  return (
    <LoginLayout>
      {/* <Logo /> */}
      <Link to="/home">
        <Heading as="h4">{t("main.title.text")}</Heading>
      </Link>

      <FormContainer>
        <LoginForm />
        <LinkContainer variation="center">
          {t("account.login.new.around.here")}{" "}
          <Link to="/register">{t("account.forms.register.header")}</Link>
        </LinkContainer>
      </FormContainer>
    </LoginLayout>
  );
}
