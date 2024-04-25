import React from "react";
import styled from "styled-components";
import CreatePowerLayoutItem from "./CreatePowerLayoutItem";

const CreatePowerLayoutStyled = styled.div`
  width: 100%;
  display: flex;
  flex-direction: column;
  gap: 24px;
`;

export default function CreatePowerLayout({ data }) {
  return (
    <CreatePowerLayoutStyled>
      {data.map((el, i) => (
        <CreatePowerLayoutItem
          title={el.title}
          description={el.description}
          num={i}
          key={el.title}
        />
      ))}
    </CreatePowerLayoutStyled>
  );
}
