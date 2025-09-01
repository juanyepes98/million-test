import {IPropertyFilters} from "@/hooks/use-properties";
import React from "react";

interface PropertyFiltersProps {
    filters: IPropertyFilters;
    onChange: (e: React.ChangeEvent<HTMLInputElement>) => void;
    onApply: () => void;
}

export const PropertyFilters = ({filters, onChange, onApply}: PropertyFiltersProps) => (
    <form
        className="flex flex-col md:flex-row gap-2 mb-4"
        onSubmit={(e) => {
            e.preventDefault();
            onApply();
        }}
    >
        <input type="text" placeholder="Name" name="Name" value={filters.Name || ""} onChange={onChange}
               className="border rounded px-2 py-1"/>
        <input type="text" placeholder="Address" name="Address" value={filters.Address || ""} onChange={onChange}
               className="border rounded px-2 py-1"/>
        <input type="number" placeholder="Min Price" name="MinPrice" value={filters.MinPrice || ""} onChange={onChange}
               className="border rounded px-2 py-1"/>
        <input type="number" placeholder="Max Price" name="MaxPrice" value={filters.MaxPrice || ""} onChange={onChange}
               className="border rounded px-2 py-1"/>
        <button type="submit" className="bg-blue-600 text-white px-4 py-1 rounded hover:bg-blue-700">Apply</button>
    </form>
);
