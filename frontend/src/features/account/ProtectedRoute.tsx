import styled from "styled-components";
import Spinner from "../../ui/interactive/Spinner";
import { useNavigate } from "react-router-dom";
import { useEffect } from "react";
import { useUser } from "./useUser";

const FullPage = styled.div`
  height: 100vh;
  background-color: var(--color-main-background);
  display: flex;
  align-items: center;
  justify-content: center;
`;

function ProtectedRoute({ children }: { children: React.ReactNode }) {
  const navigate = useNavigate();
  const { user, isLoading, error } = useUser();

  useEffect(
    function name() {
      if (user?.isAuthenticated === false && !isLoading) {
        navigate("/login");
      }
    },
    [user, isLoading, navigate]
  );

  if (isLoading || user?.isAuthenticated === null) {
    return (
      <FullPage>
        <Spinner />
      </FullPage>
    );
  }

  if (user?.isAuthenticated) {
    return children;
  }

  return null;
}

export default ProtectedRoute;
