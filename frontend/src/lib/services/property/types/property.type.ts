export type PropertyImageType = {
    id: string | null;
    file: string;
    enabled: boolean;
};

export type PropertyType = {
    id: string;
    name: string;
    propertyImages: PropertyImageType[];
};

export type PropertyTraceType = {
    id: string | null;
    dateSale: string;
    name: string;
    value: number;
    tax: number;
};

export type PropertyDetailType = {
    id: string;
    name: string;
    year: number;
    price: number;
    codeInternal: string | null;
    address: string;
    ownerId: string;
    propertyImages: {
        id: string | null;
        file: string;
        enabled: boolean;
    }[];
    propertyTraces: PropertyTraceType[];
};