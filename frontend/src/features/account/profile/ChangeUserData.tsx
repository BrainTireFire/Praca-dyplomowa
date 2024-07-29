import styled from "styled-components";
import UserChangeEmailForm from "./UserChangeEmailForm";
import UserChangesPasswordForm from "./UserChangesPasswordForm";
import UserChangeUsernameForm from "./UserChangeUsernameForm";
import Heading from "../../../ui/text/Heading";
import Button from "../../../ui/interactive/Button";
import { useTranslation } from "react-i18next";

const StyledChangeUserData = styled.div`
  display: grid;
  align-items: center;
  justify-content: center;
  grid-template-columns: 1fr;
  gap: 3rem;
`;

export default function ChangeUserData() {
  const { t } = useTranslation();

  return (
    <StyledChangeUserData>
      <UserChangeUsernameForm />
      <UserChangesPasswordForm />
      <UserChangeEmailForm />
      <Heading>{t("account.profile.change.user.data")}</Heading>
      <Button size="medium" variation="danger">
        {t("account.profile.remove.account")}
      </Button>
    </StyledChangeUserData>
  );
}
