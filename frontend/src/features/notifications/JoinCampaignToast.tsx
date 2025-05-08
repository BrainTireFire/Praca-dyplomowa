import React, { useState } from "react";
import Link from "../../ui/links/Link";
import styled from "styled-components";
import Heading from "../../ui/text/Heading";
import { ImCross } from "react-icons/im";

const ToastWrapper = styled.div`
  position: relative;
  display: flex;
  align-items: center;
  justify-content: space-between;
  padding: 12px 16px;
  border-radius: 8px;
  background-color: var(--color-navbar);
  box-shadow: 0 2px 6px rgba(0, 0, 0, 0.1);
  transition: background-color 0.2s ease;
  cursor: pointer;

  &:hover {
    background-color: var(--color-navbar-hover);
  }
`;

const ToastContent = styled.div`
  display: flex;
  flex-direction: column;
  gap: 8px;
`;

const Message = styled.p`
  margin: 0;
  font-size: 14px;
  color: var(--color-secondary-text);
`;

const ToastAction = styled.span`
  font-size: 12px;
  color: var(--color-link);
  font-weight: bold;
  text-decoration: underline;
  cursor: pointer;
  display: block;
  text-align: center;
  width: 100%;

  &:hover {
    color: var(--color-link-hover);
  }
`;

const ToastHeader = styled(Heading)`
  margin: 0;
  font-size: 16px;
  color: var(--color-header-text);
`;

const CloseButton = styled.button`
  position: absolute;
  top: 8px;
  right: 8px;
  background: none;
  border: none;
  color: var(--color-secondary-text);
  font-size: 16px;
  cursor: pointer;
  transition: color 0.2s ease;

  &:hover {
    color: var(--color-link-hover);
  }
`;

interface JoinCampaignToastProps {
  message: string;
  link: string;
}

const JoinCampaignToast: React.FC<JoinCampaignToastProps> = ({
  message,
  link,
}) => {
  const [isVisible, setIsVisible] = useState(true);

  const handleClose = (e: React.MouseEvent<HTMLButtonElement>) => {
    e.stopPropagation();
    setIsVisible(false);
  };

  if (!isVisible) return null;

  return (
    <ToastWrapper>
      <CloseButton onClick={handleClose} aria-label="Close">
        <ImCross />
      </CloseButton>
      <ToastContent>
        <ToastHeader>New person join campaign</ToastHeader>
        <Message>{message}</Message>
        <Link to={link}>
          <ToastAction>Click here to view the campaign</ToastAction>
        </Link>
      </ToastContent>
    </ToastWrapper>
  );
};

export default JoinCampaignToast;
