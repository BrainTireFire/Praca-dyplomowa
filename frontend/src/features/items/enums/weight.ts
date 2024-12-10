export const weights = ["Heavy", "Normal", "Light"] as const;

export type weight = (typeof weights)[number];

export const weightsLabelMap = {
  Heavy: "Heavy",
  Normal: "Normal",
  Light: "Light",
} as const;

export const weightsDropdown = weights.map((x) => {
  return { value: x, label: weightsLabelMap[x] };
});
