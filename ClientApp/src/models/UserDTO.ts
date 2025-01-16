export interface UserDTO {
  id: number;
  userId: string;
  firstName: string;
  lastName: string;
  email: string;
  mobileNo?: string;
  createId?: number;
  createDate?: Date;
  updateId?: number;
  updateDate?: Date;
}
export interface PaginatedList<T> {
  pageIndex: number;
  totalPages: number;
  countData: number;
  hasPreviousPage: boolean;
  hasNextPage: boolean;
  data: T[];
}