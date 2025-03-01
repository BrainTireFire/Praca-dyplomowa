import React from "react";
import EncounterMapCreaterLayout from "../session/EncounterMapCreaterLayout";
import { useParams } from "react-router-dom";

export default function EncounterEditForm() {
  const { encounterId } = useParams<{ encounterId: string }>();

  if (!encounterId) {
    return null;
  }

  return <EncounterMapCreaterLayout encounterId={parseInt(encounterId)} />;
}
