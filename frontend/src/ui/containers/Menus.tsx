import { useState } from "react";
import { useContext } from "react";
import { createContext } from "react";
import { createPortal } from "react-dom";
import { HiEllipsisVertical } from "react-icons/hi2";
import styled from "styled-components";
import { useOutsideClick } from "../../hooks/useOutsideClick";

const Menu = styled.div`
  display: flex;
  align-items: center;
  justify-content: flex-end;
`;

const StyledToggle = styled.button`
  background: none;
  border: none;
  padding: 0rem;
  border-radius: var(--border-radius-sm);
  transform: translateX(0.8rem);
  transition: all 0.2s;

  &:hover {
    background-color: var(--color-main-background);
  }

  & svg {
    width: 2.4rem;
    height: 2.4rem;
    color: var(--color-border);
  }
`;

const StyledList = styled.ul`
  position: fixed;
  z-index: 1001; // set higher than in Modal component
  background-color: var(--color-button-primary);
  box-shadow: var(--shadow-md);
  border-radius: var(--border-radius-md);

  right: ${(props) => props.position.x}px;
  top: ${(props) => props.position.y}px;
`;

const StyledButton = styled.button`
  width: 100%;
  text-align: left;
  background: none;
  border: none;
  padding: 1.2rem 2.4rem;
  font-size: 1.4rem;
  transition: all 0.2s;
  border-radius: var(--border-radius-md);

  display: flex;
  align-items: center;
  gap: 1.6rem;

  &:hover {
    background-color: var(--color-button-hover-primary);
  }

  & svg {
    width: 1.6rem;
    height: 1.6rem;
    color: var(--color-border);
    transition: all 0.3s;
  }
`;

const MenuContext = createContext();

function Menus({ children }) {
  const [openId, setOpenId] = useState("");
  const [position, setPostion] = useState(null);

  const open = setOpenId;
  const close = () => setOpenId("");

  return (
    <MenuContext.Provider value={{ openId, open, close, position, setPostion }}>
      {children}
    </MenuContext.Provider>
  );
}

function Toggle({ id, itemCount }) {
  const { openId, open, close, setPostion } = useContext(MenuContext);

  function handleClick(e) {
    e.stopPropagation();
    const rect = e.target.closest("button").getBoundingClientRect();

    const spaceBelow = window.innerHeight - (rect.y + rect.height);
    const spaceAbove = rect.y;

    const menuHeight = itemCount * 50;
    const offset = 8;

    // Decide whether to open above or below based on space
    const yPosition =
      spaceBelow < menuHeight && spaceAbove > menuHeight
        ? rect.y - menuHeight - offset // open above
        : rect.y + rect.height + offset; // open below

    setPostion({
      x: window.innerWidth - rect.width - rect.x,
      y: yPosition,
    });

    openId === "" || openId !== id ? open(id) : close();
  }

  return (
    <StyledToggle onClick={handleClick}>
      <HiEllipsisVertical />
    </StyledToggle>
  );
}

function List({ id, children }) {
  const { openId, position, close } = useContext(MenuContext);
  const ref = useOutsideClick(close, false);

  if (openId !== id) {
    return null;
  }

  return createPortal(
    <StyledList position={position} ref={ref}>
      {children}
    </StyledList>,
    document.body
  );
}

function Button({ children, icon, onClick }) {
  const { close } = useContext(MenuContext);

  function handleClick() {
    onClick?.();
    close();
  }

  return (
    <li>
      <StyledButton onClick={handleClick}>
        {icon}
        <span>{children}</span>
      </StyledButton>
    </li>
  );
}

Menus.Menu = Menu;
Menus.Toggle = Toggle;
Menus.List = List;
Menus.Button = Button;

Toggle.defaultProps = {
  itemCount: 1
}

export default Menus;
