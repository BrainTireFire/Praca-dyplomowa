import { css } from "styled-components";
import FormRowVertical from "../../../ui/forms/FormRowVertical";
import Input from "../../../ui/forms/Input";

export default function SearchForm({ onInputChange }) {
  return (
    <>
      <FormRowVertical label="Name">
        <Input
          placeholder="Name of the store"
          name="name"
          onChange={onInputChange}
        ></Input>
      </FormRowVertical>
      <FormRowVertical label="Type">
        <Input
          placeholder="Type of the store"
          name="type"
          onChange={onInputChange}
        ></Input>
      </FormRowVertical>
      <FormRowVertical label="Location">
        <Input
          placeholder="Location of the store"
          name="location"
          onChange={onInputChange}
        ></Input>
      </FormRowVertical>
    </>
  );
}
