import React from "react";
import FormContainer from "../../../ui/forms/FormContainer";
import ChangeUserData from "./ChangeUserData";
import styled from "styled-components";
import Statistics from "./Statistics";

const ProfileDashboardStyled = styled.main`
  min-height: 5vh;
  display: grid;
  grid-template-columns: 1fr 1fr; /* Two columns */
  align-items: center;
  gap: 2rem;
`;

const RightSide = styled.div`
  display: grid;
  grid-template-rows: 1fr 1fr; /* Two rows */
  gap: 2rem;
`;

export default function ProfileDashboard() {
  return (
    <ProfileDashboardStyled>
      <FormContainer>
        <ChangeUserData />
      </FormContainer>
      <RightSide>
        <FormContainer>up</FormContainer>
        <FormContainer>
          <Statistics />
        </FormContainer>
      </RightSide>
    </ProfileDashboardStyled>
  );
}
