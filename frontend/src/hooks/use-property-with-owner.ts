import {useEffect, useState} from "react";
import {PropertyDetailType} from "@/lib/services/property/types/property.type";
import {OwnerType} from "@/lib/services/owner/types/owner.type";
import {getPropertyById} from "@/lib/services/property/property.service";
import {getOwnerById} from "@/lib/services/owner/owner.service";

/**
 * Custom hook to fetch a property by its ID along with its owner.
 *
 * @param propertyId - The ID of the property to fetch
 * @returns An object containing:
 *   - property: the property data or null
 *   - owner: the owner data or null
 *   - loading: boolean indicating if the data is being fetched
 */
export function usePropertyWithOwner(propertyId?: string) {
    const [property, setProperty] = useState<PropertyDetailType | null>(null);
    const [owner, setOwner] = useState<OwnerType | null>(null);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        if (!propertyId) return;

        const fetchData = async () => {
            setLoading(true);

            const resProperty = await getPropertyById(propertyId);
            if (!resProperty.success || !resProperty.content) {
                setLoading(false);
                return;
            }
            setProperty(resProperty.content);

            const ownerId = resProperty.content.ownerId;
            if (ownerId) {
                const resOwner = await getOwnerById(ownerId);
                if (resOwner.success && resOwner.content) setOwner(resOwner.content);
            }

            setLoading(false);
        };

        fetchData();
    }, [propertyId]);

    return { property, owner, loading };
}