import { ApiResponse } from "@/lib/core/types/api-response";

class HttpClient {
    private readonly baseUrl: string;

    constructor() {
        this.baseUrl = process.env.NEXT_PUBLIC_API_URL as string || "";
    }

    private async request<T>(url: string, options: RequestInit): Promise<ApiResponse<T>> {
        const response = await fetch(`${this.baseUrl}${url}`, {
            ...options,
            headers: {
                "Content-Type": "application/json",
                ...options.headers,
            },
        });

        return await response.json() as Promise<ApiResponse<T>>;
    }

    get<T>(url: string): Promise<ApiResponse<T>> {
        return this.request<T>(url, { method: "GET" });
    }

    post<T>(url: string, body: unknown): Promise<ApiResponse<T>> {
        return this.request<T>(url, {
            method: "POST",
            body: JSON.stringify(body),
        });
    }

    put<T>(url: string, body: unknown): Promise<ApiResponse<T>> {
        return this.request<T>(url, {
            method: "PUT",
            body: JSON.stringify(body),
        });
    }

    delete<T>(url: string): Promise<ApiResponse<T>> {
        return this.request<T>(url, { method: "DELETE" });
    }
}

export const httpClient = new HttpClient();
