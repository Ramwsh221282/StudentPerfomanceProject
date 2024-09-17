import { DepartmentsFetchService } from '../../../../../dbmanager-menu/dbmanager-departments-view/services/departments-fetch.service';
import { DepartmentsPaginationService } from '../../../../../dbmanager-menu/dbmanager-departments-view/services/departments-pagination.service';

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
