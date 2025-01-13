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
  drawFieldCross,
  drawCustomCursor,
  drawSelectedBox,
  fillSelectedBox,
  drawTextName,
  drawAvatar,
} from "./CanvasUtils";
import { VirtualBoardProps } from "./../../../models/session/VirtualBoardProps";
import { Coordinate } from "../../../models/session/Coordinate";
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
  encounter,
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

  const sizeX = encounter.board.sizeX;
  const sizeY = encounter.board.sizeY;

  const drawCursors = useCallback(
    (ctx) => {
      Object.keys(userCursors).forEach((connectionId) => {
        const cursor = userCursors[connectionId];
        const cursorColor = getColorForUser(connectionId);

        drawCustomCursor(
          ctx,
          cursor.x,
          cursor.y,
          cursorColor,
          cursor.userName,
          sizeX,
          sizeY
        );
      });
    },
    [userCursors, sizeX, sizeY]
  );

  const drawCanvas = useCallback(() => {
    const canvas = canvasRef.current;
    if (!canvas) return;
    const ctx = canvas.getContext("2d");
    if (!ctx) return;

    const width = canvas.width;
    const height = canvas.height;

    // Clear the canvas and redraw static elements
    ctx.clearRect(0, 0, width, height);
    drawGrid(ctx, width, height, 16, 9);

    if (encounter.board.fields) {
      encounter.board.fields.forEach(async (field) => {
        fillSelectedBox(
          ctx,
          field,
          encounter.board.sizeX,
          encounter.board.sizeY
        );

        const matchingParticipance = encounter.participances?.find(
          (participance) => participance?.occupiedField?.id === field.id
        );

        if (matchingParticipance && field.fieldMovementCost !== "Impassable") {
          field.memberName = matchingParticipance.character.name;
          field.avatarUrl = matchingParticipance.character.isNpc
            ? "https://pbs.twimg.com/profile_images/1810521561352617985/ornocKLB_400x400.jpg"
            : "https://i1.sndcdn.com/avatars-000012078220-stfi4o-t1080x1080.jpg";

          // Call drawAvatar and drawTextName separately, ensuring drawAvatar finishes first
          await drawAvatar(
            ctx,
            field,
            encounter.board.sizeX,
            encounter.board.sizeY
          );
          drawTextName(
            ctx,
            field,
            encounter.board.sizeX,
            encounter.board.sizeY
          );
        }

        if (field.fieldMovementCost === "Impassable") {
          drawFieldCross(
            ctx,
            field,
            encounter.board.sizeX,
            encounter.board.sizeY
          );
        }
      });
    }

    Object.keys(selectedBoxes).forEach((connectionId) => {
      const box = selectedBoxes[connectionId];
      const color = getColorForUser(connectionId);
      drawSelectedBox(ctx, box, 16, 9);
    });
  }, [selectedBoxes, encounter]);

  useEffect(() => {
    const canvas = canvasRef.current;
    if (!canvas) return;
    const ctx = canvas.getContext("2d");
    if (!ctx) return;

    let animationFrameId;

    const render = () => {
      drawCanvas();
      drawCursors(ctx);
      animationFrameId = requestAnimationFrame(render);
    };

    render();

    return () => cancelAnimationFrame(animationFrameId);
  }, [drawCanvas, drawCursors]);

  const handleCanvasClick = debounce(
    async (event: React.MouseEvent<HTMLCanvasElement>) => {
      const canvas = canvasRef.current;
      if (!canvas || !connection || !groupName) return;
      const rect = canvas.getBoundingClientRect();
      const x = event.clientX - rect.left;
      const y = event.clientY - rect.top;

      const squareSize = Math.min(
        INITIAL_WIDTH / sizeX,
        INITIAL_HEIGHT / sizeY
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
    const x = event.clientX - rect.left;
    const y = event.clientY - rect.top;

    const squareSize = Math.min(INITIAL_WIDTH / sizeX, INITIAL_HEIGHT / sizeY);

    const gridX = Math.floor(x / squareSize);
    const gridY = Math.floor(y / squareSize);

    // Adjust the offset here
    const offsetX = -100; // Offset to the right
    const offsetY = -100; // Offset downwards\

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
      const x = event.clientX - rect.left;
      const y = event.clientY - rect.top;

      const squareSize = Math.min(
        INITIAL_WIDTH / sizeX,
        INITIAL_HEIGHT / sizeY
      );

      const gridX = Math.floor(x / squareSize);
      const gridY = Math.floor(y / squareSize);

      connection.invoke("SendCursorPosition", { x: gridX, y: gridY });
    };

    window.addEventListener("mousemove", handleMouseMove);
    return () => {
      window.removeEventListener("mousemove", handleMouseMove);
    };
  }, [connection, groupName, sizeX, sizeY]);

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
        onClick={(event) => handleCanvasClick(event)}
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
