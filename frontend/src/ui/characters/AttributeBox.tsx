import styled from "styled-components";

const StyledDropdown = styled.div`
  display: flex;
  align-items: center;
  flex-direction: column;
  gap: 0.8rem;
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
  width: 120px;
  height: 120px;
`;

const Header = styled.div`
  font-size: 1.6rem;
  font-weight: 500;
`;

const Circle = styled.div`
  position: absolute;
  bottom: 0;
  display: flex;
  align-items: center;
  justify-content: center;
  width: 50px;
  height: 50px;
  border: 1px solid var(--color-border);
  border-radius: 100%;
`;

const Text = styled.p`
  /* border: 1px solid var(--color-border); */
  /* background-color: transparent; */
  justify-content: center;
  align-items: center;
  width: 40px;
  height: 40px;
`;

const Input = styled.input`
  /* border: 1px solid var(--color-border); */
  /* background-color: transparent; */
  width: 40px;
  height: 40px;
`;

function AttributeBox({ children }: { children: React.ReactNode }) {
  return <StyledDropdown>{children}</StyledDropdown>;
}

AttributeBox.Header = Header;
AttributeBox.Box = Box;
AttributeBox.Circle = Circle;
AttributeBox.Input = Input;
AttributeBox.Text = Text;

export default AttributeBox;
