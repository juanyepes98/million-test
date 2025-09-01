import {useState, useEffect} from "react";
import {getProperties} from "@/lib/services/property/property.service";
import {PropertyType} from "@/lib/services/property/types/property.type";

export interface IPropertyFilters {
    Name?: string;
    Address?: string;
    MinPrice?: string;
    MaxPrice?: string;
}

export function useProperties(
    initialPage = 1,
    initialPageSize = 10,
    initialFilters: IPropertyFilters = {}
) {
    const [properties, setProperties] = useState<PropertyType[]>([]);
    const [page, setPage] = useState<number>(initialPage);
    const [pageSize, setPageSize] = useState<number>(initialPageSize);
    const [totalCount, setTotalCount] = useState<number>(0);
    const [filters, setFilters] = useState<IPropertyFilters>(initialFilters);
    const [loading, setLoading] = useState<boolean>(false);

    const fetchProperties = async () => {
        setLoading(true);
        const res = await getProperties({
            Skip: (page - 1) * pageSize,
            Take: pageSize,
            Name: filters.Name,
            Address: filters.Address,
            MinPrice: filters.MinPrice ? Number(filters.MinPrice) : undefined,
            MaxPrice: filters.MaxPrice ? Number(filters.MaxPrice) : undefined,
        });

        if (res.success && res.content) {
            setProperties(res.content.items);
            setPage(res.content.page + 1);
            setPageSize(res.content.pageSize || 10);
            setTotalCount(res.content.totalCount);
        }

        setLoading(false);
    };

    useEffect(() => {
        fetchProperties();
    }, [page, filters]);

    return {properties, page, pageSize, totalCount, filters, setFilters, setPage, loading};
}