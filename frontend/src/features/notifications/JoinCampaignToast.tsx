import React from "react";
import Link from "../../ui/links/Link";
import styled from "styled-components";
import Heading from "../../ui/text/Heading";

const ToastWrapper = styled.div`
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

interface JoinCampaignToastProps {
  message: string;
  link: string;
}

const JoinCampaignToast: React.FC<JoinCampaignToastProps> = ({
  message,
  link,
}) => {
  return (
    <Link to={link}>
      <ToastWrapper>
        <ToastContent>
          <ToastHeader>New person join campaing</ToastHeader>
          <Message>{message}</Message>
          <ToastAction>Click here to view the campaign</ToastAction>
        </ToastContent>
      </ToastWrapper>
    </Link>
  );
};

export default JoinCampaignToast;
