import React from "react";
import styled from "styled-components";
import Heading from "../../ui/text/Heading";
import Line from "../../ui/separators/Line";
import ChangeUserDataForm from "../../features/account/profile/ChangeUserData";
import FormContainer from "../../ui/forms/FormContainer";
import ProfileDashboard from "../../features/account/profile/ProfileDashboard";

const ProfileLayout = styled.main`
  display: grid;
  gap: 2rem;
  align-content: center;
  justify-content: center;
  padding: 2rem;
  margin: 2rem;
`;

export default function Profile() {
  return (
    <ProfileLayout>
      <div>
        <Heading as="h4">My profile</Heading>
        <Line size="large" />
      </div>
      <ProfileDashboard />
    </ProfileLayout>
  );
}
