import styled from "styled-components";
import Spinner from "../../ui/interactive/Spinner";
import { useNavigate } from "react-router-dom";
import { useEffect, useState } from "react";
import { useValidate } from "./useValidate";
import { useQueryClient } from "@tanstack/react-query";

const FullPage = styled.div`
  height: 100vh;
  background-color: var(--color-grey-50);
  display: flex;
  align-items: center;
  justify-content: center;
`;

function ProtectedRoute({ children }) {
  const navigate = useNavigate();
  const queryClient = useQueryClient();
  const user = queryClient.getQueryData(["user"]);
  const [isAuthenticated, setIsAuthenticated] = useState(false);
  const { validate, isLoading } = useValidate();

  useEffect(() => {
    async function check() {
      try {
        if (user && user.isAuthenticated) {
          setIsAuthenticated(true);
        } else {
          const result = await validate();
          setIsAuthenticated(result.isAuthenticated);

          if (!result.isAuthenticated) {
            console.log("Test ");
            navigate("/login");
          }
        }
      } catch (error) {
        console.log("error " + JSON.stringify(error));
        navigate("/login");
      }
    }

    check();
  }, [validate, navigate, user]);

  if (isLoading || isAuthenticated === null) {
    return (
      <FullPage>
        <Spinner />
      </FullPage>
    );
  }

  if (isAuthenticated) {
    return children;
  }

  return null;
}

export default ProtectedRoute;
