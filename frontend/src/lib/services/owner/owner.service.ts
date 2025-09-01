import {ApiResponse} from "@/lib/core/types/api-response";
import {httpClient} from "@/lib/core/http-client";
import {OwnerType} from "@/lib/services/owner/types/owner.type";

/**
 * Gets an owner by their ID.
 *
 * @param id - The owner's ID
 * @returns Promise with the API response containing the owner
 */
export async function getOwnerById(id: string): Promise<ApiResponse<OwnerType>> {
    return httpClient.get<OwnerType>(`/Owner/${id}`);
}