import { HttpErrorResponse } from '@angular/common/http';
import { EducationPlan } from '../../models/education-plan-interface';
import { FacadeService } from '../../services/facade.service';
import { Observable } from 'rxjs';
import { IOperationHandler } from '../../../../../../shared/models/ihandable-component-interface/Ioperation-handler-interface';

type HandlerDependencies = {
  facadeService: FacadeService;
};

const createHandler =
  (dependencies: HandlerDependencies) =>
  (parameter: EducationPlan): void => {
    dependencies.facadeService.refreshPagination();
    dependencies.facadeService.fetch();
  };

const createErrorHandler =
  () =>
  (error: HttpErrorResponse): Observable<never> => {
    return new Observable();
  };

export const EducationPlanDeletionHandler = (
  facadeService: FacadeService
): IOperationHandler<EducationPlan> => {
  const dependencies: HandlerDependencies = {
    facadeService: facadeService,
  };
  const handle = createHandler(dependencies);
  const handleError = createErrorHandler();
  return { handle, handleError };
};
