export const PaginationPayloadBuilder = (
  page: number,
  pageSize: number
): object => {
  return {
    page: page,
    pageSize: pageSize,
  };
};
