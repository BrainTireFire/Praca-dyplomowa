import React, { useEffect, useReducer, useState } from "react";
import styled from "styled-components";
import MapBoard from "../../homebrew/maps/MapBoard";
import { DragDropContext, Draggable, Droppable } from "react-beautiful-dnd";
import { StrictModeDroppable } from "../../../utils/StrictModeDroppable";

const GridContainer = styled.div`
  grid-row: 1 / 2;
  grid-column: 2 / 3;
  display: flex;
  justify-content: center;
  align-items: center;
  height: 100%;
  overflow: hidden;
`;

const LeftPanel = styled.div`
  grid-row: 1 / 3;
  grid-column: 1 / 2;
  display: flex;
  flex-direction: column;
  align-items: flex-start;
  background-color: var(--color-navbar);
  border: 1px solid var(--color-border);
  padding: 20px;
  height: 100%;
  width: 300px;
`;

const MainGrid = styled.div`
  display: grid;
  grid-template-rows: 1fr;
  grid-template-columns: 300px 1fr;
  margin-top: 20px;
`;

const initialState = {
  selectedBox: null,
  fields: [],
  board: {
    name: "",
    description: "",
    sizeX: 16,
    sizeY: 9,
  },
  isUpdating: false,
};

const Label = styled.label`
  font-weight: bold;
  margin-bottom: 5px;
`;

const FieldSet = styled.div`
  position: relative;
  width: 100%;
  margin-bottom: 10px;
  padding: 10px;
  display: flex;
  align-items: center;
  justify-content: center;
  background-color: var(--color-background);
  border: 1px solid var(--color-border);
  border-radius: 5px; /* Rounded corners for a button-like look */
  cursor: pointer;
  transition: background-color 0.3s ease, box-shadow 0.3s ease;

  &:hover {
    background-color: var(--color-hover);
    box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1); /* Add a shadow effect */
  }
`;

const FieldContainerStyled = styled.div`
  display: flex;
  flex-direction: column;
  align-items: center;
`;

function reducer(state, action) {
  switch (action.type) {
    case "SET_SELECTED_BOX":
      return { ...state, selectedBox: action.payload };
    case "UPDATE_FIELD":
      return {
        ...state,
        fields: state.fields.map((field) =>
          field.positionX === action.payload.data.positionX &&
          field.positionY === action.payload.data.positionY
            ? { ...field, ...action.payload.data }
            : field
        ),
      };
    case "ADD_FIELD":
      return {
        ...state,
        fields: [...state.fields, action.payload],
      };
    case "ADD_ALL_FIELDS":
      return {
        ...state,
        fields: action.payload,
      };
    case "ADD_BOARD_DATA":
      return {
        ...state,
        board: {
          ...action.payload,
          sizeX: Number(action.payload.sizeX),
          sizeY: Number(action.payload.sizeY),
        },
      };
    case "UPDATE_IS_UPDATING":
      return {
        ...state,
        isUpdating: true,
      };
    default:
      return state;
  }
}

export default function EncounterMapCreaterLayout({ boardData, campaign }) {
  const [state, dispatch] = useReducer(reducer, initialState);

  useEffect(() => {
    if (boardData) {
      dispatch({ type: "ADD_BOARD_DATA", payload: boardData });

      if (boardData.fields) {
        dispatch({ type: "ADD_ALL_FIELDS", payload: boardData.fields });
        dispatch({ type: "UPDATE_IS_UPDATING" });
      }
    }
  }, [boardData]);

  const handleFormSubmit = (data) => {
    if (!state.selectedBox) return;

    const updatedField = {
      ...data,
      positionX: state.selectedBox.x,
      positionY: state.selectedBox.y,
    };

    const existingField = state.fields.find(
      (field) =>
        field.positionX === updatedField.positionX &&
        field.positionY === updatedField.positionY
    );

    if (existingField) {
      dispatch({
        type: "UPDATE_FIELD",
        payload: { data: updatedField },
      });
    } else {
      dispatch({ type: "ADD_FIELD", payload: updatedField });
    }

    dispatch({ type: "SET_SELECTED_BOX", payload: null });
  };

  const handleMemberClick = (member) => {
    if (!state.selectedBox) return;

    const updatedField = {
      positionX: state.selectedBox.x,
      positionY: state.selectedBox.y,
      memberName: member.name,
    };

    const existingField = state.fields.find(
      (field) =>
        field.positionX === updatedField.positionX &&
        field.positionY === updatedField.positionY
    );

    if (existingField) {
      dispatch({
        type: "UPDATE_FIELD",
        payload: { data: updatedField },
      });
    } else {
      dispatch({ type: "ADD_FIELD", payload: updatedField });
    }

    dispatch({ type: "SET_SELECTED_BOX", payload: null }); // Deselect the box
  };

  return (
    <MainGrid>
      <GridContainer>
        <MapBoard
          board={state.board}
          selectedBox={state.selectedBox}
          onSelectedBox={(box) =>
            dispatch({ type: "SET_SELECTED_BOX", payload: box })
          }
          fields={state.fields}
        />
      </GridContainer>
      <LeftPanel>
        <Label>Members</Label>
        <FieldContainerStyled>
          {campaign.members.map((member) => (
            <FieldSet key={member.id} onClick={() => handleMemberClick(member)}>
              {member.name}
            </FieldSet>
          ))}
        </FieldContainerStyled>
      </LeftPanel>
    </MainGrid>
  );
}
