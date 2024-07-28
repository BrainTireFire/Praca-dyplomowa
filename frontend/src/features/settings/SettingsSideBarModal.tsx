import styled from "styled-components";
import Heading from "../../ui/text/Heading";
import { useTranslation } from "react-i18next";
import ToggleSwitch from "../../ui/interactive/ToggleSwitch";

const Container = styled.div`
  display: flex;
  flex-direction: column;
  align-items: center;
  align-content: center;
  gap: 12px;
`;

const Select = styled.select`
  padding: 8px 12px;
  border: 1px solid var(--color-border);
  border-radius: 4px;
  background-color: var(--color-main-background);
  color: var(--color-secondary-text);
  font-size: 16px;
  outline: none;
  transition: border-color 0.3s, box-shadow 0.3s;

  &:focus {
    border-color: var(--color-input-focus);
    box-shadow: 0 0 5px var(--color-input-focus);
  }

  option {
    background-color: var(--color-main-background);
    color: var(--color-secondary-text);
  }
`;

const StyledRow = styled.div`
  display: flex;
  align-items: center;
  gap: 12px;
`;

const StyledContainer = styled.div`
  display: flex;
  flex-direction: column;
  gap: 15px;
`;

function SettingsSideBarModal() {
  const { i18n } = useTranslation();
  const { t } = useTranslation();

  const changeLanguage = (event) => {
    const selectedLanguage = event.target.value;
    i18n.changeLanguage(selectedLanguage);
  };

  return (
    <Container>
      <Heading as="h1">{t("main.sidebar.settings.text")}</Heading>
      <StyledContainer>
        <StyledRow>
          <span>{t("main.navbar.link.language")}: </span>
          <Select onChange={changeLanguage} defaultValue={i18n.language}>
            <option value="en">{t("main.navbar.link.english")}</option>
            <option value="de">{t("main.navbar.link.german")}</option>
            <option value="es">{t("main.navbar.link.spanish")}</option>
            <option value="fr">{t("main.navbar.link.france")}</option>
            <option value="jp">{t("main.navbar.link.japan")}</option>
          </Select>
        </StyledRow>
        <StyledRow>
          <span>{t("main.sidebar.settings.theme.mode")}: </span>
          <ToggleSwitch />
        </StyledRow>
      </StyledContainer>
    </Container>
  );
}

export default SettingsSideBarModal;
