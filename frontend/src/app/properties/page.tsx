"use client";

import {useRouter} from "next/navigation";
import {useProperties} from "@/hooks/use-properties";
import {PropertyPagination} from "@/app/properties/components/property-pagination";
import {PropertyCard} from "@/app/properties/components/property-card";
import {PropertyFilters} from "@/app/properties/components/property-filters";
import React from "react";

export default function PropertiesPage() {
    const router = useRouter();
    const {properties, page, totalCount, pageSize, filters, setFilters, setPage, loading} = useProperties();

    const handleFilterChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const {name, value} = e.target;

        setFilters((prev) => ({
            ...prev,
            [name]: value,
        }));
        setPage(1);
    };

    const handlePropertyClick = (id: string) => router.push(`/properties/${id}`);
    const totalPages = Math.ceil(totalCount / pageSize);

    return (
        <div className="container mx-auto p-4">
            <PropertyFilters filters={filters} onChange={handleFilterChange} onApply={() => setPage(1)}/>

            {loading ? (
                <p className="text-center mt-10">Loading...</p>
            ) : (
                <>
                    <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4">
                        {properties.map((prop) => (
                            <PropertyCard key={prop.id} property={prop} onClick={handlePropertyClick}/>
                        ))}
                    </div>

                    <PropertyPagination page={page} totalPages={totalPages} onPrev={() => setPage(page - 1)}
                                        onNext={() => setPage(page + 1)}/>
                </>
            )}
        </div>
    );
}