import { useContext, createContext, ReactNode, useState } from "react";
const AuthContext = createContext<AuthContextType>({isAuthorized: false, setIsAuthorized: () => {}});

const AuthProvider: React.FC<AuthProviderProps> = ({
  children,
}) => {
  const [isAuthorized, setIsAuthorized] = useState<boolean>(false);
  return <AuthContext.Provider value={{isAuthorized: isAuthorized, setIsAuthorized: setIsAuthorized}}>{children}</AuthContext.Provider>;
};

export default AuthProvider;

export const useAuth = () => {
  return useContext(AuthContext);
};


export type AuthContextType = {
    isAuthorized: boolean;
    setIsAuthorized: React.Dispatch<React.SetStateAction<boolean>>
};

interface AuthProviderProps {
  children: ReactNode;
}