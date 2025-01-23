import styled from "styled-components";
import { sanitizeForId } from "../../../utils/stringUtils";
import { TabItemProps } from "./TabTypes";

const TabItem: React.FC<TabItemProps> = ({ label, children }) => (
  <TabPanel
    role="tabpanel"
    aria-labelledby={`tab-${sanitizeForId(label)}`}
    id={`panel-${sanitizeForId(label)}`}
  >
    {children}
  </TabPanel>
);
export default TabItem;

const TabPanel = styled.div`
  padding: 1em;
  background-color: var(--tab-panel-bg-color);
  border-radius: 0.5em;
  overflow-y: hidden;
  flex: 1 1 auto;
`;
