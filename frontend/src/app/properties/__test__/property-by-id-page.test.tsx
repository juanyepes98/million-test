import { render, screen } from '@testing-library/react';
import PropertyByIdPage from '../[id]/page';
import { usePropertyWithOwner } from '@/hooks/use-property-with-owner';

jest.mock('@/hooks/use-property-with-owner');
jest.mock('next/navigation', () => ({
    useRouter: () => ({ push: jest.fn() }),
    useParams: jest.fn(() => ({ id: '1' })),
}));

(usePropertyWithOwner as jest.Mock).mockReturnValue({
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
});

describe('PropertyByIdPage', () => {
    it('renders property details', () => {
        render(<PropertyByIdPage />);
        expect(screen.getByText(/Casa Bonita/i)).toBeInTheDocument();
        expect(screen.getByText(/Juan Pérez/i)).toBeInTheDocument();
        expect(screen.getByText(/Venta 1/i)).toBeInTheDocument();
    });

    it('shows loading when loading is true', () => {
        (usePropertyWithOwner as jest.Mock).mockReturnValueOnce({
            property: null,
            owner: null,
            loading: true,
        });
        render(<PropertyByIdPage />);
        expect(screen.getByText(/Loading.../i)).toBeInTheDocument();
    });

    it('shows not found when property is null', () => {
        (usePropertyWithOwner as jest.Mock).mockReturnValueOnce({
            property: null,
            owner: null,
            loading: false,
        });
        render(<PropertyByIdPage />);
        expect(screen.getByText(/Property not found/i)).toBeInTheDocument();
    });
});