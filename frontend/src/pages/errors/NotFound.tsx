import styled from "styled-components";
import { useMoveBack } from "../../hooks/useMoveBack";
import Button from "../../ui/interactive/Button";
import Heading from "../../ui/text/Heading";
import { FaArrowLeftLong } from "react-icons/fa6";

const StyledPageNotFound = styled.main`
  height: 100vh;
  background-color: var(--color-main-background);
  display: flex;
  align-items: center;
  justify-content: center;
  padding: 4.8rem;
`;

const Box = styled.div`
  background-color: var(--color-navbar);
  border: 1px solid var(--color-border);
  border-radius: var(--border-radius-md);

  padding: 4.8rem;
  flex: 0 1 96rem;
  text-align: center;

  & p {
    font-family: "Poppins", sans-serif;
    margin-bottom: 3.2rem;
    color: var(--color-secondary-text);
    font-size: 2rem;
  }

  & h1 {
    margin-bottom: 3.2rem;
  }
`;

const CenteredContainer = styled.div`
  display: flex;
  align-items: center;
  justify-content: center;
  flex-direction: row;
  gap: 0.8rem;
`;

function NotFound() {
  const moveBack = useMoveBack();

  return (
    <StyledPageNotFound>
      <Box>
        <Heading as="h4">Not found</Heading>
        <p>The page you are looking for could not be found 😢</p>
        <Button onClick={moveBack} size="large" variation="primary">
          <CenteredContainer>
            <FaArrowLeftLong /> Go back
          </CenteredContainer>
        </Button>
      </Box>
    </StyledPageNotFound>
  );
}

export default NotFound;
