import styled from "styled-components";
import UserChangeEmailForm from "./UserChangeEmailForm";
import UserChangesPasswordForm from "./UserChangesPasswordForm";
import UserChangeUsernameForm from "./UserChangeUsernameForm";
import Heading from "../../../ui/text/Heading";
import Button from "../../../ui/interactive/Button";

const StyledChangeUserData = styled.div`
  display: grid;
  align-items: center;
  justify-content: center;
  grid-template-columns: 1fr;
  gap: 3rem;
`;

export default function ChangeUserData() {
  return (
    <StyledChangeUserData>
      <UserChangeUsernameForm />
      <UserChangesPasswordForm />
      <UserChangeEmailForm />
      <Heading>Permanently remove the account and all data</Heading>
      <Button size="medium" variation="danger">
        Remove account
      </Button>
    </StyledChangeUserData>
  );
}
