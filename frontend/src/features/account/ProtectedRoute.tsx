import styled from "styled-components";
import Spinner from "../../ui/interactive/Spinner";
import { useNavigate } from "react-router-dom";
import { useEffect, useState } from "react";
import { useValidate } from "./useValidate";

const FullPage = styled.div`
  height: 100vh;
  background-color: var(--color-grey-50);
  display: flex;
  align-items: center;
  justify-content: center;
`;

function ProtectedRoute({ children }) {
  const navigate = useNavigate();
  const [isValid, setIsValid] = useState(false);
  const { validate, isLoading } = useValidate();

  useEffect(() => {
    async function check() {
      try {
        const result = await validate();
        setIsValid(result.isValid);
        if (!result.isValid) {
          navigate("/login");
        }
      } catch (error) {
        navigate("/login");
      }
    }

    check();
  }, [validate, navigate]);

  if (isLoading || isValid === null) {
    return (
      <FullPage>
        <Spinner />
      </FullPage>
    );
  }

  if (isValid) {
    return children;
  }

  return null;
}

export default ProtectedRoute;
