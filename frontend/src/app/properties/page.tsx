"use client";

import { useEffect, useState } from "react";
import { useRouter } from "next/navigation";
import { getProperties } from "@/lib/services/property/property.service";
import {PropertyType} from "@/lib/services/property/types/property.type";
import Image from "next/image";

export default function PropertiesPage() {
    const router = useRouter();

    const [properties, setProperties] = useState<PropertyType[]>([]);
    const [page, setPage] = useState(1);
    const [pageSize, setPageSize] = useState(10);
    const [totalCount, setTotalCount] = useState(0);

    const [filters, setFilters] = useState({
        Name: "",
        Address: "",
        MinPrice: "",
        MaxPrice: "",
    });

    const fetchProperties = async () => {
        const res = await getProperties({
            Skip: (page - 1) * pageSize,
            Take: pageSize,
            Name: filters.Name || undefined,
            Address: filters.Address || undefined,
            MinPrice: filters.MinPrice ? Number(filters.MinPrice) : undefined,
            MaxPrice: filters.MaxPrice ? Number(filters.MaxPrice) : undefined,
        });

        if (res.success && res.content) {
            setProperties(res.content.items);
            setPage(res.content.page + 1);
            setPageSize(res.content.pageSize || 10);
            setTotalCount(res.content.totalCount);
        }
    };

    useEffect(() => {
        fetchProperties();
    }, [page, filters]);

    const handleFilterChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const { name, value } = e.target;
        setFilters((prev) => ({ ...prev, [name]: value }));
    };

    const handlePropertyClick = (id: string) => {
        router.push(`/properties/${id}`);
    };

    const totalPages = Math.ceil(totalCount / pageSize);

    return (
        <div className="container mx-auto p-4">
            {/* Filters */}
            <form
                className="flex flex-col md:flex-row gap-2 mb-4"
                onSubmit={(e) => {
                    e.preventDefault();
                    setPage(1);
                    fetchProperties();
                }}
            >
                <input
                    type="text"
                    placeholder="Name"
                    name="Name"
                    value={filters.Name}
                    onChange={handleFilterChange}
                    className="border rounded px-2 py-1"
                />
                <input
                    type="text"
                    placeholder="Address"
                    name="Address"
                    value={filters.Address}
                    onChange={handleFilterChange}
                    className="border rounded px-2 py-1"
                />
                <input
                    type="number"
                    placeholder="Min Price"
                    name="MinPrice"
                    value={filters.MinPrice}
                    onChange={handleFilterChange}
                    className="border rounded px-2 py-1"
                />
                <input
                    type="number"
                    placeholder="Max Price"
                    name="MaxPrice"
                    value={filters.MaxPrice}
                    onChange={handleFilterChange}
                    className="border rounded px-2 py-1"
                />
                <button
                    type="submit"
                    className="bg-blue-600 text-white px-4 py-1 rounded hover:bg-blue-700"
                >
                    Apply
                </button>
            </form>

            {/* Properties list */}
            <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4">
                {properties.map((prop) => (
                    <div
                        key={prop.id}
                        className="border rounded overflow-hidden cursor-pointer hover:shadow-lg"
                        onClick={() => handlePropertyClick(prop.id)}
                    >
                        <Image
                            src={prop.propertyImages?.[0]?.file || "/placeholder.png"}
                            alt={prop.name}
                            width={400}
                            height={300}
                            className="w-full h-48 object-cover"
                        />
                        <div className="p-2">
                            <h2 className="font-bold">{prop.name}</h2>
                        </div>
                    </div>
                ))}
            </div>

            {/* Pagination */}
            <div className="flex justify-center gap-2 mt-4">
                <button
                    disabled={page === 1}
                    onClick={() => setPage((prev) => prev - 1)}
                    className="px-3 py-1 border rounded disabled:opacity-50"
                >
                    Prev
                </button>
                <span className="px-3 py-1 border rounded">
          {page} / {totalPages || 1}
        </span>
                <button
                    disabled={page === totalPages || totalPages === 0}
                    onClick={() => setPage((prev) => prev + 1)}
                    className="px-3 py-1 border rounded disabled:opacity-50"
                >
                    Next
                </button>
            </div>
        </div>
    );
}