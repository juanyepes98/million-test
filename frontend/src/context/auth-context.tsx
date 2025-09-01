"use client";

import { createContext, useContext, useEffect, useState } from "react";
import { login as loginService, register as registerService, logout as logoutService } from "@/lib/services/auth/auth.service";

type AuthContextType = {
    isAuthenticated: boolean;
    username: string | null;
    token: string | null;
    login: (username: string, password: string) => Promise<boolean>;
    register: (username: string, password: string) => Promise<boolean>;
    logout: () => void;
};

const AuthContext = createContext<AuthContextType | undefined>(undefined);

export const AuthProvider = ({ children }: { children: React.ReactNode }) => {
    const [isAuthenticated, setIsAuthenticated] = useState(false);
    const [token, setToken] = useState<string | null>(null);
    const [username, setUsername] = useState<string | null>(null);

    useEffect(() => {
        const storedToken = localStorage.getItem("token");
        const storedUser = localStorage.getItem("username");
        if (storedToken && storedUser) {
            setToken(storedToken);
            setUsername(storedUser);
            setIsAuthenticated(true);
        }
    }, []);

    const login = async (username: string, password: string): Promise<boolean> => {
        const res = await loginService(username, password);
        if (res.success && res.content) {
            setToken(res.content.token);
            setUsername(res.content.username);
            setIsAuthenticated(true);
            localStorage.setItem("token", res.content.token);
            localStorage.setItem("username", res.content.username);
            return true;
        }
        return false;
    };

    const register = async (username: string, password: string): Promise<boolean> => {
        const res = await registerService(username, password);
        return res.success;
    };

    const logout = () => {
        setToken(null);
        setUsername(null);
        setIsAuthenticated(false);
        logoutService();
    };

    return (
        <AuthContext.Provider value={{ isAuthenticated, username, token, login, register, logout }}>
            {children}
        </AuthContext.Provider>
    );
};

export const useAuth = () => {
    const ctx = useContext(AuthContext);
    if (!ctx) throw new Error("useAuth must be used within AuthProvider");
    return ctx;
};
