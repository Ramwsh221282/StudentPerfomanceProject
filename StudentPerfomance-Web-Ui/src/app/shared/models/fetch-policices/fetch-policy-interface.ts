import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface IFetchPolicy<T> {
  executeFetchPolicy(httpClient: HttpClient): Observable<T>;
  addPages(page: number, pageSize: number): void;
}
