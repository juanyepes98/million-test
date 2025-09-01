export type PaginationType<T> = {
    items: T[];
    page: number;
    pageSize: number;
    totalCount: number;
};