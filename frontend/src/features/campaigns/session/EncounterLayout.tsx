import styled from "styled-components";
import { FaMap } from "react-icons/fa";
import Spinner from "../../../ui/interactive/Spinner";
import { useEncounters } from "../hooks/useEncounters";
import Heading from "../../../ui/text/Heading";
import Button from "../../../ui/interactive/Button";
import { useEffect, useState } from "react";
import { Encounter } from "../../../models/encounter/Encounter";
import { useNavigate, useParams } from "react-router-dom";
import EncounterMapCreaterLayout from "./EncounterMapCreaterLayout";
import { useQuery } from "@tanstack/react-query";
import { useEncounter } from "../hooks/useEncounter";

const Container = styled.div`
  display: flex;
  flex-direction: column;
  align-items: center;
  gap: 20px;
  height: 100vh;
  padding: 20px;
  overflow-y: auto;
`;

const BoxStyled = styled.div<{ isSelected: boolean }>`
  background-color: ${({ isSelected }) =>
    isSelected ? "var(--color-button-primary)" : "var(--color-navbar)"};
  border: ${({ isSelected }) =>
    isSelected
      ? "2px solid var(--color-button-green)"
      : "1px solid var(--color-navbar)"};
  border-radius: 8px;
  padding: 20px;
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  align-items: center;
  width: 50%;
  box-shadow: var(--shadow-md);
  transition: transform 0.3s ease, box-shadow 0.3s ease;

  &:hover {
    transform: translateY(-5px);
    background-color: ${({ isSelected }) =>
      isSelected ? "" : "var(--color-navbar-hover)"};
    box-shadow: var(--shadow-lg);
  }
`;

const Section = styled.div`
  display: flex;
  flex-direction: rows;
  justify-content: space-between;
  gap: 10px;
`;

const LeftSection = styled.div`
  display: flex;
  align-items: center;
  gap: 15px;

  .icon {
    font-size: 2rem;
    color: var(--color-primary);
  }

  .details {
    display: flex;
    flex-direction: column;

    h3 {
      margin: 0;
      font-size: 1.5rem;
      color: var(--color-primary);
    }

    p {
      margin: 2px 0;
      font-size: 1rem;
      color: var(--color-text);
    }
  }
`;

const RightSection = styled.div`
  /* display: flex;
  align-items: center;
  justify-content: center;
  width: 70px;
  height: 100%;
  color: var(--color-white);
  cursor: pointer;
  transition: background-color 0.3s ease; */
`;

const CenterSection = styled.div`
  .participants {
    font-size: 0.9rem;
    color: var(--color-secondary-text);
    margin-top: 10px;

    p {
      font-size: 1rem;
      font-weight: bold;
      margin-bottom: 5px;
    }

    ul {
      list-style: none;
      padding: 0;
      margin: 0;
      font-size: 0.9rem;
      display: flex;
      flex-wrap: wrap;
      gap: 10px;
    }

    li {
      background-color: var(--color-primary-light);
      color: var(--color-primary);
      padding: 5px 10px;
      border-radius: 5px;
      font-size: 0.85rem;
      white-space: nowrap;
      box-shadow: var(--shadow-sm);
    }
  }
`;

const ContentSection = styled.div`
  flex: 1;
  display: flex;
  flex-direction: column;
  gap: 5px;
`;

const ActionButtonWrapper = styled.div`
  margin-top: 15px; /* Adds spacing between content and button */
`;

const HeaderStyled = styled.header``;

const ButtonStyled = styled.button`
  background-color: var(--color-button-hover-primary);
  color: var(--color-white);
  padding: 10px 20px;
  width: 100%;
  border: none;
  border-radius: 5px;
  cursor: pointer;
  transition: background-color 0.3s ease;

  &:hover {
    background-color: var(--color-button-hover-danger);
  }
`;

const MoreDetailsWrapper = styled.div`
  display: flex;
  justify-content: space-between;
  flex-direction: rows;
  background-color: var(--color-light-background);
  padding: 15px;
  border: 1px solid var(--color-border);
  border-radius: 8px;
  margin-top: 10px;
  box-shadow: var(--shadow-sm);
`;

const DetailSection = styled.div`
  margin-bottom: 15px;

  h4 {
    font-size: 1.2rem;
    color: var(--color-primary);
    margin-bottom: 8px;
  }

  p {
    font-size: 1rem;
    color: var(--color-text);
    margin: 2px 0;
  }
`;

const CharacterList = styled.ul`
  list-style: none;
  padding: 0;
  margin: 0;

  li {
    background-color: var(--color-primary-light);
    color: var(--color-primary);
    padding: 5px 10px;
    border-radius: 5px;
    font-size: 0.9rem;
    margin-bottom: 5px;
    box-shadow: var(--shadow-sm);
  }
`;

export default function EncounterLayout() {
  const navigate = useNavigate();
  const { campaignId } = useParams<{ campaignId: string }>();
  const [selectedEncounter, setSelectedEncounter] = useState<Encounter>();
  const [toggleMap, setToggleMap] = useState(false);
  const [detailsToggled, setDetailsToggled] = useState<Map<number, boolean>>(
    new Map()
  );
  const { isLoading, encounters } = useEncounters(campaignId);

  useEffect(() => {
    if (!isLoading && encounters && encounters.length > 0) {
      const activeEncounter = encounters.find(
        (encounter) => encounter.isActive
      );

      if (activeEncounter && activeEncounter.campaign?.id) {
        navigate(
          `/campaigns/${activeEncounter.campaign.id}/session/${activeEncounter.id}`
        );
      }
    }
  }, [encounters, isLoading, navigate]);

  if (isLoading) {
    return <Spinner />;
  }

  if (!encounters || encounters.length === 0) {
    return <div>There are no encounters</div>;
  }

  if (toggleMap) {
    return (
      <EncounterMapCreaterLayout
        encounterId={selectedEncounter?.id}
        onToggle={setToggleMap}
        startEncounter={true}
      />
    );
  }

  const handleSelectEncounter = (encounterId: number) => {
    const encounter = encounters.find((enc) => enc.id === encounterId);
    if (encounter) {
      setSelectedEncounter(encounter);
    }
  };

  const handleMoreDetails = (
    encounterId: number,
    e: React.MouseEvent<HTMLButtonElement>
  ) => {
    e.stopPropagation();
    setDetailsToggled((prev) => {
      const newMap = new Map(prev);
      newMap.set(encounterId, !newMap.get(encounterId));
      return newMap;
    });
  };

  return (
    <Container>
      <Heading as="h2">Select the encounter</Heading>
      {encounters.map((encounter) => {
        const isDetailsToggled = detailsToggled.get(encounter.id) || false;

        return (
          <BoxStyled
            isSelected={selectedEncounter?.id === encounter.id}
            key={encounter.id}
            onClick={() => handleSelectEncounter(encounter.id)}
          >
            <ContentSection>
              <Section>
                <LeftSection>
                  <FaMap className="icon" />
                  <div className="details">
                    <HeaderStyled>{encounter.name}</HeaderStyled>
                    <p>
                      Board: <strong>{encounter.board.name}</strong>
                    </p>
                    <p>
                      Campaign: <strong>{encounter.campaign.name}</strong>
                    </p>
                  </div>
                </LeftSection>
                <CenterSection></CenterSection>
                <RightSection>
                  <ButtonStyled
                    onClick={(e) => handleMoreDetails(encounter.id, e)}
                  >
                    {isDetailsToggled ? "Hide" : "More Details"}
                  </ButtonStyled>
                </RightSection>
              </Section>
              <ActionButtonWrapper>
                {isDetailsToggled && (
                  <MoreDetailsWrapper>
                    <DetailSection>
                      <HeaderStyled>Board:</HeaderStyled>
                      <p>Name: {encounter.board.name}</p>
                      <p>Description: {encounter.board.description}</p>
                      <p>
                        Size: {encounter.board.sizeX} x {encounter.board.sizeY}
                      </p>
                    </DetailSection>
                    <DetailSection>
                      <HeaderStyled>Campaign:</HeaderStyled>
                      <p>Name: {encounter.campaign.name}</p>
                      <p>Description: {encounter.campaign.description}</p>
                    </DetailSection>
                    <DetailSection>
                      <HeaderStyled>Members:</HeaderStyled>
                      <CharacterList>
                        {encounter.campaign.members.length > 0
                          ? encounter.campaign.members.map((members) => (
                              <p key={members.id}>{members.name}</p>
                            ))
                          : "No members"}
                      </CharacterList>
                    </DetailSection>
                    <DetailSection>
                      <HeaderStyled>Npc:</HeaderStyled>
                      <CharacterList>
                        {encounter.participances.length > 0
                          ? encounter.participances.map((participant) => (
                              <p key={participant.id}>
                                {participant.character.name}
                              </p>
                            ))
                          : "No npc"}
                      </CharacterList>
                    </DetailSection>
                  </MoreDetailsWrapper>
                )}
              </ActionButtonWrapper>
            </ContentSection>
          </BoxStyled>
        );
      })}
      <Button
        size="large"
        disabled={!selectedEncounter}
        onClick={() => setToggleMap(!toggleMap)}
      >
        Continue
      </Button>
    </Container>
  );
}
