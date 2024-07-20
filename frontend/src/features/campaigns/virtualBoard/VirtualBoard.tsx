import React, { useEffect, useRef, useState, useCallback } from "react";
import styled from "styled-components";
import { HubConnection } from "@microsoft/signalr";

const CanvasContainer = styled.div`
  display: flex;
  justify-content: center;
  align-items: center;
  overflow: hidden;
  position: relative;
  width: 100%;
  height: 100%;
`;

const GRID_SIZE = 70;
const MIN_SCALE = 0.5;
const MAX_SCALE = 3;
const INITIAL_WIDTH = 1260;
const INITIAL_HEIGHT = 700;

type Coordinate = {
  x: number;
  y: number;
};

const drawGrid = (
  ctx: CanvasRenderingContext2D,
  width: number,
  height: number,
  scale: number
) => {
  ctx.save();
  ctx.scale(scale, scale);

  ctx.strokeStyle = "#49A078";
  ctx.lineWidth = 1;

  for (let x = 0; x <= width / scale; x += GRID_SIZE) {
    ctx.beginPath();
    ctx.moveTo(x, 0);
    ctx.lineTo(x, height / scale);
    ctx.stroke();
  }

  for (let y = 0; y <= height / scale; y += GRID_SIZE) {
    ctx.beginPath();
    ctx.moveTo(0, y);
    ctx.lineTo(width / scale, y);
    ctx.stroke();
  }

  ctx.restore();
};

const highlightBox = (
  ctx: CanvasRenderingContext2D,
  x: number,
  y: number,
  scale: number
) => {
  ctx.save();
  ctx.strokeStyle = "#FA9021";
  ctx.lineWidth = 2;
  ctx.strokeRect(x * GRID_SIZE, y * GRID_SIZE, GRID_SIZE, GRID_SIZE);
  ctx.restore();
};

type VirtualBoardProps = {
  connection: HubConnection;
};

const VirtualBoard: React.FC<VirtualBoardProps> = ({ connection }) => {
  const canvasRef = useRef<HTMLCanvasElement | null>(null);
  const [selectedBox, setSelectedBox] = useState<Coordinate | null>(null);
  const [scale, setScale] = useState<number>(1);
  const [translatePos, setTranslatePos] = useState<Coordinate>({ x: 0, y: 0 });

  const drawCanvas = useCallback(() => {
    const canvas = canvasRef.current;
    if (!canvas) return;
    const ctx = canvas.getContext("2d");
    if (!ctx) return;
    const width = canvas.width;
    const height = canvas.height;

    ctx.clearRect(0, 0, width, height);
    drawGrid(ctx, width, height, scale);

    if (selectedBox) {
      highlightBox(ctx, selectedBox.x, selectedBox.y, scale);
    }
  }, [selectedBox, scale]);

  useEffect(() => {
    drawCanvas();
  }, [drawCanvas]);

  useEffect(() => {
    if (!connection) return;

    connection.on("ReceiveSelectedBox", (box: Coordinate) => {
      setSelectedBox(box);
    });

    return () => {
      connection.off("ReceiveSelectedBox");
    };
  }, [connection]);

  useEffect(() => {
    if (connection && selectedBox) {
      connection.send("UpdateSelectedBox", selectedBox).catch(console.error);
    }
  }, [connection, selectedBox]);

  const handleCanvasClick = (event: React.MouseEvent<HTMLCanvasElement>) => {
    const canvas = canvasRef.current;
    if (!canvas) return;
    const rect = canvas.getBoundingClientRect();
    const x = (event.clientX - rect.left) / scale - translatePos.x;
    const y = (event.clientY - rect.top) / scale - translatePos.y;

    const gridX = Math.floor(x / GRID_SIZE);
    const gridY = Math.floor(y / GRID_SIZE);

    setSelectedBox({ x: gridX, y: gridY });
  };

  const handleKeyDown = useCallback(
    (event: KeyboardEvent) => {
      if (!selectedBox) return;

      let { x, y } = selectedBox;

      switch (event.key) {
        case "ArrowUp":
          y = Math.max(0, y - 1);
          break;
        case "ArrowDown":
          y = Math.min(Math.floor(INITIAL_HEIGHT / GRID_SIZE) - 1, y + 1);
          break;
        case "ArrowLeft":
          x = Math.max(0, x - 1);
          break;
        case "ArrowRight":
          x = Math.min(Math.floor(INITIAL_WIDTH / GRID_SIZE) - 1, x + 1);
          break;
        default:
          return;
      }

      setSelectedBox({ x, y });
    },
    [selectedBox]
  );

  useEffect(() => {
    window.addEventListener("keydown", handleKeyDown);
    return () => window.removeEventListener("keydown", handleKeyDown);
  }, [handleKeyDown]);

  return (
    <CanvasContainer>
      <canvas
        ref={canvasRef}
        style={{
          backgroundColor: "#292929",
          border: "2px solid #49A078",
        }}
        width={INITIAL_WIDTH}
        height={INITIAL_HEIGHT}
        onClick={handleCanvasClick}
      >
        Canvas
      </canvas>
    </CanvasContainer>
  );
};

export default VirtualBoard;
