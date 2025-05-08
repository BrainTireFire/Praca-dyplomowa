import { useContext, createContext, ReactNode, useState } from "react";
const AuthContext = createContext<AuthContextType>({payloadContainer: {payload: false}, setPayloadContainer: () => {}});

const AuthProvider: React.FC<AuthProviderProps> = ({
  children,
}) => {
  const [payloadContainer, setPayloadContainer] = useState<{payload: boolean}>({payload: false});
  return <AuthContext.Provider value={{payloadContainer: payloadContainer, setPayloadContainer: setPayloadContainer}}>{children}</AuthContext.Provider>;
};

export default AuthProvider;

export const useAuth = () => {
  return useContext(AuthContext);
};


export type AuthContextType = {
    payloadContainer: {payload: boolean};
    setPayloadContainer: React.Dispatch<React.SetStateAction<{payload: boolean}>>
};

interface AuthProviderProps {
  children: ReactNode;
}