import React, {
  useEffect,
  useRef,
  useState,
  useCallback,
  useContext,
} from "react";
import styled from "styled-components";
import { debounce } from "lodash";
import {
  GRID_SIZE,
  INITIAL_HEIGHT,
  INITIAL_WIDTH,
  getColorForUser,
  drawGrid,
  drawFieldCross,
  drawCustomCursor,
  drawSelectedBox,
  fillSelectedBox,
  drawTextName,
  drawAvatar,
  drawWeaponAttackRange as drawAttackRange,
  checkIfTargetInWeaponAttackRange as checkIfTargetInAttackRange,
  getOccupiedCoordinatesForSize,
  drawSelectedTargetMarker,
  getCircleAreaCells,
  highlightArea,
  getAngleFromCharacterToCursor,
  getConeAreaCellsDeg,
  getCubeAreaCells,
  getLineAreaCells,
  getCylinderAreaCells,
  highlightPowerRange,
  drawSelectedPowerTargetsMarkers,
  arraysEqual,
} from "./CanvasUtils";
import { VirtualBoardProps } from "./../../../models/session/VirtualBoardProps";
import { Coordinate } from "../../../models/session/Coordinate";
import { ControlledCharacterContext } from "./context/ControlledCharacterContext";
import { useParticipanceData } from "../hooks/useParticipanceData";
import { size } from "../../effects/sizes";
import { AreaShapeLabels } from "../../powers/models/power";

const CanvasContainer = styled.div`
  display: flex;
  justify-content: center;
  align-items: center;
  overflow: auto;
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
  mode,
  dispatch,
  path,
  otherPath,
  weaponAttack,
  power,
  onWeaponAttackOverlay,
  selectedTargets,
}: VirtualBoardProps) {
  const canvasRef = useRef<HTMLCanvasElement | null>(null);
  const [selectedBoxes, setSelectedBoxes] = useState<{
    [connectionId: string]: Coordinate;
  }>({});
  const [userCursors, setUserCursors] = useState<{
    [connectionId: string]: CursorInfo;
  }>({});
  const [localCursorPosition, setLocalCursorPosition] = useState<Coordinate>({
    x: 0,
    y: 0,
  });
  const [selectedAreaPower, setSelectedAreaPower] = useState<Coordinate[]>([]);

  const [contextMenu, setContextMenu] = useState({
    x: 0,
    y: 0,
    isVisible: false,
    id: "",
  });
  const [selectedBox, setSelectedBox] = useState<Coordinate | null>(null);
  const [translatePos, setTranslatePos] = useState<Coordinate>({ x: 0, y: 0 });
  const [charactersIdsInPowerRange, setCharactersIdsInPowerRange] = useState<
    number[]
  >([]);
  const [controlledCharacterId] = useContext(ControlledCharacterContext);
  const { isLoading: isLoadingParticipanceData, participance } =
    useParticipanceData(encounter.id, controlledCharacterId);
  // const [targetId, setTargetId] = useState<number | null>(null);

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
  const ActiveCharacterSize: size = participance?.size!;
  const drawCanvas = useCallback(() => {
    const canvas = canvasRef.current;
    if (!canvas) return;
    const ctx = canvas.getContext("2d");
    if (!ctx) return;

    const width = canvas.width;
    const height = canvas.height;

    // Clear the canvas and redraw static elements
    ctx.clearRect(0, 0, width, height);
    drawGrid(ctx, width, height, sizeX, sizeY);

    if (encounter.board.fields) {
      encounter.board.fields.forEach(async (field) => {
        fillSelectedBox(
          ctx,
          field,
          encounter.board.sizeX,
          encounter.board.sizeY
        );

        if (field.fieldMovementCost === "Impassable") {
          drawFieldCross(
            ctx,
            field,
            encounter.board.sizeX,
            encounter.board.sizeY
          );
        }
      });

      encounter.board.fields.forEach(async (field) => {
        const matchingParticipance = encounter.participances?.find(
          (participance) => participance?.occupiedField?.id === field.id
        );

        if (matchingParticipance && field.fieldMovementCost !== "Impassable") {
          field.memberName = matchingParticipance.character.name;
          field.avatarUrl = matchingParticipance.character.isNpc
            ? "https://pbs.twimg.com/profile_images/1810521561352617985/ornocKLB_400x400.jpg"
            : "https://s3.amazonaws.com/files.d20.io/images/390056921/JkAY2BnZBWR-IYsYkqx3_Q/original.png";

          // Call drawAvatar and drawTextName separately, ensuring drawAvatar finishes first
          await drawAvatar(
            ctx,
            field,
            encounter.board.sizeX,
            encounter.board.sizeY,
            matchingParticipance.character.size.name
          );
          drawTextName(
            ctx,
            field,
            encounter.board.sizeX,
            encounter.board.sizeY,
            matchingParticipance.character.size.name
          );
        }
      });
    }
    let controlledCharacter = encounter.participances.find(
      (x) => x.character.id === controlledCharacterId
    );
    let occupiedField = controlledCharacter?.occupiedField;
    if (mode === "WeaponAttack") {
      let occupiedField = encounter.participances.find(
        (x) => x.character.id === controlledCharacterId
      )?.occupiedField;
      drawAttackRange(
        ctx,
        { x: occupiedField!.positionX, y: occupiedField!.positionY },
        weaponAttack.range,
        controlledCharacter?.character.size.name!,
        encounter.board.sizeX,
        encounter.board.sizeY
      );
    }
    if (mode === "PowerCast") {
      let newPowerArea: Coordinate[] = [];

      if (power.targetType === "Caster" || power.targetType == "Character") {
        let range = power.range ? Math.floor(power.range / 5) : 0;
        let areaSize = power.areaSize ? Math.floor(power.areaSize / 5) : 0;

        let casterPosition: Coordinate = {
          x: occupiedField!.positionX,
          y: occupiedField!.positionY,
        };

        switch (power.areaShape) {
          case AreaShapeLabels.Sphere:
            newPowerArea = getCircleAreaCells(
              localCursorPosition,
              casterPosition,
              areaSize,
              range
            );
            break;

          case AreaShapeLabels.Cone:
            let cursorAngle = 0;
            if (localCursorPosition) {
              cursorAngle = getAngleFromCharacterToCursor(
                casterPosition.x,
                casterPosition.y,
                localCursorPosition.x,
                localCursorPosition.y,
                encounter.board.sizeX,
                encounter.board.sizeY
              );
            }

            newPowerArea = getConeAreaCellsDeg(
              localCursorPosition,
              casterPosition,
              areaSize,
              range,
              cursorAngle,
              90
            );
            break;

          case AreaShapeLabels.Cube:
            newPowerArea = getCubeAreaCells(
              localCursorPosition,
              casterPosition,
              areaSize,
              range
            );
            break;

          case AreaShapeLabels.Line:
            let cursonLineAngle = 0;
            if (localCursorPosition) {
              cursonLineAngle = getAngleFromCharacterToCursor(
                casterPosition.x,
                casterPosition.y,
                localCursorPosition.x,
                localCursorPosition.y,
                encounter.board.sizeX,
                encounter.board.sizeY
              );
            }

            newPowerArea = getLineAreaCells(
              localCursorPosition,
              casterPosition,
              cursonLineAngle,
              areaSize,
              range
            );
            break;

          case AreaShapeLabels.Cylinder:
            newPowerArea = getCylinderAreaCells(
              localCursorPosition,
              casterPosition,
              areaSize,
              range
            );
            break;

          default:
            break;
        }

        setSelectedAreaPower(newPowerArea);
        highlightArea(
          ctx,
          newPowerArea,
          encounter.board.sizeX,
          encounter.board.sizeY
        );

        highlightPowerRange(
          ctx,
          casterPosition,
          range,
          encounter.board.sizeX,
          encounter.board.sizeY
        );
      }

      let newIdsInPowerRange: number[] = [];

      encounter.participances.forEach((participance) => {
        const occupiedField = participance.occupiedField;
        const targetedCharacter = participance.character;

        if (!occupiedField) return;

        let targetOccupiedCoordinates = getOccupiedCoordinatesForSize(
          occupiedField.positionX,
          occupiedField.positionY,
          targetedCharacter.size.name
        );

        const isInSelectedArea = targetOccupiedCoordinates.some((coord) =>
          newPowerArea.some((cell) => cell.x === coord.x && cell.y === coord.y)
        );

        if (isInSelectedArea) {
          drawSelectedPowerTargetsMarkers(
            ctx,
            { x: occupiedField.positionX, y: occupiedField.positionY },
            0,
            targetedCharacter.size.name,
            encounter.board.sizeX,
            encounter.board.sizeY
          );

          if (!newIdsInPowerRange.includes(targetedCharacter.id)) {
            newIdsInPowerRange.push(targetedCharacter.id);
          }
        }

        if (selectedTargets.find((x) => x === participance.character.id)) {
          drawSelectedTargetMarker(
            ctx,
            { x: occupiedField.positionX, y: occupiedField.positionY },
            0,
            targetedCharacter.size.name,
            encounter.board.sizeX,
            encounter.board.sizeY
          );
        }
      });

      setCharactersIdsInPowerRange((prev) => {
        // optional: only dispatch if changed to avoid unnecessary re-renders
        if (!arraysEqual(prev, newIdsInPowerRange)) {
          dispatch({
            type: "TOGGLE_POWER_TARGET_ARRAY",
            payload: newIdsInPowerRange,
          });
        }

        return newIdsInPowerRange;
      });
    }

    Object.keys(selectedBoxes).forEach((connectionId) => {
      const box = selectedBoxes[connectionId];
      const color = getColorForUser(connectionId);
      if (mode === "Movement" || otherPath.length > 0) {
        let localPath: number[] = [];
        if (otherPath.length > 0) {
          localPath = otherPath;
        } else if (path.length > 0) {
          localPath = path;
        }

        localPath.forEach((element) => {
          let field = encounter.board.fields.find(
            (field) => field.id === element
          );
          if (!!field) {
            drawSelectedBox(
              ctx,
              { x: field.positionX, y: field.positionY },
              sizeX,
              sizeY
            );
          }
        });
      } else {
        drawSelectedBox(ctx, box, sizeX, sizeY);
      }
    });
  }, [
    sizeX,
    sizeY,
    encounter.board.fields,
    encounter.board.sizeX,
    encounter.board.sizeY,
    encounter.participances,
    mode,
    selectedBoxes,
    weaponAttack?.range,
    controlledCharacterId,
    power?.targetType,
    selectedTargets,
    otherPath,
    path,
  ]);

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

      let selectedField = encounter.board.fields.find(
        (field) => field.positionX === gridX && field.positionY === gridY
      );
      if (mode === "Movement") {
        if (
          checkIfCanAddFieldToPath(
            selectedField!.positionX,
            selectedField!.positionY
          )
        ) {
          dispatch({
            type: "TOGGLE_PATH",
            payload: selectedField?.id as number,
          });
        }
      } else if (mode === "WeaponAttack") {
        let occupiedField = encounter.participances.find(
          (x) => x.character.id === controlledCharacterId
        )?.occupiedField;
        let clickedCharacter = encounter.participances.find((participance) => {
          let targetX = participance.occupiedField.positionX;
          let targetY = participance.occupiedField.positionY;
          let targetOccupiedCoordinates = getOccupiedCoordinatesForSize(
            targetX,
            targetY,
            participance.character.size.name
          );
          for (var targetOccupiedCoordinate of targetOccupiedCoordinates) {
            if (
              targetOccupiedCoordinate.x === selectedField?.positionX &&
              targetOccupiedCoordinate.y === selectedField.positionY
            ) {
              return true;
            }
          }
          return false;
        });
        if (!!clickedCharacter) {
          const inRange = checkIfTargetInAttackRange(
            occupiedField!.positionX,
            occupiedField!.positionY,
            ActiveCharacterSize,
            clickedCharacter!.occupiedField.positionX,
            clickedCharacter!.occupiedField.positionY,
            clickedCharacter!.character.size.name,
            weaponAttack.range
          );
          if (inRange) {
            console.log("In range");
            // setTargetId(clickedCharacter.character.id);
            onWeaponAttackOverlay(
              clickedCharacter.character.id,
              controlledCharacterId,
              encounter.campaign.id,
              weaponAttack.weaponId,
              weaponAttack.isRanged,
              weaponAttack.range
            );
          } else {
            console.log("Not in range");
          }
        }
      } else if (mode === "PowerCast") {
        let occupiedField = encounter.participances.find(
          (x) => x.character.id === controlledCharacterId
        )?.occupiedField;
        let clickedCharacter = encounter.participances.find((participance) => {
          let targetX = participance.occupiedField.positionX;
          let targetY = participance.occupiedField.positionY;
          let targetOccupiedCoordinates = getOccupiedCoordinatesForSize(
            targetX,
            targetY,
            participance!.character.size.name
          );
          for (var targetOccupiedCoordinate of targetOccupiedCoordinates) {
            if (
              targetOccupiedCoordinate.x === selectedField?.positionX &&
              targetOccupiedCoordinate.y === selectedField.positionY
            ) {
              return true;
            }
          }
          return false;
        });
        if (!!clickedCharacter) {
          const inRange = checkIfTargetInAttackRange(
            occupiedField!.positionX,
            occupiedField!.positionY,
            ActiveCharacterSize,
            clickedCharacter!.occupiedField.positionX,
            clickedCharacter!.occupiedField.positionY,
            clickedCharacter!.character.size.name,
            power.range ? power.range : 0
          );
          if (inRange) {
            console.log("In range");
            // setTargetId(clickedCharacter.character.id);
            dispatch({
              type: "TOGGLE_POWER_TARGET",
              payload: clickedCharacter.character.id,
            });
          } else {
            console.log("Not in range");
          }
        }
      } else {
        await connection.invoke(
          "SendSelectedBoxes",
          groupName,
          updatedSelectedBoxes
        );
      }
    },
    200
  );

  const checkIfCanAddFieldToPath = (gridX: number, gridY: number) => {
    let selectedField = encounter.board.fields.find(
      (field) => field.positionX === gridX && field.positionY === gridY
    );
    let lastField = encounter.board.fields.find(
      (field) => field.id === path[path.length - 1]
    );
    let occupiedField = encounter.participances.find(
      (x) => x.character.id === controlledCharacterId
    )?.occupiedField;
    let distanceX =
      (lastField?.positionX ?? -1) - (selectedField?.positionX ?? 1);
    let distanceY =
      (lastField?.positionY ?? -1) - (selectedField?.positionY ?? 1);
    let distanceFromStartX =
      (occupiedField?.positionX ?? -1) - (selectedField?.positionX ?? 1);
    let distanceFromStartY =
      (occupiedField?.positionY ?? -1) - (selectedField?.positionY ?? 1);
    let distanceFromLastInPathOk =
      Math.abs(distanceX) <= 1 && Math.abs(distanceY) <= 1;
    let distanceFromCurrentPositionOk =
      Math.abs(distanceFromStartX) === 1 || Math.abs(distanceFromStartY) === 1;
    let fieldAlreadyInPath = !!path.find((x) => x === selectedField?.id);
    return (
      !!selectedField &&
      !!participance &&
      ((((path.length === 0 && distanceFromCurrentPositionOk) ||
        distanceFromLastInPathOk) &&
        path.length * 5 <
          participance.totalMovement - participance.movementUsed) ||
        fieldAlreadyInPath)
    );
  };

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

      if (mode === "Movement") {
        let selectedField = encounter.board.fields.find(
          (field) => field.positionX === x && field.positionY === y
        );
        dispatch({ type: "TOGGLE_PATH", payload: selectedField?.id as number });
      } else {
        connection.invoke("SendSelectedBoxes", groupName, updatedSelectedBoxes);
      }
    },
    [
      connection,
      dispatch,
      encounter.board.fields,
      groupName,
      mode,
      path,
      selectedBoxes,
    ]
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
        connection.off("ReceiveSelectedBoxes");
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

      setLocalCursorPosition({ x: gridX, y: gridY });

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
      {/* {contextMenu.isVisible && (
        <VirtualBoardMenu
          position={{ x: contextMenu.x, y: contextMenu.y }}
          // onColorSelect={handleColorChange}
        />
      )} */}
    </CanvasContainer>
  );
}
