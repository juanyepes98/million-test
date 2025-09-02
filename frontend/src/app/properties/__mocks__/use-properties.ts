export const useProperties = jest.fn(() => ({
    properties: [
        { id: '1', name: 'Casa Bonita', price: 100000 },
        { id: '2', name: 'Apartamento Azul', price: 200000 },
    ],
    page: 1,
    totalCount: 2,
    pageSize: 1,
    filters: {},
    setFilters: jest.fn(),
    setPage: jest.fn(),
    loading: false,
}));