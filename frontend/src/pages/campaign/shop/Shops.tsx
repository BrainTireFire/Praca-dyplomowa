import styled, { css } from "styled-components";
import Line from "../../../ui/separators/Line";
import Heading from "../../../ui/text/Heading";
import Box from "../../../ui/containers/Box";
import Button from "../../../ui/interactive/Button";
import { useState } from "react";
import Modal from "../../../ui/containers/Modal";
import CreateShop from "./CreateShop";
import { useCampaign } from "../../../features/campaigns/useCampaign";
import Spinner from "../../../ui/interactive/Spinner";
import { useTranslation } from "react-i18next";
import SearchForm from "../../../features/campaigns/shop/SearchForm";
import ShopsTable from "../../../features/campaigns/shop/ShopsTable";

const Container = styled.div`
  display: flex;
  flex-direction: column;
  justify-content: center;
  gap: 20px;
  align-items: center;
`;

const SearchFormContainer = styled.div`
  display: flex;
  justify-content: space-around;
  flex-direction: row;
`;

export default function Shops() {
  const { isLoading, campaign } = useCampaign();
  const { t } = useTranslation();
  const [searchInputs, setSearchInputs] = useState({
    name: "",
    type: "",
    location: "",
  });

  if (isLoading) {
    return <Spinner />;
  }

  if (!campaign) {
    return <div>{t("campaign.error.notFound")}</div>;
  }

  const { shops }: Campaign = campaign;

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    if (value.length < 3) {
      setSearchInputs((previous) => ({ ...previous, [name]: "" }));
      return;
    }
    setSearchInputs((previous) => ({ ...previous, [name]: value }));
  };

  const filterShopsData = shops.filter((shop) => {
    return Object.keys(searchInputs).every((key) =>
      shop[key].toLowerCase().includes(searchInputs[key].toLowerCase())
    );
  });

  return (
    <Container>
      <Heading as="h1">Shops</Heading>
      <Line size="percantage" bold="large" />
      <Box style={{ width: "70%" }}>
        <SearchFormContainer>
          <SearchForm onInputChange={handleInputChange} />
        </SearchFormContainer>
      </Box>
      <ShopsTable shops={filterShopsData}></ShopsTable>
      <Modal>
        <Modal.Open opens="CreateShop">
          <Button style={{ width: "200px" }}>Create new Shop</Button>
        </Modal.Open>
        <Modal.Window name="CreateShop">
          <CreateShop />
        </Modal.Window>
      </Modal>
    </Container>
  );
}