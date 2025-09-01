interface PaginationProps {
    page: number;
    totalPages: number;
    onPrev: () => void;
    onNext: () => void;
}

export const PropertyPagination = ({page, totalPages, onPrev, onNext}: PaginationProps) => (
    <div className="flex justify-center gap-2 mt-4">
        <button disabled={page === 1} onClick={onPrev} className="px-3 py-1 border rounded disabled:opacity-50">Prev
        </button>
        <span className="px-3 py-1 border rounded">{page} / {totalPages || 1}</span>
        <button disabled={page === totalPages || totalPages === 0} onClick={onNext}
                className="px-3 py-1 border rounded disabled:opacity-50">Next
        </button>
    </div>
);
