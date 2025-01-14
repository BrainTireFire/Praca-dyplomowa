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

interface Notification {
  id: string;
  message: string;
  campaignId: string;
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
  const connection = useRef<signalR.HubConnection | null>(null);
  const [notifications, setNotifications] = useState<Notification[]>([]);

  useEffect(() => {
    // Set up SignalR connection
    connection.current = new signalR.HubConnectionBuilder()
      .withUrl(`${BASE_URL}/notifications`)
      .configureLogging(signalR.LogLevel.Information)
      .withAutomaticReconnect()
      .build();

    connection.current
      .start()
      .then(() => console.log("Connected to NotificationHub"))
      .catch((err) => console.error("SignalR Connection Error:", err));

    connection.current.on(
      "ReceiveNotification",
      (message: string, campaignId: string) => {
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
              <JoinCampaignToast
                message={message}
                link={`/campaigns/${campaignId}`}
              />
            ),
            error: (error) => `Notification failed: ${error}`,
          },
          { success: { duration: 10000, icon: null } }
        );
      }
    );

    return () => {
      connection.current?.stop();
    };
  }, []);

  return (
    <NotificationContext.Provider value={{ notifications }}>
      {children}
    </NotificationContext.Provider>
  );
};
