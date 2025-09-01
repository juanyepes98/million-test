import Image from "next/image";
import {PropertyType} from "@/lib/services/property/types/property.type";

interface PropertyCardProps {
    property: PropertyType;
    onClick: (id: string) => void;
}

export const PropertyCard = ({property, onClick}: PropertyCardProps) => (
    <div className="border rounded overflow-hidden cursor-pointer hover:shadow-lg"
         onClick={() => onClick(property.id)}>
        <Image src={property.propertyImages?.[0]?.file || "/placeholder.png"}
               alt={property.name}
               width={400}
               height={300}
               className="w-full h-48 object-cover"/>
        <div className="p-2">
            <h2 className="font-bold">{property.name}</h2>
        </div>
    </div>
);