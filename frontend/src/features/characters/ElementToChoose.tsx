import styled from "styled-components";
import Box from "../../ui/containers/Box";
import Heading from "../../ui/text/Heading";
import { Effect, Power, Resource } from "../../services/apiCharacters";
import Line from "../../ui/separators/Line";

const Container = styled(Box)`
  display: flex;
  flex-direction: column;
`;

export default function ElementToChoose({
  element,
}: {
  element: Effect | Power | Resource;
}) {
  return (
    <Container>
      <Heading>{element.name}</Heading>
      <Line size="percantage"></Line>
      <span>
        {(element as Resource).level
          ? (element as Resource).amount +
            " x " +
            (element as Resource).level +
            " level"
          : (element as Effect).description}
      </span>
    </Container>
  );
}
