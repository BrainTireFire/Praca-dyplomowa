import styled, { css } from "styled-components";
import Line from "../../../ui/separators/Line";
import Heading from "../../../ui/text/Heading";
import Box from "../../../ui/containers/Box";
import Button from "../../../ui/interactive/Button";
import { useState } from "react";
import Modal from "../../../ui/containers/Modal";
import CreateShop from "../../../features/campaigns/shop/CreateShop";
import Spinner from "../../../ui/interactive/Spinner";
import { useTranslation } from "react-i18next";
import SearchForm from "../../../features/campaigns/shop/SearchForm";
import ShopsTable from "../../../features/campaigns/shop/ShopsTable";
import { Shop } from "../../../models/shop";
import { useShops } from "../../../features/campaigns/shop/hooks/useShops";

const Container = styled.div`
  display: grid;
  grid-template-columns: 1fr;
  width: 100%;
  gap: 5%;
  justify-items: center;
  margin-top: 1%;
`;

const SearchFormContainer = styled.div`
  display: flex;
  justify-content: space-around;
  flex-direction: row;
`;

export default function Shops() {
  const { shops, isPending } = useShops();
  const { t } = useTranslation();
  const [searchInputs, setSearchInputs] = useState({
    name: "",
    type: "",
    location: "",
  });

  if (isPending) {
    return <Spinner />;
  }

  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    if (value.length < 3) {
      setSearchInputs((previous) => ({ ...previous, [name]: "" }));
      return;
    }
    setSearchInputs((previous) => ({ ...previous, [name]: value }));
  };

  const filterShopsData =
    shops?.filter((shop) =>
      Object.entries(searchInputs).every(([key, value]) =>
        shop[key as keyof Shop]
          ?.toString()
          .toLowerCase()
          .includes(value.toLowerCase())
      )
    ) || [];

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
