import styled from "styled-components";
import Box from "../../ui/containers/Box";
import Heading from "../../ui/text/Heading";
import { Effect, Power } from "../../services/apiCharacters";
import Line from "../../ui/separators/Line";

const Container = styled(Box)`
  display: flex;
  flex-direction: column;
`;

export default function EffectPowerToChoose({
  effectOrPower,
}: {
  effectOrPower: Effect | Power;
}) {
  return (
    <Container>
      <Heading>{effectOrPower.name}</Heading>
      <Line size="percantage"></Line>
      <span>{effectOrPower.description}</span>
    </Container>
  );
}
