import { useCallback, useContext, useEffect, useState } from "react";
import Menus from "../../../ui/containers/Menus";
import Table from "../../../ui/containers/Table";
import styled from "styled-components";
import { ItemIdContext } from "../contexts/ItemIdContext";
import { ReusableTable } from "../../../ui/containers/ReusableTable";
import Button from "../../../ui/interactive/Button";
import Spinner from "../../../ui/interactive/Spinner";
import { ImmaterialResourceAmount } from "../../../models/immaterialResourceAmount";
import { ImmaterialResourceBlueprint } from "../../../models/immaterialResourceBlueprint";
import { useImmaterialResourceBlueprints } from "../hooks/useImmaterialResourceBlueprints";
import { useItemResources } from "../hooks/useItemResources";
import { useUpdateItemResources } from "../hooks/useUpdateItemResources";
import FormRowVertical from "../../../ui/forms/FormRowVertical";
import Input from "../../../ui/forms/Input";

export default function ResourcesTable({
  resources,
}: {
  resources: ImmaterialResourceAmount[];
}) {
  return (
    <Menus>
      <Table
        header="Resources available when equipped"
        button="Select"
        columns="1fr 1fr 1fr"
        modal={<ResourceSelectionForm />}
      >
        <Table.Header>
          <div>Name</div>
          <div>Level</div>
          <div>Number</div>
        </Table.Header>
        <Table.Body
          data={resources}
          render={(resource: ImmaterialResourceAmount) => (
            <ResourceRow key={resource.blueprintId} resource={resource} />
          )}
          columnCount={3}
        />
        <Table.Footer>{/* <Pagination count={count} /> */}</Table.Footer>
      </Table>
    </Menus>
  );
}

const Cell = styled.div`
  font-size: 1.2rem;
  font-weight: 600;
  color: var(--color-secondary-text);
`;

function ResourceRow({ resource }: { resource: ImmaterialResourceAmount }) {
  return (
    <Table.Row>
      <Cell>{resource.name}</Cell>
      <Cell>{resource.level}</Cell>
      <Cell>{resource.count}</Cell>
    </Table.Row>
  );
}

function ResourceSelectionForm() {
  const { itemId } = useContext(ItemIdContext);

  const { isLoading: isLoadingAllResources, resources: allResources } =
    useImmaterialResourceBlueprints();
  const { isLoading: isLoadingItemResources, resources: itemResources } =
    useItemResources(itemId);
  const { isPending, updateItemResources } = useUpdateItemResources(() => {},
  itemId as number);

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
`;

const Column1 = styled.div`
  grid-column: 1/2;
`;
const Column2 = styled.div`
  grid-column: 2/3;
  display: flex;
  flex-direction: column;
`;
const Column3 = styled.div`
  grid-column: 3/4;
`;
