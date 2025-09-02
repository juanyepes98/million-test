import { render, screen } from '@testing-library/react';
import { useProperties } from '@/hooks/use-properties';

jest.mock('next/navigation', () => ({
    useRouter: () => ({ push: jest.fn() }),
}));

jest.mock('@/hooks/use-properties');

(useProperties as jest.Mock).mockReturnValue({
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
});

import PropertiesPage from '../page';

describe('PropertiesPage', () => {
    it('renders properties and pagination', () => {
        render(<PropertiesPage />);
        expect(screen.getByText(/Casa Bonita/i)).toBeInTheDocument();
        expect(screen.getByText(/Apartamento Azul/i)).toBeInTheDocument();
        expect(screen.getByText(/Next/i)).toBeInTheDocument();
    });

    it('shows loading when loading is true', () => {
        (useProperties as jest.Mock).mockReturnValueOnce({
            ...useProperties(),
            loading: true,
        });
        render(<PropertiesPage />);
        expect(screen.getByText(/Loading.../i)).toBeInTheDocument();
    });
});