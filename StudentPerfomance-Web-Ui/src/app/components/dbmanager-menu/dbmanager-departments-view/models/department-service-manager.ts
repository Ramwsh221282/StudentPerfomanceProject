import { DepartmentsFetchService } from '../services/departments-fetch.service';
import { DepartmentsPaginationService } from '../services/departments-pagination.service';

export class DepartmentServiceManager {
  public refreshData(
    fetchService: DepartmentsFetchService,
    paginationService: DepartmentsPaginationService
  ) {
    const factory =
      fetchService.createFetchByPageRequestParamsFactory(paginationService);
    fetchService.fetch(factory);
  }

  public refreshSelection(fetchService: DepartmentsFetchService) {
    fetchService.refreshSelection();
  }
}
