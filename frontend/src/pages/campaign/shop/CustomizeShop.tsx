import Heading from "../../../ui/text/Heading";
import { useCampaign } from "../../../features/campaigns/useCampaign";
import { useParams } from "react-router-dom";
import Spinner from "../../../ui/interactive/Spinner";
import { useTranslation } from "react-i18next";

function CustomizeShop() {
  const { isLoading, campaign } = useCampaign();
  const { t } = useTranslation();
  const { shopId } = useParams<{ shopId: string }>();
  if (isLoading) {
    return <Spinner />;
  }

  if (!campaign) {
    return <div>{t("campaign.error.notFound")}</div>;
  }
  const { shops } = campaign;
  const shop = shops[shopId - 1];
  return (
    <Heading as="h1">
      Shop #{shop.id}
      <br />
      {shop.name}
    </Heading>
  );
}

export default CustomizeShop;
