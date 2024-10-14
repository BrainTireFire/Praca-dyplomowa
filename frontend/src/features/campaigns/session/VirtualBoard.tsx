import React, { useEffect, useRef, useState, useCallback } from "react";
import styled from "styled-components";
import { debounce } from "lodash";
import {
  GRID_SIZE,
  INITIAL_HEIGHT,
  INITIAL_WIDTH,
  getColorForUser,
  drawGrid,
  highlightBox,
  drawCustomCursor,
  drawSelectedBox,
} from "./CanvasUtils";
import { VirtualBoardProps } from "./../../../models/session/VirtualBoardProps";
import { Coordinate } from "../../../models/session/Coordinate";
import Menus from "../../../ui/containers/Menus";
import Modal from "../../../ui/containers/Modal";
import VirtualBoardMenu from "../../../ui/containers/VirtualBoardMenu";

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

type CursorInfo = {
  x: number;
  y: number;
  userName: string;
};

export default function VirtualBoard({
  connection,
  groupName,
}: VirtualBoardProps) {
  const canvasRef = useRef<HTMLCanvasElement | null>(null);
  const [selectedBoxes, setSelectedBoxes] = useState<{
    [connectionId: string]: Coordinate;
  }>({});
  const [userCursors, setUserCursors] = useState<{
    [connectionId: string]: CursorInfo;
  }>({});

  const [contextMenu, setContextMenu] = useState({
    x: 0,
    y: 0,
    isVisible: false,
    id: "",
  });
  const [selectedBox, setSelectedBox] = useState<Coordinate | null>(null);

  const [translatePos, setTranslatePos] = useState<Coordinate>({ x: 0, y: 0 });

  const drawCanvas = useCallback(() => {
    const canvas = canvasRef.current;
    if (!canvas) return;
    const ctx = canvas.getContext("2d");
    if (!ctx) return;
    const width = canvas.width;
    const height = canvas.height;

    ctx.clearRect(0, 0, width, height);
    drawGrid(ctx, width, height, 16, 9);

    Object.keys(selectedBoxes).forEach((connectionId) => {
      const box = selectedBoxes[connectionId];
      const color = getColorForUser(connectionId);
      drawSelectedBox(ctx, box, 16, 9);
    });

    // Draw cursors
    Object.keys(userCursors).forEach((connectionId) => {
      const cursor = userCursors[connectionId];
      const cursorColor = getColorForUser(connectionId);

      drawCustomCursor(ctx, cursor.x, cursor.y, cursorColor, cursor.userName);
    });
  }, [selectedBoxes, userCursors]);

  useEffect(() => {
    drawCanvas();
  }, [drawCanvas]);

  const handleCanvasClick = debounce(
    async (
      event: React.MouseEvent<HTMLCanvasElement>,
      columns: number,
      rows: number
    ) => {
      const canvas = canvasRef.current;
      if (!canvas || !connection || !groupName) return;
      const rect = canvas.getBoundingClientRect();
      // const x = event.clientX - rect.left - translatePos.x;
      // const y = event.clientY - rect.top - translatePos.y;
      const x = event.clientX - rect.left;
      const y = event.clientY - rect.top;

      const squareSize = Math.min(
        INITIAL_WIDTH / columns,
        INITIAL_HEIGHT / rows
      );

      const gridX = Math.floor(x / squareSize);
      const gridY = Math.floor(y / squareSize);

      // const gridX = Math.floor(x / GRID_SIZE);
      // const gridY = Math.floor(y / GRID_SIZE);

      const connectionId = connection?.connectionId as string;

      const updatedSelectedBoxes = {
        ...selectedBoxes,
        [connectionId]: { x: gridX, y: gridY },
      };
      setSelectedBoxes(updatedSelectedBoxes);

      await connection.invoke(
        "SendSelectedBoxes",
        groupName,
        updatedSelectedBoxes
      );
    },
    200
  );

  const handleCanvasRightClick = (
    event: React.MouseEvent<HTMLCanvasElement>
  ) => {
    event.preventDefault();
    const canvas = canvasRef.current;
    if (!canvas) return;
    const rect = canvas.getBoundingClientRect();
    const x = event.clientX - rect.left - translatePos.x;
    const y = event.clientY - rect.top - translatePos.y;

    const gridX = Math.floor(x / GRID_SIZE);
    const gridY = Math.floor(y / GRID_SIZE);

    // Adjust the offset here
    const offsetX = -120; // Offset to the right
    const offsetY = -100; // Offset downwards

    setSelectedBox({ x: gridX, y: gridY });
    setContextMenu({
      x: event.clientX + offsetX,
      y: event.clientY + offsetY,
      isVisible: true,
      id: `${gridX}+${gridY}`,
    });
  };

  // const handleColorChange = (color: string) => {
  //   if (!selectedBox || !connection || !groupName) return;
  //   const connectionId = connection.connectionId as string;
  //   setSelectedBoxes((prev) => ({
  //     ...prev,
  //     [connectionId]: { ...selectedBox, color },
  //   }));
  //   setContextMenu({ ...contextMenu, isVisible: false, id: "" });
  // };

  const handleKeyDown = useCallback(
    (event: KeyboardEvent) => {
      if (!connection || !selectedBoxes) return;

      const connectionId = connection.connectionId as string;
      if (!connectionId) return;

      const currentBox = selectedBoxes[connectionId] || { x: 0, y: 0 };
      let { x, y } = currentBox;

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

      if (!connectionId) return;

      const updatedSelectedBoxes = {
        ...selectedBoxes,
        [connectionId]: { x, y },
      };
      setSelectedBoxes(updatedSelectedBoxes);

      connection?.invoke("SendSelectedBoxes", groupName, updatedSelectedBoxes);
    },
    [connection, groupName, selectedBoxes]
  );

  useEffect(() => {
    if (connection) {
      connection.on(
        "ReceiveSelectedBoxes",
        (boxes: { [connectionId: string]: Coordinate }) => {
          setSelectedBoxes(boxes);
        }
      );

      connection.on("UpdateCursors", (cursors: Record<string, Coordinate>) => {
        const updatedCursors: { [connectionId: string]: CursorInfo } = {};
        Object.keys(cursors).forEach((connectionId) => {
          updatedCursors[connectionId] = {
            ...cursors[connectionId],
            userName: "User" + connectionId,
          };
        });
        setUserCursors(updatedCursors);
      });
    }
    return () => {
      if (connection) {
        connection.off("ReceiveSelectedBox");
        connection.off("UpdateCursors");
      }
    };
  }, [connection]);

  useEffect(() => {
    const handleMouseMove = (event: MouseEvent) => {
      if (!connection || !groupName) return;
      const canvas = canvasRef.current;
      if (!canvas) return;
      const rect = canvas.getBoundingClientRect();
      const x = event.clientX - rect.left - translatePos.x;
      const y = event.clientY - rect.top - translatePos.y;

      const gridX = Math.floor(x / GRID_SIZE);
      const gridY = Math.floor(y / GRID_SIZE);

      connection.invoke("SendCursorPosition", { x: gridX, y: gridY });
    };

    window.addEventListener("mousemove", handleMouseMove);
    return () => {
      window.removeEventListener("mousemove", handleMouseMove);
    };
  }, [connection, groupName, translatePos]);

  useEffect(() => {
    window.addEventListener("keydown", handleKeyDown);
    return () => window.removeEventListener("keydown", handleKeyDown);
  }, [handleKeyDown]);

  return (
    <CanvasContainer>
      <Canvas
        ref={canvasRef}
        width={INITIAL_WIDTH}
        height={INITIAL_HEIGHT}
        onClick={(event) => handleCanvasClick(event, 16, 9)}
        onContextMenu={handleCanvasRightClick}
      >
        Canvas
      </Canvas>
      {contextMenu.isVisible && (
        <VirtualBoardMenu
          position={{ x: contextMenu.x, y: contextMenu.y }}
          // onColorSelect={handleColorChange}
        />
      )}
    </CanvasContainer>
  );
}
