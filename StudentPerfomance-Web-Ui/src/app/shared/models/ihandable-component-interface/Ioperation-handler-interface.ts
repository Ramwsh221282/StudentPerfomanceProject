import { HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface IOperationHandler<T> {
  handle(parameter: T): void;
  handleError(error: HttpErrorResponse): Observable<never>;
}
