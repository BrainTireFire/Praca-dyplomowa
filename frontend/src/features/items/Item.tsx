import Box from "../../ui/containers/Box";
import Dropdown from "../../ui/forms/Dropdown";
import FormRowVertical from "../../ui/forms/FormRowVertical";
import Input from "../../ui/forms/Input";
import TextArea from "../../ui/forms/TextArea";
import GrantedPowersTable from "./GrantedPowersTable";
import GrantedResourcesTable from "./GrantedResourcesTable";

export default function Item() {
  return (
    <Box>
      <FormRowVertical label="Name">
        <Input type="text"></Input>
      </FormRowVertical>
      <FormRowVertical label="Item family">
        <Dropdown
          chosenValue={""}
          setChosenValue={() => {}}
          valuesList={[]}
        ></Dropdown>
      </FormRowVertical>
      <FormRowVertical label="Description" fillHeight={false}>
        <TextArea value={"test"}></TextArea>
      </FormRowVertical>
      <FormRowVertical label="Weight">
        <Input type="number"></Input>
      </FormRowVertical>
      <GrantedPowersTable powers={[]}></GrantedPowersTable>
      <GrantedResourcesTable resources={[]}></GrantedResourcesTable>
    </Box>
  );
}
