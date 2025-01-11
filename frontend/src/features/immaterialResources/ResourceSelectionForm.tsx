import { useCallback, useContext, useEffect, useState } from "react";
import { ParentObjectIdContext } from "../../context/ParentObjectIdContext";
import { useImmaterialResourceBlueprints } from "../powers/hooks/useImmaterialResourceBlueprints";
import { useObjectResources } from "../../hooks/useObjectResources";
import { useUpdateObjectResources } from "../../hooks/useUpdateObjectResources";
import { ImmaterialResourceAmount } from "../../models/immaterialResourceAmount";
import { ImmaterialResourceBlueprint } from "../../models/immaterialResourceBlueprint";
import { ReusableTable } from "../../ui/containers/ReusableTable";
import Spinner from "../../ui/interactive/Spinner";
import Button from "../../ui/interactive/Button";
import FormRowVertical from "../../ui/forms/FormRowVertical";
import Input from "../../ui/forms/Input";
import styled, { css } from "styled-components";

export function ResourceSelectionForm() {
  const { objectId, objectType } = useContext(ParentObjectIdContext);

  const {
    isLoading: isLoadingAllResources,
    immaterialResourceBlueprints: allResources,
  } = useImmaterialResourceBlueprints();
  const { isLoading: isLoadingItemResources, resources: itemResources } =
    useObjectResources(objectId, objectType);
  const { isPending, updateObjectResources: updateItemResources } =
    useUpdateObjectResources(() => {}, objectId as number, objectType);

  const [itemResourcesLocal, setItemResourcesLocal] = useState(
    itemResources as ImmaterialResourceAmount[]
  );

  const [selectedResourceIdFromAll, setSelectedResourceIdFromAll] = useState<
    number | null
  >(null);
  const [selectedResourceIdFromItem, setSelectedResourceIdFromItem] = useState<
    number | null
  >(null);
  const [resourceCount, setResourceCount] = useState<number>(1);
  const [resourceLevel, setResourceLevel] = useState<number>(1);

  let id = 0;
  const getNewId = useCallback(() => {
    return id++;
  }, [id]);

  useEffect(() => {
    setItemResourcesLocal(
      itemResources?.map((r) => {
        return { ...r, id: getNewId() };
      }) ?? []
    );
  }, [itemResources, getNewId]);

  const handleSelectAllResources = (row: { id: number; name: string }) => {
    let selectedItem = allResources?.find(
      (_value: ImmaterialResourceBlueprint, index: number) => index === row.id
    );
    setSelectedResourceIdFromAll(selectedItem ? selectedItem.id : null);
    setSelectedResourceIdFromItem(null);
  };
  const handleSelectItemResources = (row: { id: number; name: string }) => {
    let selectedItem = itemResourcesLocal?.find(
      (_value, index) => index === row.id
    );
    setSelectedResourceIdFromItem(selectedItem ? selectedItem.id : null);
    setSelectedResourceIdFromAll(null);
  };

  return (
    <Grid>
      <Column1>
        {!isLoadingAllResources && (
          <ReusableTable
            mainHeader="Available resources"
            tableRowsColomns={{ Name: "name" }}
            data={
              allResources?.map((resource, index) => {
                return {
                  id: index,
                  name: resource.name,
                };
              }) ?? []
            }
            isSelectable={true}
            onSelect={handleSelectAllResources}
            customTableContainer={css`
              height: 100%;
            `}
          ></ReusableTable>
        )}
        {isLoadingAllResources && <Spinner />}
      </Column1>
      <Column2>
        {!isPending && (
          <>
            <Button
              disabled={
                resourceCount <= 0 ||
                resourceLevel <= 0 ||
                selectedResourceIdFromAll === null
              }
              onClick={() => {
                setItemResourcesLocal(() => {
                  return [
                    ...(
                      itemResourcesLocal as ImmaterialResourceAmount[]
                    ).filter(
                      (resource) =>
                        !(
                          resource.blueprintId === selectedResourceIdFromAll &&
                          resource.level === resourceLevel
                        )
                    ),
                    {
                      blueprintId: selectedResourceIdFromAll as number,
                      name:
                        allResources!.find(
                          (x) => x.id === selectedResourceIdFromAll
                        )?.name ?? "name missing",
                      level: resourceLevel,
                      count:
                        resourceCount +
                        (itemResourcesLocal as ImmaterialResourceAmount[])
                          .filter(
                            (resource) =>
                              resource.blueprintId ===
                                selectedResourceIdFromAll &&
                              resource.level === resourceLevel
                          )
                          .map((x) => x.count)
                          .reduce(
                            (accumulator, current) => accumulator + current,
                            0
                          ),
                      id: getNewId(),
                    },
                  ];
                });
                setSelectedResourceIdFromAll(null);
              }}
            >
              {">>"}
            </Button>
            <Button
              disabled={selectedResourceIdFromItem === null}
              onClick={() => {
                setItemResourcesLocal(() => {
                  return (
                    itemResourcesLocal as ImmaterialResourceAmount[]
                  ).filter(
                    (resource) => resource.id !== selectedResourceIdFromItem
                  );
                });
                setSelectedResourceIdFromItem(null);
              }}
            >
              {"<<"}
            </Button>
            <FormRowVertical
              label="Count"
              assistiveText="Value must be greater or equal 1"
            >
              <Input
                type="number"
                min={1}
                value={resourceCount}
                required={true}
                onChange={(e) => setResourceCount(Number(e.target.value))}
              ></Input>
            </FormRowVertical>
            <FormRowVertical
              label="Level"
              assistiveText="Value must be greater or equal 1"
            >
              <Input
                type="number"
                min={1}
                value={resourceLevel}
                required={true}
                onChange={(e) => {
                  e.target.checkValidity();
                  e.target.reportValidity();
                  setResourceLevel(Number(e.target.value));
                }}
              ></Input>
            </FormRowVertical>
            <Button onClick={() => updateItemResources(itemResourcesLocal)}>
              {"Save"}
            </Button>
          </>
        )}
      </Column2>
      <Column3>
        {!isLoadingItemResources && (
          <ReusableTable
            mainHeader="Selected resources"
            tableRowsColomns={{
              Name: "name",
              Level: "level",
              Number: "number",
            }}
            data={
              itemResourcesLocal?.map((resource, index) => {
                return {
                  id: index,
                  name: resource.name,
                  level: resource.level,
                  number: resource.count,
                };
              }) ?? []
            }
            isSelectable={true}
            onSelect={handleSelectItemResources}
            customTableContainer={css`
              height: 100%;
            `}
          ></ReusableTable>
        )}
        {isLoadingItemResources && <Spinner />}
      </Column3>
    </Grid>
  );
}

const Grid = styled.div`
  display: grid;
  grid-template-columns: auto 1fr auto;
  grid-column-gap: 10px;
  width: 80vw;
  height: 70vh;
`;

const Column1 = styled.div`
  grid-column: 1/2;
  height: 100%;
  overflow-y: hidden;
`;
const Column2 = styled.div`
  grid-column: 2/3;
  display: flex;
  flex-direction: column;
`;
const Column3 = styled.div`
  grid-column: 3/4;
  height: 100%;
  overflow-y: hidden;
`;
