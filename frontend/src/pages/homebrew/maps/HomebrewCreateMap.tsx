import styled from "styled-components";
import MapCreatorLayout from "../../../features/homebrew/maps/MapCreatorLayout";

const Container = styled.div`
  display: grid;
  grid-template-columns: 0.7fr 3fr;
  grid-template-rows: auto 1fr;
  gap: 10px;
  height: 100vh;
`;

export default function HomebrewCreateMap() {
  return <Container></Container>;
}
