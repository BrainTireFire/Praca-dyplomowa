import styled from "styled-components";
import SessionLayout from "../../../features/campaigns/session/SessionLayout";
import { useEncounter } from "../../../features/campaigns/hooks/useEncounter";
import Spinner from "../../../ui/interactive/Spinner";

const Container = styled.div`
  display: grid;
  grid-template-columns: 3fr 1fr;
  grid-template-rows: auto 1fr;
  gap: 10px;
  height: 100vh;
`;

export default function MainSession() {
  const { isLoading, encounter } = useEncounter(89);

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
