import {ApiResponse} from "@/lib/core/types/api-response";
import {PaginationType} from "@/lib/core/types/pagination.type";
import {PropertyDetailType, PropertyType} from "@/lib/services/property/types/property.type";
import {httpClient} from "@/lib/core/http-client";

export async function getProperties(params?: {
    Skip?: number;
    Take?: number;
    Name?: string;
    Address?: string;
    MinPrice?: number;
    MaxPrice?: number;
}): Promise<ApiResponse<PaginationType<PropertyType>>> {
    // Set queryParams
    const query = new URLSearchParams();
    if (params) {
        Object.entries(params).forEach(([key, value]) => {
            if (value !== undefined && value !== null) {
                query.append(key, value.toString());
            }
        });
    }

    return httpClient.get<PaginationType<PropertyType>>(`/Property?${query.toString()}`);
}

export async function getPropertyById(id: string): Promise<ApiResponse<PropertyDetailType>> {
    return httpClient.get<PropertyDetailType>(`/Property/${id}`);
}