"use client";

import {useParams, useRouter} from "next/navigation";
import Image from "next/image";
import {usePropertyWithOwner} from "@/hooks/use-property-with-owner";

/**
 * Page to display a property by its ID.
 *
 * Uses the `usePropertyWithOwner` hook to fetch property and owner data.
 * Shows property image, details, and traces.
 * Includes a "Back" button to navigate to the home page.
 */
export default function PropertyByIdPage() {
    const params = useParams();
    const router = useRouter();
    const propertyId = Array.isArray(params?.id) ? params.id[0] : params?.id;

    const { property, owner, loading } = usePropertyWithOwner(propertyId);

    if (loading) return <p className="text-center mt-10">Loading...</p>;
    if (!property) return <p className="text-center mt-10">Property not found</p>;

    return (
        <div className="container mx-auto p-4">
            <div className="mb-4">
                <button className="px-4 py-2 bg-blue-600 text-white rounded shadow hover:bg-blue-700 transition"
                        onClick={() => router.push("/")}>
                    Back
                </button>
            </div>

            <div className="flex flex-col md:flex-row gap-6">
                {/* Property Image */}
                <div className="md:w-1/2 w-full h-64 md:h-auto relative rounded overflow-hidden shadow-md">
                    <Image src={property.propertyImages?.[0]?.file || "/placeholder.png"}
                           alt={property.name}
                           fill
                           className="object-cover rounded"/>
                </div>

                {/* Property Details */}
                <div className="md:w-1/2 w-full border rounded shadow-md p-4 flex flex-col gap-2">
                    <h1 className="text-2xl font-bold">{ property.name }</h1>
                    <p><span className="font-semibold">Year:</span> { property.year }</p>
                    <p><span className="font-semibold">Price:</span> ${ property.price.toLocaleString() }</p>
                    { property.codeInternal && <p><span className="font-semibold">Code:</span> { property.codeInternal }</p> }
                    <p><span className="font-semibold">Address:</span> { property.address }</p>
                    { owner && <p><span className="font-semibold">Owner:</span> { owner.name }</p> }

                    {/* Property Traces */}
                    { property.propertyTraces.length > 0 && (
                        <div className="mt-4">
                            <h2 className="text-lg font-semibold mb-2">Property Traces</h2>
                            <ul className="flex flex-col gap-2">
                                { property.propertyTraces.map((trace, idx) => (
                                    <li key={idx} className="border rounded p-2 bg-gray-400">
                                        <p><span className="font-semibold">Date:</span> { new Date(trace.dateSale).toLocaleDateString() }</p>
                                        {trace.name && <p><span className="font-semibold">Name:</span> { trace.name }</p>}
                                        <p><span className="font-semibold">Value:</span> ${ trace.value.toLocaleString() }</p>
                                        <p><span className="font-semibold">Tax:</span> ${ trace.tax.toLocaleString() }</p>
                                    </li>
                                )) }
                            </ul>
                        </div>
                    ) }
                </div>
            </div>
        </div>
    );
}