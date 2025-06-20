import styled from "styled-components";
import GlobalStyles from "../styles/GlobalStyles";
import Heading from "./text/Heading";
import Button from "./interactive/Button";
import { useEffect } from "react";

const StyledErrorFallback = styled.main`
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
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
  flex: 0 1 96rem;
  text-align: center;

  @media (max-width: 600px) {
    padding: 3.2rem;
    flex: 0 1 100%;
  }

  & h1 {
    margin-bottom: 1.6rem;
    font-size: 2.4rem;
  }

  & p {
    font-family: "Poppins", sans-serif;
    margin-bottom: 3.2rem;
    color: var(--color-secondary-text);
    font-size: 1.6rem;
  }

  & button {
    font-size: 1.6rem;
  }
`;

type FallbackProps = {
  error: Error;
  resetErrorBoundary: () => void;
};

function ErrorFallback({ error, resetErrorBoundary }: FallbackProps) {
  useEffect(() => {
    // Log the error details for developers
    // console.error("Error:", error.message);
    // console.error("Stack trace:", error.stack);
  }, [error]);

  return (
    <>
      <GlobalStyles />
      <StyledErrorFallback>
        <Box role="alert" aria-live="assertive">
          <Heading as="h1">Something went wrong!</Heading>
          <p>
            We're sorry, but an unexpected error has occurred. Please try again.
          </p>
          <Button size="large" onClick={resetErrorBoundary}>
            Try again
          </Button>
        </Box>
      </StyledErrorFallback>
    </>
  );
}

export default ErrorFallback;
