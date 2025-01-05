export const movementCosts = ["Normal", "High", "Impassable"] as const;

export type movementCost = (typeof movementCosts)[number];

export const MovementCostLabelMap = {
  Normal: "Normal",
  High: "High",
  Impassable: "Impassable",
} as const;

export const movementCostsDropdown = movementCosts.map((x) => {
  return { value: x, label: MovementCostLabelMap[x] };
});
