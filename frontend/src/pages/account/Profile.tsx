import React from "react";
import styled from "styled-components";
import Heading from "../../ui/text/Heading";
import Line from "../../ui/separators/Line";
import ChangeUserDataForm from "../../features/account/profile/ChangeUserData";
import FormContainer from "../../ui/forms/FormContainer";
import ProfileDashboard from "../../features/account/profile/ProfileDashboard";
import { useTranslation } from "react-i18next";

const ProfileLayout = styled.main`
  display: grid;
  gap: 2rem;
  align-content: center;
  justify-content: center;
  padding: 2rem;
  margin: 2rem;
`;

export default function Profile() {
  const { t } = useTranslation();

  return (
    <ProfileLayout>
      <div>
        <Heading as="h4">{t("account.profile.myprofile.header")}</Heading>
        <Line size="large" />
      </div>
      <ProfileDashboard />
    </ProfileLayout>
  );
}
