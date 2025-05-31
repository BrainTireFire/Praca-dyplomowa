import React, {
  createContext,
  useEffect,
  useRef,
  useState,
  ReactNode,
} from "react";
import * as signalR from "@microsoft/signalr";
import { toast } from "react-hot-toast";
import { BASE_URL } from "../services/constAPI";
import JoinCampaignToast from "../features/notifications/JoinCampaignToast";
import SessionStartedToast from "../features/notifications/SessionStartedToast";
import KickCampaingToast from "../features/notifications/KickCampaingToast";
import { ability } from "../features/effects/abilities";
import AbilityRollRequestToast from "../features/notifications/rolls/request/AbilityRollRequestToast";
import { useLocation, useSearchParams } from "react-router-dom";
import AbilityRollResultToast from "../features/notifications/rolls/result/AbilityRollResultToast";
import { skill } from "../features/effects/skills";
import SkillRollRequestToast from "../features/notifications/rolls/request/SkillRollRequestToast";
import SkillRollResultToast from "../features/notifications/rolls/result/SkillRollResultToast";
import SavingThrowRollRequestToast from "../features/notifications/rolls/request/SavingThrowRollRequestToast";
import SavingThrowRollResultToast from "../features/notifications/rolls/result/SavingThrowRollResultToast";
import { ShortRestHealthpointsRegained } from "../models/shortRest/shortRestHealthpointsRegained";
import ShortRestResultToast from "../features/notifications/shortRest/shortRestResult";
import { useAuth } from "./AuthContext";

interface Notification {
  id: string;
  message: string;
  campaignId: string;
  characterId: string;
}

interface NotificationContextType {
  notifications: Notification[];
}

export const NotificationContext = createContext<
  NotificationContextType | undefined
>(undefined);

interface NotificationProviderProps {
  children: ReactNode;
}

export const NotificationProvider: React.FC<NotificationProviderProps> = ({
  children,
}) => {
  const hubConnection = useRef<signalR.HubConnection | null>(null);
  const [notifications, setNotifications] = useState<Notification[]>([]);
  const [searchParams, setSearchParams] = useSearchParams();

  const { payloadContainer } = useAuth();

  useEffect(() => {
    hubConnection.current = new signalR.HubConnectionBuilder()
      .withUrl(`${BASE_URL}/notifications`)
      .configureLogging(signalR.LogLevel.Information)
      .withAutomaticReconnect()
      .build();

    hubConnection.current
      .start()
      .then(() => console.log("Connected to NotificationHub"))
      .catch((err) => console.error("SignalR Connection Error:", err));

    hubConnection.current.on(
      "ReceiveNotification",
      (message: string, campaignId: string, isKick?: boolean) => {
        const newNotification: Notification = {
          id: new Date().toISOString(),
          message,
          campaignId,
        };
        setNotifications((prev) => [...prev, newNotification]);

        toast.promise(
          new Promise<{ message: string; campaignId: string }>(
            (resolve, reject) => {
              // Resolve with both message and campaignId
              resolve({ message, campaignId });
            }
          ),
          {
            loading: "Loading notification...",
            success: ({ message, campaignId }) =>
              isKick ? (
                <KickCampaingToast
                  message={message}
                  link={`/campaigns/${campaignId}`}
                />
              ) : (
                <JoinCampaignToast
                  message={message}
                  link={`/campaigns/${campaignId}`}
                />
              ),
            error: (error) => `Notification failed: ${error}`,
          },
          { success: { duration: 5000, icon: null } }
        );
      }
    );

    hubConnection.current.on(
      "AbilityRollRequest",
      (
        message: string,
        characterId: string,
        characterName: string,
        ability: ability
      ) => {
        toast.promise(
          new Promise<{ message: string; characterId: string }>(
            (resolve, reject) => {
              // Resolve with both message and campaignId
              resolve({ message, characterId });
            }
          ),
          {
            loading: "Loading notification...",
            success: ({ message, characterId }) => (
              <AbilityRollRequestToast
                message={message}
                characterId={characterId}
                characterName={characterName}
                ability={ability}
              />
            ),
            error: (error) => `Notification failed: ${error}`,
          },
          { success: { duration: 5000, icon: null } }
        );
      }
    );

    hubConnection.current.on(
      "AbilityRollPerformed",
      (
        message: string,
        characterName: string,
        ability: ability,
        roll: number
      ) => {
        toast.promise(
          new Promise<{ message: string; characterName: string }>(
            (resolve, reject) => {
              // Resolve with both message and campaignId
              resolve({ message, characterName });
            }
          ),
          {
            loading: "Loading notification...",
            success: ({ message, characterName }) => (
              <AbilityRollResultToast
                message={message}
                characterName={characterName}
                ability={ability}
                roll={roll}
              />
            ),
            error: (error) => `Notification failed: ${error}`,
          },
          { success: { duration: 5000, icon: null } }
        );
      }
    );

    hubConnection.current.on(
      "SkillRollRequest",
      (
        message: string,
        characterId: string,
        characterName: string,
        skill: skill
      ) => {
        toast.promise(
          new Promise<{ message: string; characterId: string }>(
            (resolve, reject) => {
              // Resolve with both message and campaignId
              resolve({ message, characterId });
            }
          ),
          {
            loading: "Loading notification...",
            success: ({ message, characterId }) => (
              <SkillRollRequestToast
                message={message}
                characterId={characterId}
                characterName={characterName}
                skill={skill}
              />
            ),
            error: (error) => `Notification failed: ${error}`,
          },
          { success: { duration: 5000, icon: null } }
        );
      }
    );

    hubConnection.current.on(
      "SkillRollPerformed",
      (message: string, characterName: string, skill: skill, roll: number) => {
        toast.promise(
          new Promise<{ message: string; characterName: string }>(
            (resolve, reject) => {
              // Resolve with both message and campaignId
              resolve({ message, characterName });
            }
          ),
          {
            loading: "Loading notification...",
            success: ({ message, characterName }) => (
              <SkillRollResultToast
                message={message}
                characterName={characterName}
                skill={skill}
                roll={roll}
              />
            ),
            error: (error) => `Notification failed: ${error}`,
          },
          { success: { duration: 5000, icon: null } }
        );
      }
    );

    hubConnection.current.on(
      "SavingThrowRollRequest",
      (
        message: string,
        characterId: string,
        characterName: string,
        ability: ability
      ) => {
        toast.promise(
          new Promise<{ message: string; characterId: string }>(
            (resolve, reject) => {
              // Resolve with both message and campaignId
              resolve({ message, characterId });
            }
          ),
          {
            loading: "Loading notification...",
            success: ({ message, characterId }) => (
              <SavingThrowRollRequestToast
                message={message}
                characterId={characterId}
                characterName={characterName}
                ability={ability}
              />
            ),
            error: (error) => `Notification failed: ${error}`,
          },
          { success: { duration: 5000, icon: null } }
        );
      }
    );

    hubConnection.current.on(
      "SavingThrowRollPerformed",
      (
        message: string,
        characterName: string,
        ability: ability,
        roll: number
      ) => {
        toast.promise(
          new Promise<{ message: string; characterName: string }>(
            (resolve, reject) => {
              // Resolve with both message and campaignId
              resolve({ message, characterName });
            }
          ),
          {
            loading: "Loading notification...",
            success: ({ message, characterName }) => (
              <SavingThrowRollResultToast
                message={message}
                characterName={characterName}
                ability={ability}
                roll={roll}
              />
            ),
            error: (error) => `Notification failed: ${error}`,
          },
          { success: { duration: 5000, icon: null } }
        );
      }
    );

    hubConnection.current.on(
      "SessionHasBeenStarted",
      (message: string, campaignId: string, encounterId: string) => {
        const newNotification: Notification = {
          id: new Date().toISOString(),
          message,
          campaignId,
        };
        setNotifications((prev) => [...prev, newNotification]);

        toast.promise(
          new Promise<{ message: string; campaignId: string }>(
            (resolve, reject) => {
              // Resolve with both message and campaignId
              resolve({ message, campaignId });
            }
          ),
          {
            loading: "Loading notification...",
            success: ({ message, campaignId }) => (
              <SessionStartedToast
                message={message}
                link={`/campaigns/${campaignId}/session/${encounterId}`}
              />
            ),
            error: (error) => `Notification failed: ${error}`,
          },
          { success: { duration: 5000, icon: null } }
        );
      }
    );

    hubConnection.current.on(
      "ShortRestPerformed",
      (data: ShortRestHealthpointsRegained[]) => {
        console.log(data);
        toast.promise(
          new Promise<{ data: ShortRestHealthpointsRegained[] }>(
            (resolve, reject) => {
              // Resolve with both message and campaignId
              resolve({ data });
            }
          ),
          {
            loading: "Loading notification...",
            success: ({ data }) => (
              <ShortRestResultToast healthpointData={data} />
            ),
            error: (error) => `Notification failed: ${error}`,
          },
          { success: { duration: 5000, icon: null } }
        );
      }
    );

    return () => {
      hubConnection.current?.stop();
    };
  }, [payloadContainer]);

  return (
    <NotificationContext.Provider value={{ notifications }}>
      {children}
    </NotificationContext.Provider>
  );
};
