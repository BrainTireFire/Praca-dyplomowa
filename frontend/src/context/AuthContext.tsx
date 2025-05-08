import { useContext, createContext, ReactNode, useState } from "react";
const AuthContext = createContext<AuthContextType>({isAuthorized: {payload: false}, setIsAuthorized: () => {}});

const AuthProvider: React.FC<AuthProviderProps> = ({
  children,
}) => {
  const [isAuthorized, setIsAuthorized] = useState<{payload: boolean}>({payload: false});
  return <AuthContext.Provider value={{isAuthorized: isAuthorized, setIsAuthorized: setIsAuthorized}}>{children}</AuthContext.Provider>;
};

export default AuthProvider;

export const useAuth = () => {
  return useContext(AuthContext);
};


export type AuthContextType = {
    isAuthorized: {payload: boolean};
    setIsAuthorized: React.Dispatch<React.SetStateAction<{payload: boolean}>>
};

interface AuthProviderProps {
  children: ReactNode;
}