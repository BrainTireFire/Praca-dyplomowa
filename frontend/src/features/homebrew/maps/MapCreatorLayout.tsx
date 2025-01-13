import React, { useEffect, useReducer, useState } from "react";
import styled from "styled-components";
import MapCreatorForm from "./MapCreatorForm";
import MapBoard from "./MapBoard";

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
  justify-content: space-between;
  background-color: var(--color-navbar);
  border: 1px solid var(--color-border);
  height: 100%;
  width: 300px;
`;

const MainGrid = styled.div`
  display: grid;
  grid-template-rows: 1fr;
  grid-template-columns: 300px 1fr;
  max-height: 100%;
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

export default function MapCreatorLayout({ boardData }) {
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
        <MapCreatorForm state={state} onSubmit={handleFormSubmit} />
      </LeftPanel>
    </MainGrid>
  );
}
