import { useContext, useEffect, useState } from "react";
import { CharacterIdContext } from "./contexts/CharacterIdContext";
import { useEquipmentSlots } from "./hooks/useEquipmentSlots";
import Spinner from "../../ui/interactive/Spinner";
import styled, { css } from "styled-components";
import Heading from "../../ui/text/Heading";
import { CharacterEquipmentAndSlotsDto_Item } from "../../services/apiCharacters";
import { DragDropContext, Droppable, Draggable } from "react-beautiful-dnd";
import { StrictModeDroppable } from "../../utils/StrictModeDroppable";
import { useDraggableInPortal } from "../../utils/useDraggableInPortal";
import { useEquipItem } from "./hooks/useEquipItem";
import { useUnequipItem } from "./hooks/useUnequipItem";
// import { DraggableGoal } from "../../utils/DraggableGoal";

export default function EquipmentSlotScreen({
  onCloseModal,
}: {
  onCloseModal: () => void;
}) {
  const { characterId } = useContext(CharacterIdContext);
  const { equipmentAndSlots, isError, isLoading, error } =
    useEquipmentSlots(characterId);
  const renderDraggable = useDraggableInPortal();

  const { equipItemInSlot, isPending: isPendingEquip } = useEquipItem(() => {});
  const { unequipItemInSlot, isPending: isPendingUnequip } = useUnequipItem(
    () => {}
  );

  const [draggableId, setDraggableId] = useState("");
  const [hoverDroppableId, setHoverDroppableId] = useState("");

  const onDragEnd = (result: any) => {
    setHoverDroppableId("");
    setDraggableId("");
    if (result.reason === "DROP") {
      if (
        result.destination.droppableId === "equipmentColumn" &&
        result.source.droppableId !== "equipmentColumn"
      ) {
        unequipItemInSlot({
          characterId,
          slotId: result.source.droppableId,
          itemId: result.draggableId,
        });
      } else if (
        result.destination.droppableId !== "equipmentColumn" &&
        result.source.droppableId === "equipmentColumn"
      ) {
        equipItemInSlot({
          characterId,
          slotId: result.destination.droppableId,
          itemId: result.draggableId,
        });
      }
    }
  };

  const onDragStart = (entry: any) => setDraggableId(entry.draggableId);

  const onDragUpdate = (entry: any) =>
    setHoverDroppableId(entry.destination.droppableId);

  const isHoveringOverApplicableSlot = (
    slotIdString: string,
    draggable: CharacterEquipmentAndSlotsDto_Item
  ) => {
    const slotId = parseInt(slotIdString);
    if (draggable.equippableInSlots.filter((x) => x.id === slotId).length > 0) {
      return true;
    } else {
      return false;
    }
  };

  if (isLoading || isPendingEquip || isPendingUnequip) {
    return <Spinner />;
  }
  if (isError) {
    return <div>{error?.message}</div>;
  }
  console.log(equipmentAndSlots);
  return (
    <>
      <Heading as="h1">Equipment selection</Heading>
      <DragDropContext
        onDragEnd={onDragEnd}
        onDragStart={onDragStart}
        onDragUpdate={onDragUpdate}
      >
        <ScreenGrid>
          <StrictModeDroppable droppableId={"equipmentColumn"}>
            {(provided) => (
              <EquipmentColumn
                ref={provided.innerRef}
                {...provided.droppableProps}
              >
                <Heading as="h3">Equippable items</Heading>
                {equipmentAndSlots?.items
                  .filter((item) => item.slots.length === 0)
                  .map((item, index) => (
                    <Draggable
                      draggableId={item.id.toString()}
                      index={index}
                      key={item.id.toString()}
                    >
                      {renderDraggable((provided: any, snapshot: any) => (
                        <ItemBox
                          {...provided.draggableProps}
                          {...provided.dragHandleProps}
                          ref={provided.innerRef}
                          isOverCorrectTarget={isHoveringOverApplicableSlot(
                            snapshot.draggingOver,
                            item
                          )}
                        >
                          {item.name}
                          {console.log(snapshot)}
                        </ItemBox>
                      ))}
                    </Draggable>
                  ))}
                {provided.placeholder}
              </EquipmentColumn>
            )}
          </StrictModeDroppable>
          <SlotsColumn>
            <Heading as="h3">Available equipment slots</Heading>
            <SlotsMenu>
              {equipmentAndSlots?.slots.map((slot) => (
                <StrictModeDroppable
                  droppableId={slot.id.toString()}
                  key={slot.id}
                >
                  {(provided) => (
                    <SlotBox
                      ref={provided.innerRef}
                      {...provided.droppableProps}
                      dragging={draggableId !== ""}
                      isMatching={equipmentAndSlots?.items
                        .find((item) => item.id.toString() === draggableId)
                        ?.equippableInSlots.find((s) => s.id === slot.id)}
                      draggedOver={hoverDroppableId === slot.id.toString()}
                    >
                      <Heading as="h4">{slot.name}</Heading>
                      {equipmentAndSlots?.items
                        .filter((item) =>
                          item.slots.some((s) => s.id === slot.id)
                        )
                        .map((item, index) => (
                          <Draggable
                            draggableId={item.id.toString()}
                            index={index}
                            key={item.id.toString()}
                          >
                            {renderDraggable((provided: any, snapshot: any) => (
                              <ItemBox
                                {...provided.draggableProps}
                                {...provided.dragHandleProps}
                                ref={provided.innerRef}
                                isOverCorrectTarget={isHoveringOverApplicableSlot(
                                  snapshot.draggingOver,
                                  item
                                )}
                              >
                                {item.name}
                              </ItemBox>
                            ))}
                          </Draggable>
                        ))}

                      {provided.placeholder}
                    </SlotBox>
                  )}
                </StrictModeDroppable>
              ))}
            </SlotsMenu>
          </SlotsColumn>
        </ScreenGrid>
      </DragDropContext>
    </>
  );
}

const ScreenGrid = styled.div`
  display: grid;
  grid-template-columns: 150px 2fr;
  grid-template-rows: 1fr;
  height: 500px;
  width: 1000px;
`;

const EquipmentColumn = styled.div`
  grid-column-start: 1;
  grid-column-end: 2;
  border-right: 1px solid var(--color-border);
  margin-right: 5px;
  padding-right: 5px;
`;

const ItemBox = styled.div`
  background-color: ${(props: any) =>
    props.isOverCorrectTarget ? "green" : css`var(--color-main-background)`};
  border: 1px solid var(--color-border);
  padding: 1rem 1rem;
  width: 100%;
  height: auto;
  border-radius: var(--border-radius-lg);
`;

const SlotsColumn = styled.div`
  grid-column-start: 2;
  grid-column-end: 3;
`;
const SlotsMenu = styled.div`
  display: flex;
  flex-direction: row;
  flex-wrap: wrap;
`;

const SlotBox = styled.div<any>`
  background-color: ${(props: any) =>
    props.draggedOver
      ? css`rgba(var(--color-secondary-background-rgb), 0.2)`
      : css`rgba(var(--color-secondary-background-rgb), 0.05)`};
  border: 5px solid var(--color-border);
  border-color: ${(props: any) =>
    props.dragging
      ? props.isMatching
        ? "green"
        : "darkred"
      : css`rgba(var(--color-secondary-background-rgb), 0.05)`};
  width: 150px;
  aspect-ratio: 1; /* This ensures a square shape */
  border-radius: var(--border-radius-lg);
`;
