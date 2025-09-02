export const usePropertyWithOwner = jest.fn(() => ({
    property: {
        id: '1',
        name: 'Casa Bonita',
        year: 2020,
        price: 100000,
        codeInternal: 'CB123',
        address: 'Calle 123',
        propertyImages: [{ file: '/image.jpg' }],
        propertyTraces: [
            {
                dateSale: '2023-01-01',
                name: 'Venta 1',
                value: 90000,
                tax: 10000,
            },
        ],
    },
    owner: { name: 'Juan Pérez' },
    loading: false,
}));