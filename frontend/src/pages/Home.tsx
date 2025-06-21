import { useTranslation } from "react-i18next";
import styled, { css } from "styled-components";
import Button from "../ui/interactive/Button";
import { Link } from "react-router-dom";
import Heading from "../ui/text/Heading";
import Box from "../ui/containers/Box";
import { FaGithub } from "react-icons/fa";
import { IoMail } from "react-icons/io5";
import Logo from "../ui/interactive/Logo";
import { useNavigate } from "react-router-dom";

const StyledHome = styled.div`
  display: flex;
  flex-direction: column;
  gap: 1rem;
  padding: 4rem;
`;

const ButtonContainer = styled.div`
  display: flex;
  justify-content: end;
  gap: 2rem;
`;

const ButtonContainerPlayNow = styled.div`
  display: flex;
  justify-content: start;
  padding-top: 2rem;
`;

const HeaderParagraph = styled.p`
  font-size: 1.2em;
  font-weight: bold;
`;

const BodyParagraph = styled.p`
  font-size: 3em;
  font-weight: bold;
`;

const DevelopersContainer = styled.div`
  display: flex;
  justify-content: center;
  gap: 2rem;
  padding-top: 2rem;
`;

const PersonContainer = styled.div`
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  gap: 2rem;
`;

const LogoContainer = styled.div`
  width: 300px;
  height: 120px;
  display: flex;
  align-items: center;
  justify-content: center;

  img {
    max-width: 100%;
    max-height: 100%;
    object-fit: contain;
  }
`;

export default function Home() {
  const navigate = useNavigate();
  const { t } = useTranslation();

  return (
    <StyledHome>
      <ButtonContainer>
        <Button size="large" onClick={() => navigate(`/login`)}>
          {t("login.text")}
        </Button>
        <Button size="large" onClick={() => navigate(`/register`)}>
          {t("signup.text")}
        </Button>
      </ButtonContainer>
      <Heading as="h12" align="left" color="textColor">
        <LogoContainer>
          <img src="logo.png" alt="Logo" />
        </LogoContainer>
      </Heading>
      <HeaderParagraph>PJATK students present</HeaderParagraph>
      <BodyParagraph>
        A breakthrough of a tool for a Dungeons & Dragons game
      </BodyParagraph>
      <p>
        A tool designed from scratch for D&D players - by D&D players.
        Playability is key!
      </p>
      <ButtonContainerPlayNow>
        <Button size="verylarge">
          <Link to="/main">{t("homepage.play.now.button")}</Link>
        </Button>
      </ButtonContainerPlayNow>
      <Heading as="h8" align="center" color="textColor">
        {t("homepage.developers")}
      </Heading>

      <DevelopersContainer>
        <Box
          customStyles={css`
            display: flex;
            align-items: center;
            justify-content: center;
            gap: 2rem;
          `}
        >
          <PersonContainer>
            <Logo src="maciekGithub.png" alt="Maciek R" />
            <Heading as="h5" align="center" color="textColor">
              Maciej Romaniuk
            </Heading>
            <Box
              customStyles={css`
                display: flex;
                flex-direction: column;
                align-items: left;
                justify-content: start;
                padding: 2rem 1rem;
                width: 300px;
                height: 150px;
                gap: 2rem;
              `}
            >
              <div
                style={{ display: "flex", alignItems: "center", gap: "1rem" }}
              >
                <FaGithub style={{ fontSize: "3em" }} />
                <Link
                  to="https://github.com/Lokk3n"
                  style={{ display: "inline-block", verticalAlign: "middle" }}
                >
                  https://github.com/Lokk3n
                </Link>
              </div>
              <div
                style={{ display: "flex", alignItems: "center", gap: "1rem" }}
              >
                <IoMail style={{ fontSize: "3em" }} />
                <p>s22107@pjwstk.edu.pl</p>
              </div>
            </Box>
          </PersonContainer>
          <PersonContainer>
            <Logo src="avatar.jpg" alt="Maciek" />
            <Heading as="h5" align="center" color="textColor">
              Maciej Kawęcki
            </Heading>
            <Box
              customStyles={css`
                display: flex;
                flex-direction: column;
                align-items: left;
                justify-content: start;
                padding: 2rem 1rem;
                width: 300px;
                height: 150px;
                gap: 2rem;
              `}
            >
              <div
                style={{ display: "flex", alignItems: "center", gap: "1rem" }}
              >
                <FaGithub style={{ fontSize: "3em" }} />
                <Link
                  to="https://github.com/BrainTireFire"
                  style={{ display: "inline-block", verticalAlign: "middle" }}
                >
                  https://github.com/BrainTireFire
                </Link>
              </div>
              <div
                style={{ display: "flex", alignItems: "center", gap: "1rem" }}
              >
                <IoMail style={{ fontSize: "3em" }} />
                <p>s20202@pjwstk.edu.pl</p>
              </div>
            </Box>
          </PersonContainer>
          <PersonContainer>
            <Logo src="bartekGithub.png" alt="Bartek" />
            <Heading as="h5" align="center" color="textColor">
              Bartłomiej Kamiński
            </Heading>
            <Box
              customStyles={css`
                display: flex;
                flex-direction: column;
                align-items: left;
                justify-content: start;
                padding: 2rem 1rem;
                width: 300px;
                height: 150px;
                gap: 2rem;
              `}
            >
              <div
                style={{ display: "flex", alignItems: "center", gap: "1rem" }}
              >
                <FaGithub style={{ fontSize: "3em" }} />
                <Link
                  to="https://github.com/Snickers333"
                  style={{ display: "inline-block", verticalAlign: "middle" }}
                >
                  https://github.com/Snickers333
                </Link>
              </div>
              <div
                style={{ display: "flex", alignItems: "center", gap: "1rem" }}
              >
                <IoMail style={{ fontSize: "3em" }} />
                <p>s23950@pjwstk.edu.pl</p>
              </div>
            </Box>
          </PersonContainer>
        </Box>
      </DevelopersContainer>
    </StyledHome>
  );
}
