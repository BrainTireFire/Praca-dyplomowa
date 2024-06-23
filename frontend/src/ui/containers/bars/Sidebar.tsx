import styled from "styled-components";
import MainSidebar from "./MainSidebar";

const StyledSidebar = styled.aside`
  background-color: var(--color-navbar);
  padding-top: 8rem;
  padding-left: 1rem;
  border-right: 1px solid var(--color-navbar);

  grid-row: 1 / -1;
  display: flex;
  flex-direction: column;
  gap: 3.2rem;
`;

function Sidebar() {
  return (
    <StyledSidebar>
      <MainSidebar />
    </StyledSidebar>
  );
}

export default Sidebar;
