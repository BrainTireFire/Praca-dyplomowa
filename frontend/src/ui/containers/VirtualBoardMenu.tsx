import React from "react";
import styled from "styled-components";

const Menu = styled.div`
  position: absolute;
  background-color: var(--color-navbar);
  border: 1px solid var(--color-border);
  box-shadow: var(--shadow-md);
  z-index: 1000;
  display: flex;
  flex-direction: column;
`;

const MenuItem = styled.div`
  padding: 8px 12px;
  cursor: pointer;
  &:hover {
    background-color: var(--color-navbar);
  }
`;

const VirtualBoardMenu = ({ position }) => {
  const colors = ["red", "green", "blue", "yellow"];

  return (
    <Menu style={{ top: position.y, left: position.x }}>
      {colors.map((color) => (
        <MenuItem key={color} onClick={() => alert(color)}>
          {color}
        </MenuItem>
      ))}
    </Menu>
  );
};

export default VirtualBoardMenu;
