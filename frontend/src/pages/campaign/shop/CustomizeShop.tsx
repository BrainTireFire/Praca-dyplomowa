import Heading from "../../../ui/text/Heading";
import { useParams } from "react-router-dom";
import Spinner from "../../../ui/interactive/Spinner";
import { useTranslation } from "react-i18next";
import { useShop } from "../../../features/campaigns/shop/hooks/useShop";

function CustomizeShop() {
  const { shop, isPending } = useShop();
  const { t } = useTranslation();

  if (isPending) {
    return <Spinner />;
  }

  if (!shop) {
    return <div>shop not found</div>;
  }

  return (
    <Heading as="h1">
      Shop #{shop.id}
      <br />
      {shop.name}
    </Heading>
  );
}

export default CustomizeShop;
