import { HttpParams } from '@angular/common/http';

export interface IRequestParamsFactory {
  get Params(): HttpParams;
}
