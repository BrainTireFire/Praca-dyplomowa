import React from "react";
import styled from "styled-components";
import Heading from "../../ui/text/Heading";
import FormContainer from "../../ui/forms/FormContainer";
import LinkContainer from "../../ui/containers/LinkContainer";
import Link from "../../ui/links/Link";
import RegisterForm from "../../features/account/RegisterForm";
import { useTranslation } from "react-i18next";

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
  const { t } = useTranslation();

  return (
    <RegisterLayout>
      {/* <Logo /> */}
      <Link to="/home">
        <Heading as="h4">{t("main.title.text")}</Heading>
      </Link>
      <FormContainer>
        <RegisterForm />
        <LinkContainer variation="center">
          {t("account.forms.register.already.have.an.account")}{" "}
          <Link to="/login">{t("account.forms.register.login.button")}</Link>
        </LinkContainer>
      </FormContainer>
    </RegisterLayout>
  );
}
