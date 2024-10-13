import { Observable } from 'rxjs';
import { IFetchPolicy } from './fetch-policy-interface';

export interface IObservableFetchable<T> {
  setPolicy(policy: IFetchPolicy<T>): void;
  fetch(): Observable<T>;
}
