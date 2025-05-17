import { useContext, useState } from "react";
import { EditModeContext } from "../../context/EditModeContext";
import { ReusableTable } from "../../ui/containers/ReusableTable2";
import Spinner from "../../ui/interactive/Spinner";
import Modal from "../../ui/containers/Modal";
import styled, { css } from "styled-components";
import Button from "../../ui/interactive/Button";
import ItemFamilyForm from "../../features/itemFamilies/ItemFamilyForm";
import { useImmaterialResources } from "./hooks/useImmaterialResources";
import ImmaterialResourceForm from "../../features/immaterialResources/ImmaterialResourceForm";
import { useNavigate, useSearchParams } from "react-router-dom";

export default function ImmaterialResources() {
  const editMode = useContext(EditModeContext);
  const { isLoading, immaterialResources, error } = useImmaterialResources();


  const navigate = useNavigate();
  const [searchParams] = useSearchParams();
  const selectedItemId = searchParams.get("id");

  // const [selectedItemId, setSelectedItemId] = useState<null | number>(null);
  // const { createItem, isPending: isPendingCreation } = useCreateItem(() => {});
  const handleSelect = (row: any) => {
    let selectedItem = immaterialResources?.find(
      (_value, index) => index === row.id
    );
    if(!!selectedItem){
      navigate(`/immaterialResources?id=${selectedItem.id}`)
    }
  };

  if (isLoading) {
    return <Spinner></Spinner>;
  }
  return (
    <Container>
      <Column1>
        <ReusableTable
          tableRowsColomns={{
            Name: "Name",
            Owner: "OwnerName",
          }}
          data={
            immaterialResources
              ? immaterialResources.map((immaterialResource, index) => {
                  return {
                    id: index,
                    Name: immaterialResource.name,
                    OwnerName: immaterialResource.ownerName,
                  };
                })
              : []
          }
          isSelectable={true}
          onSelect={handleSelect}
          isSearching={true}
          customTableContainer={css`
            height: 95%;
          `}
        ></ReusableTable>
        {editMode && (
          <Modal>
            <Modal.Open opens="TableAction">
              <Button
                customStyles={css`
                  height: 5%;
                `}
              >
                Create new
              </Button>
            </Modal.Open>
            <Modal.Window name="TableAction">
              <ImmaterialResourceForm></ImmaterialResourceForm>
            </Modal.Window>
          </Modal>
        )}
      </Column1>
      <Column2>
        {selectedItemId && (
          <ImmaterialResourceForm
            immaterialResourceId={Number(selectedItemId)}
            key={selectedItemId}
          ></ImmaterialResourceForm>
        )}
      </Column2>
    </Container>
  );
}

const Container = styled.div`
  display: flex;
  flex-direction: row;
  max-height: 100%;
  height: 100%;
  column-gap: 10px;
`;

const Column1 = styled.div`
  max-height: 100%;
  height: 100%;
  width: 40%;
  display: flex;
  flex-direction: column;
`;
const Column2 = styled.div`
  max-height: 100%;
  height: 100%;
  max-width: 60%;
  width: 60%;
`;
