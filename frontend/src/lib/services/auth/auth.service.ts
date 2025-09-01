import { ApiResponse } from "@/lib/core/types/api-response";
import { httpClient } from "@/lib/core/http-client";

export type LoginResponse = {
    token: string;
    username: string;
};

export async function login(username: string, password: string): Promise<ApiResponse<LoginResponse>> {
    return httpClient.post<LoginResponse>("/Auth/login", { username, password });
}

export async function register(username: string, password: string): Promise<ApiResponse<string>> {
    return httpClient.post<string>("/Auth/register", { username, password });
}

export function logout() {
    if (typeof window !== "undefined") {
        localStorage.removeItem("token");
        localStorage.removeItem("username");
    }
}