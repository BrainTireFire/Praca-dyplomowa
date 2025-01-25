CREATE OR REPLACE FUNCTION DeleteEffectGroupIfChildless()
RETURNS TRIGGER AS $$
BEGIN
    IF OLD."R_OwnedByGroupId" IS NOT NULL THEN
        IF NOT EXISTS (
            SELECT 1
            FROM public."EffectInstances"
            WHERE "R_OwnedByGroupId" = OLD."R_OwnedByGroupId"
        ) THEN
            DELETE FROM public."EffectGroups"
            WHERE "Id" = OLD."R_OwnedByGroupId";
        END IF;
    END IF;

    RETURN NULL;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER AfterEffectInstanceDelete
AFTER DELETE ON public."EffectInstances"
FOR EACH ROW
EXECUTE FUNCTION DeleteEffectGroupIfChildless();