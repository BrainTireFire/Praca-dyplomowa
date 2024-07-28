import React from "react";
import Menus from "../../../ui/containers/Menus";
import Table from "../../../ui/containers/Table";
import ProficiencyRow from "./ProficiencyRow";
import { Language } from "../../../models/language";

export default function KnownLanguagesTable({
  languages,
}: {
  languages: Language[];
}) {
  return (
    <Menus>
      <Table
        header="Known languages"
        button="Choices available"
        columns="1fr 3.2rem"
      >
        <Table.Body
          data={languages}
          render={(language) => (
            <ProficiencyRow key={language.id} item={language} />
          )}
        />
        <Table.Footer>{/* <Pagination count={count} /> */}</Table.Footer>
      </Table>
    </Menus>
  );
}
