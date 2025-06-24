import { Coordinate } from "../../../models/session/Coordinate";
import { size } from "../../effects/sizes";

const getCssVariable = (variableName: string): string => {
  return getComputedStyle(document.documentElement)
    .getPropertyValue(variableName)
    .trim();
};

const getSquareSize = (columns: number, rows: number) =>
  Math.min(INITIAL_WIDTH / columns, INITIAL_HEIGHT / rows);

const getDistanceCircle = (a: Coordinate, b: Coordinate) =>
  Math.sqrt((a.x - b.x) ** 2 + (a.y - b.y) ** 2);

const getDistance = (a: Coordinate, b: Coordinate) =>
  Math.max(Math.abs(a.x - b.x), Math.abs(a.y - b.y));

function normalizeAngle(angle: number): number {
  while (angle <= -Math.PI) angle += 2 * Math.PI;
  while (angle > Math.PI) angle -= 2 * Math.PI;
  return angle;
}

export function arraysEqual(a: number[], b: number[]) {
  if (a.length !== b.length) return false;
  const sortedA = [...a].sort((x, y) => x - y);
  const sortedB = [...b].sort((x, y) => x - y);
  return sortedA.every((val, index) => val === sortedB[index]);
}

export const GRID_SIZE = 70;
export const INITIAL_WIDTH = 1280;
export const INITIAL_HEIGHT = 720;
export const CURSOR_SIZE = 10;

export const getColorForUser = (connectionId: string) => {
  // Example function to assign colors
  const colors = [
    "#FF0000",
    "#00FF00",
    "#0000FF",
    "#FFFF00",
    "#FF00FF",
    "#00FFFF",
  ];
  const index = parseInt(connectionId, 36) % colors.length;
  return colors[index];
};

export const drawGrid = (
  ctx: CanvasRenderingContext2D,
  width: number,
  height: number,
  columns?: number = 16,
  rows?: number = 9
) => {
  ctx.save();

  const squareSize = Math.min(width / columns, height / rows);

  ctx.strokeStyle = getCssVariable("--color-border");
  ctx.lineWidth = 1;

  for (let x = 0; x <= width; x += squareSize) {
    ctx.beginPath();
    ctx.moveTo(x, 0);
    ctx.lineTo(x, height);
    ctx.stroke();
  }

  for (let y = 0; y <= height; y += squareSize) {
    ctx.beginPath();
    ctx.moveTo(0, y);
    ctx.lineTo(width, y);
    ctx.stroke();
  }

  ctx.restore();
};

export const highlightBox = (
  ctx: CanvasRenderingContext2D,
  x: number,
  y: number,
  color: string
) => {
  ctx.save();
  ctx.strokeStyle = color;
  ctx.lineWidth = 2;
  ctx.strokeRect(x * GRID_SIZE, y * GRID_SIZE, GRID_SIZE, GRID_SIZE);
  ctx.restore();
};

export const highlightArea = (
  ctx: CanvasRenderingContext2D,
  cells: Coordinate[],
  columns: number,
  rows: number
) => {
  const squareSize = getSquareSize(columns, rows);

  ctx.save();
  ctx.strokeStyle = getCssVariable("--color-button-hover-danger");
  ctx.lineWidth = 2;

  for (const cell of cells) {
    ctx.strokeRect(
      cell.x * squareSize,
      cell.y * squareSize,
      squareSize,
      squareSize
    );
  }

  ctx.restore();
};

export const highlightPowerRange = (
  ctx: CanvasRenderingContext2D,
  center: Coordinate,
  range: number,
  columns: number,
  rows: number
) => {
  const squareSize = getSquareSize(columns, rows);

  const size = (range * 2 + 1) * squareSize;
  const topLeftX = (center.x - range) * squareSize;
  const topLeftY = (center.y - range) * squareSize;

  ctx.save();
  ctx.strokeStyle = getCssVariable("--color-input-focus");
  ctx.lineWidth = 2;

  ctx.strokeRect(topLeftX, topLeftY, size, size);

  ctx.restore();
};

export const highlightPowerCircleRange = (
  ctx: CanvasRenderingContext2D,
  center: Coordinate,
  range: number,
  columns: number,
  rows: number
) => {
  const squareSize = getSquareSize(columns, rows);

  // Oblicz środek w pikselach
  const centerX = (center.x + 0.5) * squareSize;
  const centerY = (center.y + 0.5) * squareSize;

  // Przelicz promień z jednostek siatki na piksele
  const radiusInPixels = range * squareSize;

  ctx.save();
  ctx.strokeStyle = getCssVariable("--color-input-focus");
  ctx.lineWidth = 2;

  ctx.beginPath();
  ctx.arc(centerX, centerY, radiusInPixels, 0, Math.PI * 2);
  ctx.stroke();

  ctx.restore();
};

export const drawCustomCursor = (
  ctx: CanvasRenderingContext2D,
  x: number,
  y: number,
  color: string,
  userName: string,
  columns: number,
  rows: number
) => {
  const squareSize = getSquareSize(columns, rows);

  // Calculate the center position of the grid cell where the mouse is
  const centerX = x * squareSize + squareSize / 2;
  const centerY = y * squareSize + squareSize / 2;

  // Draw custom cursor circle centered in the grid box
  ctx.save();
  ctx.fillStyle = color;
  ctx.beginPath();
  ctx.arc(centerX, centerY, CURSOR_SIZE, 0, 2 * Math.PI);
  ctx.closePath();
  ctx.fill();
  ctx.restore();

  // Draw user name above the cursor, centered
  ctx.save();
  ctx.fillStyle = color;
  ctx.font = "12px Poppins";
  const textWidth = ctx.measureText(userName).width;
  const textX = centerX - textWidth / 2; // Center the text horizontally
  const textY = centerY - CURSOR_SIZE - 5; // Position text just above the cursor

  ctx.fillText(userName, textX, textY);
  ctx.restore();
};

export const getCircleAreaCells = (
  cursorPressPosition: Coordinate,
  casterPosition: Coordinate,
  areaSize: number,
  range: number
): Coordinate[] => {
  const cells: Coordinate[] = [];

  const inRangeX = Math.abs(cursorPressPosition.x - casterPosition.x) <= range;
  const inRangeY = Math.abs(cursorPressPosition.y - casterPosition.y) <= range;

  if (!inRangeX || !inRangeY) {
    return [];
  }

  for (let dx = -areaSize; dx <= areaSize; dx++) {
    for (let dy = -areaSize; dy <= areaSize; dy++) {
      const x = cursorPressPosition.x + dx;
      const y = cursorPressPosition.y + dy;

      // Sprawdź dystans do środka pola (środek siatki w kwadracie)
      const distance = Math.sqrt(dx * dx + dy * dy);

      if (distance + 0.5 <= areaSize) {
        cells.push({ x, y });
      }
    }
  }

  return cells;
};

export const getCubeAreaCells = (
  cursorPressPosition: Coordinate,
  casterPosition: Coordinate,
  areaSize: number,
  range: number
): Coordinate[] => {
  const cells: Coordinate[] = [];
  const half = Math.floor(areaSize / 2);

  if (getDistance(cursorPressPosition, casterPosition) > range) {
    return [];
  }

  for (let dx = -half; dx <= half; dx++) {
    for (let dy = -half; dy <= half; dy++) {
      const x = cursorPressPosition.x + dx;
      const y = cursorPressPosition.y + dy;

      cells.push({ x, y });
    }
  }

  return cells;
};

export const getCylinderAreaCells = (
  cursorPosition: Coordinate,
  casterPosition: Coordinate,
  areaSize: number,
  range: number
): Coordinate[] => {
  const cells: Coordinate[] = [];

  if (getDistance(cursorPosition, casterPosition) > range) {
    return [];
  }

  for (let dx = -areaSize; dx <= areaSize; dx++) {
    for (let dy = -areaSize; dy <= areaSize; dy++) {
      const x = cursorPosition.x + dx;
      const y = cursorPosition.y + dy;
      const dist = Math.sqrt(dx * dx + dy * dy);

      if (dist <= areaSize) {
        cells.push({ x, y });
      }
    }
  }

  return cells;
};

export const getLineAreaCells = (
  cursorPosition: Coordinate,
  casterPosition: Coordinate,
  directionDeg: number,
  areaSize: number,
  range: number
): Coordinate[] => {
  const cells: Coordinate[] = [];

  if (getDistance(cursorPosition, casterPosition) > range) {
    return [];
  }

  const angleRad = (directionDeg * Math.PI) / 180;

  for (let i = 1; i <= areaSize; i++) {
    const dx = Math.round(Math.cos(angleRad) * i);
    const dy = Math.round(Math.sin(angleRad) * i);
    const x = casterPosition.x + dx;
    const y = casterPosition.y + dy;

    cells.push({ x, y });
  }

  return cells;
};

export function getConeAreaCellsDeg(
  cursorPosition: Coordinate,
  casterPosition: Coordinate,
  areaSize: number,
  range: number,
  directionDeg: number,
  coneWidthDeg: number
): Coordinate[] {
  const cells: Coordinate[] = [];

  if (getDistance(cursorPosition, casterPosition) > range) {
    return [];
  }

  for (let dx = -areaSize; dx <= areaSize; dx++) {
    for (let dy = -areaSize; dy <= areaSize; dy++) {
      const x = casterPosition.x + dx;
      const y = casterPosition.y + dy;

      const dist = Math.sqrt(dx * dx + dy * dy);
      if (dist > areaSize || (dx === 0 && dy === 0)) continue;

      const angleRad = Math.atan2(dy, dx);
      let angleDeg = (angleRad * 180) / Math.PI;
      if (angleDeg < 0) angleDeg += 360;

      let diff = Math.abs(angleDeg - directionDeg);
      if (diff > 180) diff = 360 - diff;

      if (diff <= coneWidthDeg / 2) {
        cells.push({ x, y });
      }
    }
  }

  return cells;
}

export function getAngleFromCharacterToCursor(
  charX: number,
  charY: number,
  cursorX: number,
  cursorY: number,
  columns: number,
  rows: number
): number {
  const squareSize = getSquareSize(columns, rows);

  // Przeliczanie kursora na piksele (jeśli przekazujemy je w jednostkach kratkowych)
  const cursorXInPixels = cursorX * squareSize;
  const cursorYInPixels = cursorY * squareSize;

  // Obliczanie środka postaci w pikselach
  const charCenterX = (charX + 0.5) * squareSize;
  const charCenterY = (charY + 0.5) * squareSize;

  const dx = cursorXInPixels - charCenterX;
  const dy = cursorYInPixels - charCenterY;

  // Obliczanie kąta w radianach
  const angleRad = Math.atan2(dy, dx);

  // Przekształcenie na stopnie
  let angleDeg = (angleRad * 180) / Math.PI;

  // Upewnienie się, że kąt jest w zakresie [0, 360)
  if (angleDeg < 0) angleDeg += 360;

  return angleDeg;
}

export const drawSelectedBox = (
  ctx: CanvasRenderingContext2D,
  selectedBox: Coordinate | null,
  columns: number,
  rows: number
) => {
  if (!selectedBox) return;

  const squareSize = getSquareSize(columns, rows);

  ctx.save();
  ctx.strokeStyle = getCssVariable("--color-link");
  ctx.lineWidth = 2;

  // Draw the selected box at the correct position and size
  ctx.strokeRect(
    selectedBox.x * squareSize, // X position of the selected square
    selectedBox.y * squareSize, // Y position of the selected square
    squareSize, // Width of the square
    squareSize // Height of the square
  );

  ctx.restore();
};

export const drawWeaponAttackRange = (
  ctx: CanvasRenderingContext2D,
  origin: Coordinate,
  range: number,
  characterSize: size,
  columns: number,
  rows: number
) => {
  drawBoundingBoxOverCharacter(
    ctx,
    origin,
    range,
    characterSize,
    columns,
    rows,
    "--color-input-text",
    2
  );
};

export const drawSelectedTargetMarker = (
  ctx: CanvasRenderingContext2D,
  origin: Coordinate,
  range: number,
  characterSize: size,
  columns: number,
  rows: number
) => {
  drawBoundingBoxOverCharacter(
    ctx,
    origin,
    range,
    characterSize,
    columns,
    rows,
    "--color-form-error",
    10
  );
};

export const drawSelectedPowerTargetsMarkers = (
  ctx: CanvasRenderingContext2D,
  origin: Coordinate,
  range: number,
  characterSize: size,
  columns: number,
  rows: number
) => {
  drawBoundingBoxOverCharacter(
    ctx,
    origin,
    range,
    characterSize,
    columns,
    rows,
    "--color-form-error",
    10
  );
};

export const drawBoundingBoxOverCharacter = (
  ctx: CanvasRenderingContext2D,
  origin: Coordinate,
  range: number,
  characterSize: size,
  columns: number,
  rows: number,
  color: string,
  lineWidth: number
) => {
  const squareSize = getSquareSize(columns, rows);

  ctx.save();
  ctx.strokeStyle = getCssVariable(color);
  ctx.lineWidth = lineWidth;

  let originOccupiedCoordinates = getOccupiedCoordinatesForSize(
    origin.x,
    origin.y,
    characterSize
  );

  let paddedOriginOccupiedCoordinates = padOccupiedCoordinates(
    originOccupiedCoordinates,
    range / 5
  );

  paddedOriginOccupiedCoordinates.forEach((box) => {
    ctx.strokeRect(
      box.x * squareSize, // X position of the selected square
      box.y * squareSize, // Y position of the selected square
      squareSize, // Width of the square
      squareSize // Height of the square
    );
  });

  ctx.restore();
};

export const getSizeMultiplier = (size: size) => {
  if (size === "Tiny") {
    return 1;
  }
  if (size === "Small") {
    return 1;
  }
  if (size === "Medium") {
    return 1;
  }
  if (size === "Large") {
    return 2;
  }
  if (size === "Huge") {
    return 3;
  }
  if (size === "Gargantuan") {
    return 4;
  }
  return 1;
};

export const checkIfTargetInWeaponAttackRange = (
  originX: number,
  originY: number,
  originSize: size,
  targetX: number,
  targetY: number,
  targetSize: size,
  range: number
) => {
  let originOccupiedCoordinates = getOccupiedCoordinatesForSize(
    originX,
    originY,
    originSize
  );
  let paddedOriginOccupiedCoordinates = padOccupiedCoordinates(
    originOccupiedCoordinates,
    range / 5
  );
  let targetOccupiedCoordinates = getOccupiedCoordinatesForSize(
    targetX,
    targetY,
    targetSize
  );

  for (let originCoord of paddedOriginOccupiedCoordinates) {
    for (let targetCoord of targetOccupiedCoordinates) {
      if (originCoord.x === targetCoord.x && originCoord.y === targetCoord.y) {
        return true;
      }
    }
  }

  return false;
};

export const getOccupiedCoordinatesForSize = (
  x: number,
  y: number,
  size: size
) => {
  let multiplier = getSizeMultiplier(size);
  let occupiedCoordinates = [];
  for (let i = 0; i < multiplier; i++) {
    for (let j = 0; j < multiplier; j++) {
      occupiedCoordinates.push({ x: x + i, y: y + j });
    }
  }
  return occupiedCoordinates;
};

const padOccupiedCoordinates = (
  occupiedCoordinates: Coordinate[],
  padding: number
) => {
  let paddedCoordinates = new Set<string>();

  occupiedCoordinates.forEach(({ x, y }) => {
    for (let dx = -padding; dx <= padding; dx++) {
      let paddedCoordinatesLocal = new Set<string>();
      for (let dy = -padding; dy <= padding; dy++) {
        paddedCoordinates.add(`${x + dx},${y + dy}`);
        paddedCoordinatesLocal.add(`${x + dx},${y + dy}`);
      }
    }
  });

  // Convert the set of strings back to an array of coordinate objects
  return Array.from(paddedCoordinates).map((coord) => {
    const [x, y] = coord.split(",").map(Number);
    return { x, y };
  });
};

export const drawSelectedBoxes = (
  ctx: CanvasRenderingContext2D,
  selectedBoxes: Coordinate[], // Array of selected boxes
  columns: number,
  rows: number
) => {
  if (!selectedBoxes.length) return; // No boxes to draw

  const squareSize = getSquareSize(columns, rows);

  ctx.save();
  ctx.strokeStyle = getCssVariable("--color-link");
  ctx.lineWidth = 2;

  // Iterate over each selected box and draw it
  selectedBoxes.forEach((box) => {
    ctx.strokeRect(
      box.x * squareSize, // X position of the selected square
      box.y * squareSize, // Y position of the selected square
      squareSize, // Width of the square
      squareSize // Height of the square
    );
  });

  ctx.restore();
};

export const drawFieldBoxWithText = (
  ctx: CanvasRenderingContext2D,
  field: {
    positionX: number;
    positionY: number;
    color: string;
    memberName?: string;
  },
  columns: number,
  rows: number
) => {
  if (!ctx || !field) {
    return;
  }

  const squareSize = getSquareSize(columns, rows);

  ctx.save();

  // Fill the box with the provided color

  ctx.fillStyle = field.color;
  ctx.fillRect(
    field.positionX * squareSize, // X position of the field
    field.positionY * squareSize, // Y position of the field
    squareSize, // Width of the field
    squareSize // Height of the field
  );

  // Apply the border (stroke)
  ctx.strokeStyle = getCssVariable("--color-border");
  ctx.lineWidth = 1; // Set the width of the border
  ctx.strokeRect(
    field.positionX * squareSize, // X position of the field
    field.positionY * squareSize, // Y position of the field
    squareSize, // Width of the field
    squareSize // Height of the field
  );

  // Draw the text if `memberName` is provided
  if (field.memberName) {
    ctx.fillStyle = "white"; // Text color
    ctx.font = "12px Poppins"; // Font size and style

    // Measure the width of the text
    const textWidth = ctx.measureText(field.memberName).width;

    // Position the text in the center of the field
    const textX = field.positionX * squareSize + squareSize / 2 - textWidth / 2; // Center horizontally
    const textY = field.positionY * squareSize + squareSize / 2 + 4; // Center vertically

    // Draw the text
    ctx.fillText(field.memberName, textX, textY);
  }

  ctx.restore();
};

export const fillSelectedBox = (
  ctx: CanvasRenderingContext2D,
  field: {
    positionX: number;
    positionY: number;
    color: string;
    memberName?: string;
  },
  columns: number,
  rows: number
) => {
  if (!ctx || !field) {
    return;
  }

  const squareSize = getSquareSize(columns, rows);

  ctx.save();

  // Fill the box with the provided color

  ctx.fillStyle = field.color;
  ctx.fillRect(
    field.positionX * squareSize, // X position of the field
    field.positionY * squareSize, // Y position of the field
    squareSize, // Width of the field
    squareSize // Height of the field
  );

  // Apply the border (stroke)
  ctx.strokeStyle = getCssVariable("--color-border");
  ctx.lineWidth = 1; // Set the width of the border
  ctx.strokeRect(
    field.positionX * squareSize, // X position of the field
    field.positionY * squareSize, // Y position of the field
    squareSize, // Width of the field
    squareSize // Height of the field
  );

  ctx.restore();
};

const avatarCache: { [url: string]: HTMLImageElement } = {};

export const drawAvatar = async (
  ctx: CanvasRenderingContext2D,
  field: {
    positionX: number;
    positionY: number;
    avatarUrl?: string;
  },
  columns: number,
  rows: number,
  characterSize: size
) => {
  if (!field.avatarUrl) return;
  const numericCharacterSize = getSizeMultiplier(characterSize);
  const squareSize = getSquareSize(columns, rows);
  const avatarSize = squareSize * numericCharacterSize;
  const avatarX = field.positionX * squareSize;
  const avatarY = field.positionY * squareSize;

  // Check if the image is already in cache
  let avatarImage = avatarCache[field.avatarUrl];

  if (!avatarImage) {
    // If not cached, load the image
    avatarImage = new Image();
    avatarImage.src = field.avatarUrl;

    // Cache the image when it is loaded
    avatarImage.onload = () => {
      avatarCache[field.avatarUrl] = avatarImage;
    };
  }

  // If the image is cached or has just loaded, draw it
  if (avatarImage.complete) {
    ctx.save();

    ctx.beginPath();
    ctx.arc(
      avatarX + avatarSize / 2,
      avatarY + avatarSize / 2,
      avatarSize / 2,
      0,
      Math.PI * 2
    );
    ctx.clip();
    ctx.drawImage(avatarImage, avatarX, avatarY, avatarSize, avatarSize);

    ctx.restore();
  }
};

export const drawTextName = (
  ctx: CanvasRenderingContext2D,
  field: {
    positionX: number;
    positionY: number;
    memberName?: string;
  },
  columns: number,
  rows: number,
  characterSize: size
) => {
  if (!field.memberName) return;

  const numericCharacterSize = getSizeMultiplier(characterSize);
  const squareSize = getSquareSize(columns, rows);

  ctx.fillStyle = getCssVariable("--color-text-name-canvas");
  ctx.font = `${squareSize * 0.3}px Poppins`;

  const textWidth = ctx.measureText(field.memberName).width;

  const textX =
    field.positionX * squareSize +
    (squareSize / 2) * numericCharacterSize -
    textWidth / 2;
  const textY =
    field.positionY * squareSize +
    (squareSize / 2) * numericCharacterSize +
    (squareSize * 0.6) / 1.5;

  ctx.fillText(field.memberName, textX, textY);

  ctx.restore();
};

export const drawFieldCross = (
  ctx: CanvasRenderingContext2D,
  field: {
    positionX: number;
    positionY: number;
    color: string;
  },
  columns: number,
  rows: number
) => {
  if (!ctx || !field) {
    return;
  }

  const squareSize = getSquareSize(columns, rows);

  const squareX = field.positionX * squareSize;
  const squareY = field.positionY * squareSize;

  const padding = squareSize * 0.03;

  ctx.save();

  ctx.strokeStyle = getCssVariable("--color-form-error");
  ctx.lineWidth = squareSize * 0.04;

  ctx.beginPath();
  ctx.moveTo(squareX + padding, squareY + padding);
  ctx.lineTo(squareX + squareSize - padding, squareY + squareSize - padding);
  ctx.stroke();

  ctx.beginPath();
  ctx.moveTo(squareX + squareSize - padding, squareY + padding);
  ctx.lineTo(squareX + padding, squareY + squareSize - padding);
  ctx.stroke();

  ctx.restore();
};
