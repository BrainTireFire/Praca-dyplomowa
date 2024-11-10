import React, { useCallback, useEffect, useRef, useState } from "react";
import styled from "styled-components";
import {
  GRID_SIZE,
  INITIAL_HEIGHT,
  INITIAL_WIDTH,
  getColorForUser,
  drawGrid,
  highlightBox,
  drawCustomCursor,
  drawSelectedBox,
  fillSelectedBox,
} from "../../campaigns/session/CanvasUtils";
import { Coordinate } from "../../../models/session/Coordinate";

const CanvasContainer = styled.div`
  display: flex;
  justify-content: center;
  align-items: center;
  overflow: hidden;
  position: relative;
  width: 100%;
  height: 100%;
`;

const Canvas = styled.canvas`
  background-color: var(--color-main-background);
  border: 2px solid var(--color-border);
`;

export default function MapBoard({
  board,
  selectedBox,
  onSelectedBox,
  fields,
}: any) {
  const canvasRef = useRef<HTMLCanvasElement | null>(null);

  const drawCanvas = useCallback(() => {
    const canvas = canvasRef.current;
    if (!canvas) return;
    const ctx = canvas.getContext("2d");
    if (!ctx) return;
    const width = canvas.width;
    const height = canvas.height;

    ctx.clearRect(0, 0, width, height);
    drawGrid(ctx, width, height, board.sizeX, board.sizeY);

    fields?.forEach((field: any) => {
      fillSelectedBox(ctx, field, board.sizeX, board.sizeY, field.color);
    });

    if (selectedBox) {
      drawSelectedBox(ctx, selectedBox, board.sizeX, board.sizeY);
    }
  }, [selectedBox, fields, board.sizeX, board.sizeY]);

  useEffect(() => {
    drawCanvas();
  }, [drawCanvas]);

  const handleCanvasClick = (
    event: React.MouseEvent<HTMLCanvasElement>,
    columns: number,
    rows: number
  ) => {
    const canvas = canvasRef.current;
    if (!canvas) return;

    const rect = canvas.getBoundingClientRect();
    const x = event.clientX - rect.left;
    const y = event.clientY - rect.top;

    const squareSize = Math.min(INITIAL_WIDTH / columns, INITIAL_HEIGHT / rows);

    const gridX = Math.floor(x / squareSize);
    const gridY = Math.floor(y / squareSize);

    if (selectedBox && selectedBox.x === gridX && selectedBox.y === gridY) {
      onSelectedBox(null); // Deselect the box
    } else {
      onSelectedBox({ x: gridX, y: gridY }); // Select the new box
    }
  };

  return (
    <CanvasContainer>
      <Canvas
        ref={canvasRef}
        width={INITIAL_WIDTH}
        height={INITIAL_HEIGHT}
        onClick={(event) => handleCanvasClick(event, board.sizeX, board.sizeY)}
      >
        Canvas
      </Canvas>
    </CanvasContainer>
  );
}
