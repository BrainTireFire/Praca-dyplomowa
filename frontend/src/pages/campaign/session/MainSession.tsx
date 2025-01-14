import styled from "styled-components";
import SessionLayout from "../../../features/campaigns/session/SessionLayout";
import { useEncounter } from "../../../features/campaigns/hooks/useEncounter";
import Spinner from "../../../ui/interactive/Spinner";
import { useParams } from "react-router-dom";

const Container = styled.div`
  display: grid;
  grid-template-columns: 3fr 1fr;
  grid-template-rows: auto auto auto;
  gap: 10px;
  height: 100vh;
  overflow: auto;
`;

export default function MainSession() {
  const { groupName } = useParams<{ groupName: string }>();
  const { isLoading, encounter } = useEncounter(Number(groupName));

  if (isLoading) {
    return <Spinner />;
  }

  if (!encounter) {
    return <div>There are no encounter</div>;
  }

  return (
    <Container>
      <SessionLayout encounter={encounter} />
    </Container>
  );
}
