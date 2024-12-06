export const rollMoments = ["OnCast", "OnResolve"] as const;
export type rollMoment = (typeof rollMoments)[number];

export const RollMomentLabelMap = {
  OnCast: "On cast",
  OnResolve: "On resolve",
} as const;

export const skillsDropdown = rollMoments.map((x) => {
  return { value: x, label: RollMomentLabelMap[x] };
});
