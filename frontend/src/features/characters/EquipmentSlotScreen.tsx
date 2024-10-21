import { useContext, useEffect } from "react";
import { CharacterIdContext } from "./contexts/CharacterIdContext";
import { useEquipmentSlots } from "./hooks/useEquipmentSlots";
import Spinner from "../../ui/interactive/Spinner";
import styled from "styled-components";
import Heading from "../../ui/text/Heading";
import { CharacterEquipmentAndSlotsDto_Item } from "../../services/apiCharacters";
import { Slot } from "../../models/slot";
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

  const onDragEnd = (result: any) => {
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

  if (isLoading || isPendingEquip || isPendingUnequip) {
    return <Spinner />;
  }
  if (isError) {
    return <div>{error?.message}</div>;
  }
  console.log(equipmentAndSlots);
  return (
    <ModalBreaker>
      <Heading as="h1">Equipment selection</Heading>
      <DragDropContext onDragEnd={onDragEnd}>
        <ScreenGrid>
          <StrictModeDroppable droppableId={"equipmentColumn"}>
            {(provided) => (
              <EquipmentColumn
                ref={provided.innerRef}
                {...provided.droppableProps}
              >
                {equipmentAndSlots?.items
                  .filter((item) => item.slots.length === 0)
                  .map((item, index) => (
                    <Draggable
                      draggableId={item.id.toString()}
                      index={index}
                      key={item.id.toString()}
                    >
                      {renderDraggable((provided: any) => (
                        <ItemBox
                          {...provided.draggableProps}
                          {...provided.dragHandleProps}
                          ref={provided.innerRef}
                        >
                          {item.name}
                        </ItemBox>
                      ))}
                    </Draggable>
                  ))}
                {provided.placeholder}
              </EquipmentColumn>
            )}
          </StrictModeDroppable>
          <SlotsMenu>
            {equipmentAndSlots?.slots.map((slot) => (
              <StrictModeDroppable
                droppableId={slot.id.toString()}
                key={slot.id}
              >
                {(provided) => (
                  <SlotBox ref={provided.innerRef} {...provided.droppableProps}>
                    {slot.name}
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
                          {renderDraggable((provided: any) => (
                            <ItemBox
                              {...provided.draggableProps}
                              {...provided.dragHandleProps}
                              ref={provided.innerRef}
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
        </ScreenGrid>
      </DragDropContext>
    </ModalBreaker>
  );
}

const ScreenGrid = styled.div`
  display: grid;
  grid-template-columns: 1fr 2fr;
  grid-template-rows: 1fr;
`;

const EquipmentColumn = styled.div`
  grid-column-start: 1;
  grid-column-end: 2;
`;

const ItemBox = styled.div`
  background-color: rgba(var(--color-secondary-background-rgb), 0.05);
  border: 1px solid var(--color-border);
  padding: 1rem 1rem;
  width: 100%;
  height: auto;
  border-radius: var(--border-radius-lg);
`;

const SlotsMenu = styled.div`
  grid-column-start: 2;
  grid-column-end: 3;
  display: flex;
  flex-direction: row;
  flex-wrap: wrap;
`;

const SlotBox = styled.div`
  background-color: rgba(var(--color-secondary-background-rgb), 0.05);
  border: 1px solid var(--color-border);
  width: 25%;
  aspect-ratio: 1; /* This ensures a square shape */
  border-radius: var(--border-radius-lg);
`;

// const StyledModal = styled.div`
//   position: fixed;
//   top: 50%;
//   left: 50%;
//   transform: translate(-50%, -50%);
//   background-color: var(--color-navbar);
//   border-radius: var(--border-radius-lg);
//   box-shadow: var(--shadow-lg);
//   padding: 3.2rem 4rem;
//   transition: all 0.5s;
// `;

const ModalBreaker = styled.div`
  position: relative;
  top: 0;
  left: 0;
  transform: none;
`;
