import styled from "styled-components";

const StyledDropdown = styled.div`
  display: flex;
  align-items: center;
  flex-direction: column;
  gap: 0.2rem;
`;

const Box = styled.div`
  position: relative;
  display: flex;
  align-items: center;
  justify-content: center;
  flex-direction: column;
  font-size: 3rem;
  padding-bottom: 40px;

  border: 1px solid var(--color-border);
  border-radius: var(--border-radius-sm);
  width: 7rem;
  height: 7rem;
`;

const Header = styled.div`
  font-size: 1.2rem;
  font-weight: 500;
`;

const Circle = styled.div`
  position: absolute;
  bottom: 0;
  display: flex;
  align-items: center;
  justify-content: center;
  width: 4rem;
  height: 4rem;
  border: 1px solid var(--color-border);
  border-radius: 100%;
  font-size: 2rem;
`;

const Text = styled.p`
  /* border: 1px solid var(--color-border); */
  /* background-color: transparent; */
  justify-content: center;
  align-items: center;
  font-size: 2rem;
`;

function AttributeBox({ attribute }: { attribute: any }) {
  return (
    <StyledDropdown>
      <Header>{attribute.header}</Header>
      <Box>
        <Text>{attribute.value}</Text>
        <Circle>{attribute.modifier}</Circle>
      </Box>
    </StyledDropdown>
  );
}

// AttributeBox.Header = Header;
// AttributeBox.Box = Box;
// AttributeBox.Circle = Circle;
// AttributeBox.Input = Input;
// AttributeBox.Text = Text;

export default AttributeBox;
