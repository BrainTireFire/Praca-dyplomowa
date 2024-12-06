import FormRowVertical from "../../ui/forms/FormRowVertical";
import Input from "../../ui/forms/Input";

export function CoinPurseForm({}) {
  return (
    <>
      <FormRowVertical label="Gold pieces">
        <Input type="number"></Input>
      </FormRowVertical>
      <FormRowVertical label="Silver pieces">
        <Input type="number"></Input>
      </FormRowVertical>
      <FormRowVertical label="Copper pieces">
        <Input type="number"></Input>
      </FormRowVertical>
    </>
  );
}
