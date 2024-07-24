import React from "react";
import Heading from "../../../ui/text/Heading";
import styled from "styled-components";
import { useTranslation } from "react-i18next";

const StyledStatistics = styled.div`
  display: grid;
  align-items: center;
  justify-content: center;
  grid-template-columns: 1fr;
  gap: 3rem;
  margin-top: 3rem;
`;

const StyledStatisticsRow = styled.div`
  display: grid;
  align-items: center;
  justify-content: center;
  grid-template-columns: 1fr 1fr;
  gap: 7rem;
  margin-left: 5rem;
`;

export default function Statistics() {
  const { t } = useTranslation();

  return (
    <>
      <Heading as="h4">{t("account.profile.statistics.header")}</Heading>
      <StyledStatistics>
        <StyledStatisticsRow>
          <Heading as="h3" align="left">
            {t("account.profile.statistics.games.played")}
          </Heading>
          3000
        </StyledStatisticsRow>

        <StyledStatisticsRow>
          <Heading as="h3" align="left">
            {t("account.profile.statistics.games.won")}
          </Heading>
          200
        </StyledStatisticsRow>

        <StyledStatisticsRow>
          <Heading as="h3" align="left">
            {t("account.profile.statistics.account.created")}
          </Heading>
          1000
        </StyledStatisticsRow>
      </StyledStatistics>
    </>
  );
}
