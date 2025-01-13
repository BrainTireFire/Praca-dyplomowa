const getCssVariable = (variableName: string): string => {
  return getComputedStyle(document.documentElement)
    .getPropertyValue(variableName)
    .trim();
};

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

export const drawCustomCursor = (
  ctx: CanvasRenderingContext2D,
  x: number,
  y: number,
  color: string,
  userName: string,
  columns: number,
  rows: number
) => {
  const squareSize = Math.min(INITIAL_WIDTH / columns, INITIAL_HEIGHT / rows);

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

export const drawSelectedBox = (
  ctx: CanvasRenderingContext2D,
  selectedBox: { x: number; y: number } | null,
  columns: number,
  rows: number
) => {
  if (!selectedBox) return;

  const squareSize = Math.min(INITIAL_WIDTH / columns, INITIAL_HEIGHT / rows);

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

export const drawSelectedBoxes = (
  ctx: CanvasRenderingContext2D,
  selectedBoxes: { x: number; y: number }[], // Array of selected boxes
  columns: number,
  rows: number
) => {
  if (!selectedBoxes.length) return; // No boxes to draw

  const squareSize = Math.min(INITIAL_WIDTH / columns, INITIAL_HEIGHT / rows);

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

  const squareSize = Math.min(INITIAL_WIDTH / columns, INITIAL_HEIGHT / rows);

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

  const squareSize = Math.min(INITIAL_WIDTH / columns, INITIAL_HEIGHT / rows);

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
  rows: number
) => {
  if (!field.avatarUrl) return;

  const squareSize = Math.min(INITIAL_WIDTH / columns, INITIAL_HEIGHT / rows);
  const avatarSize = squareSize * 0.6;

  const avatarX =
    field.positionX * squareSize + squareSize / 2 - avatarSize / 2;
  const avatarY =
    field.positionY * squareSize + squareSize / 2 - avatarSize / 1.4;

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
  rows: number
) => {
  if (!field.memberName) return;

  const squareSize = Math.min(INITIAL_WIDTH / columns, INITIAL_HEIGHT / rows);

  ctx.fillStyle = "white";
  ctx.font = `${squareSize * 0.2}px Poppins`;

  const textWidth = ctx.measureText(field.memberName).width;

  const textX = field.positionX * squareSize + squareSize / 2 - textWidth / 2;
  const textY =
    field.positionY * squareSize + squareSize / 2 + (squareSize * 0.6) / 1.5;

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

  const squareSize = Math.min(INITIAL_WIDTH / columns, INITIAL_HEIGHT / rows);

  const squareX = field.positionX * squareSize;
  const squareY = field.positionY * squareSize;

  const padding = squareSize * 0.03;

  ctx.save();

  ctx.strokeStyle = "red";
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
