import { IFetchPolicy } from './fetch-policy-interface';

export interface IFetchable<T> {
  setPolicy(policy: IFetchPolicy<T>): void;
  fetch(): void;
  addPages(page: number, pageSize: number): void;
}
