import { useNavigate } from "react-router-dom";
import { MouseEvent } from "react";

export function useMoveBack() {
  const navigate = useNavigate();
  return (event: MouseEvent<HTMLButtonElement>) => {
    event.preventDefault();
    navigate(-1);
  };
}
