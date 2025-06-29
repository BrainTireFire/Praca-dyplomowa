import { HiXMark } from "react-icons/hi2";
import styled from "styled-components";
import { createPortal } from "react-dom";
import { cloneElement, createContext, useState, useContext } from "react";

import { useOutsideClick } from "../../hooks/useOutsideClick";

const StyledModal = styled.div.attrs(() => ({
  className: "click-outside-disable",
}))`
  position: fixed;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
  background-color: var(--color-navbar);
  border-radius: var(--border-radius-lg);
  box-shadow: var(--shadow-lg);
  padding: 3.2rem 4rem;
  transition: all 0.5s;
`;

const Overlay = styled.div`
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  height: 100vh;
  /* background-color: var(--color-button-primary); */
  backdrop-filter: blur(4px);
  z-index: 1000; // lower than in Menus component
  transition: all 0.5s;
`;

const Button = styled.button`
  background: none;
  border: none;
  padding: 0.4rem;
  border-radius: var(--border-radius-sm);
  transform: translateX(0.8rem);
  transition: all 0.2s;
  position: absolute;
  top: 1.2rem;
  right: 1.9rem;

  &:hover {
    background-color: var(--color-button-primary);
  }

  & svg {
    width: 2.4rem;
    height: 2.4rem;
    /* Sometimes we need both */
    /* fill: var(--color-secondary-text);
    stroke: var(--color-secondary-text); */
    color: var(--color-button-primary);
  }

  &:hover svg {
    color: var(--color-image-hover);
  }
`;

const ModalContext = createContext();

function Modal({ children }: { children: React.ReactNode }) {
  const [openName, setOpenName] = useState("");

  const close = () => setOpenName("");
  const open = setOpenName;

  return (
    <ModalContext.Provider value={{ open, close, openName }}>
      {children}
    </ModalContext.Provider>
  );
}

function Open({ children, opens: opensWindowName }) {
  const { open } = useContext(ModalContext);

  return cloneElement(children, { onClick: () => open(opensWindowName) });
}

function Window({
  name,
  children,
}: {
  name: string;
  children: React.ReactNode;
}) {
  const { openName, close } = useContext(ModalContext);
  // const ref = useOutsideClick(close);

  if (name !== openName) {
    return null;
  }

  return createPortal(
    <Overlay>
      {/* <StyledModal ref={ref}> // commented out to disable closing the modal when clicking outside of it as it broke nested modals*/}
      <StyledModal>
        <Button onClick={close}>
          <HiXMark />
        </Button>
        <div>{cloneElement(children, { onCloseModal: close })}</div>
      </StyledModal>
    </Overlay>,
    document.body
  );
}

Modal.Open = Open;
Modal.Window = Window;

export default Modal;
