import styled from "styled-components";
import Button from "../../ui/interactive/Button";
import Heading from "../../ui/text/Heading";
import { FaArrowLeftLong } from "react-icons/fa6";
import { useNavigate } from "react-router-dom";

const StyledServiceDown = styled.main`
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

  @media (max-width: 600px) {
    padding: 3.2rem;
    flex: 0 1 100%;
  }

  & p {
    font-family: "Poppins", sans-serif;
    margin-bottom: 3.2rem;
    color: var(--color-secondary-text);
    font-size: 2rem;
  }

  & h1 {
    margin-bottom: 3.2rem;
    font-size: 2.4rem;
  }
`;

const CenteredContainer = styled.div`
  display: flex;
  align-items: center;
  justify-content: center;
  flex-direction: row;
  gap: 0.8rem;
`;

function ServiceDown() {
  const navigate = useNavigate();

  return (
    <StyledServiceDown>
      <Box>
        <Heading as="h4">Service Down</Heading>
        <p>The service is currently unavailable. Please try again later</p>
        <Button
          onClick={() => navigate("/home")}
          size="large"
          variation="primary"
        >
          <CenteredContainer>
            <FaArrowLeftLong /> Go back
          </CenteredContainer>
        </Button>
      </Box>
    </StyledServiceDown>
  );
}

export default ServiceDown;
