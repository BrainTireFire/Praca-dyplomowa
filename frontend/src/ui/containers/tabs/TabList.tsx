import { ReactElement, useState } from "react";
import { TabItemProps, TabListProps } from "./TabTypes";
import React from "react";
import TabItem from "./TabItem";
import { sanitizeForId } from "../../../utils/stringUtils";
import styled from "styled-components";
import Button from "../../interactive/Button";

const TabList: React.FC<TabListProps> = ({ children, activeTabIndex = 0 }) => {
  const [activeTab, setActiveTab] = useState(activeTabIndex);
  const handleTabClick = (index: number) => {
    setActiveTab(index);
  };
  const tabs = React.Children.toArray(children).filter(
    (child): child is ReactElement<TabItemProps> =>
      React.isValidElement(child) && child.type === TabItem
  );
  return (
    <div className="tabs">
      <TabListWrapper>
        <TabListInternal role="tablist" aria-orientation="horizontal">
          {tabs.map((tab, index) => (
            <li key={`tab-${index}`}>
              <TabButton
                key={`tab-btn-${index}`}
                role="tab"
                id={`tab-${sanitizeForId(tab.props.label)}`}
                aria-controls={`panel-${sanitizeForId(tab.props.label)}`}
                aria-selected={activeTab === index}
                onClick={() => handleTabClick(index)}
                className={`${activeTab === index && "tab-btn--active"}`}
              >
                {tab.props.label}
              </TabButton>
            </li>
          ))}
        </TabListInternal>
      </TabListWrapper>
      {tabs[activeTab]}
    </div>
  );
};

export default TabList;

const TabListWrapper = styled.nav`
  overflow-x: scroll;
  scrollbar-width: none;
  -webkit-overflow-scrolling: touch;
`;

const TabListInternal = styled.ul`
  ::-webkit-scrollbar {
    display: none;
  }
  width: fit-content;
  min-width: 100%;
  display: flex;
  gap: 0.5em;
  margin: 0;
  padding: 0;
  border-bottom: 2px solid #eee;
  li {
    display: block;
    margin-bottom: -2px;
  }
`;

const TabButton = styled(Button)`
  font: inherit;
  padding: 0.75em 1em;
  border: 2px solid var(--tab-border-color);
  border-width: 0 0 2px;
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
  cursor: pointer;
  color: inherit;
  white-space: nowrap;

  &:not(.tab-btn--active):hover {
    color: var(--site-text-color);
  }
  &:not(.tab-btn--active) {
    color: var(--site-text-color);
    background-color: var(--color-button-secondary);
  }

  &.tab-btn--active {
    color: var(--tab-text-color--highlight);
    border-color: var(--tab-text-color--highlight);
  }
`;
