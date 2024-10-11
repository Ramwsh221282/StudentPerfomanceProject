import { HttpErrorResponse } from '@angular/common/http';
import { EducationDirection } from '../../models/education-direction-interface';
import { FacadeService } from '../../services/facade.service';
import { Observable } from 'rxjs';
import { IOperationHandler } from '../../../../../../shared/models/ihandable-component-interface/Ioperation-handler-interface';

type HandlerDependencies = {
  facadeService: FacadeService;
};

const createHandler =
  (dependencies: HandlerDependencies) =>
  (parameter: EducationDirection): void => {
    dependencies.facadeService.refreshPagination();
    dependencies.facadeService.fetch();
  };

const createErrorHandler =
  (dependencies: HandlerDependencies) =>
  (error: HttpErrorResponse): Observable<never> => {
    return new Observable();
  };

export const EducationDirectionDeletionHandler = (
  facadeService: FacadeService
): IOperationHandler<EducationDirection> => {
  const dependecies: HandlerDependencies = {
    facadeService: facadeService,
  };
  const handle = createHandler(dependecies);
  const handleError = createErrorHandler(dependecies);
  return { handle, handleError };
};
