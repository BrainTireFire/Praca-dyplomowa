export type ImmaterialResourceBlueprint = {
  id: number;
  name: string;
  refreshesOn: RefreshType;
};

export type ImmaterialResourceBlueprintWithOwner = {
  id: number | null;
  name: string;
  refreshesOn: RefreshType;
  ownerName: string;
  editable: boolean;
};

export const immaterialResourceInitialValue: ImmaterialResourceBlueprintWithOwner =
  {
    id: null,
    name: "New immaterial resource",
    refreshesOn: "TurnStart",
    ownerName: "",
    editable: true,
  };

export const refreshTypes = ["TurnStart", "ShortRest", "LongRest"] as const;
export type RefreshType = (typeof refreshTypes)[number];

export const refreshTypeLabel = Object.fromEntries(
  refreshTypes.map((type) => [type, type.replace(/([A-Z])/g, " $1").trim()])
);

export const refreshTypeOptions = refreshTypes.map((type) => ({
  value: type,
  label: refreshTypeLabel[type],
}));
