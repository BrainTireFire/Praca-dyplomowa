import EffectBlueprintForm, {
  initialState,
} from "../features/effects/EffectBlueprintForm";

export default function Effects() {
  return (
    <EffectBlueprintForm effectBlueprint={initialState}></EffectBlueprintForm>
  );
}
