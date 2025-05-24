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
      <Table header="Known languages" columns="1fr">
        <Table.Body
        columnCount={1}
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
