import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { IRequestParamsFactory } from '../../../../../models/RequestParamsFactory/irequest-params-factory.interface';
import { HttpParams } from '@angular/common/http';
import { EducationDirection } from '../models/education-direction-interface';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'any',
})
export class SearchDirectionsService extends BaseService {
  public constructor() {
    super();
  }

  public getAll(): Observable<EducationDirection[]> {
    return this.httpClient.get<EducationDirection[]>(`${this.baseApiUri}/all`);
  }

  public filterPagedByCode(
    factory: IRequestParamsFactory
  ): Observable<EducationDirection[]> {
    const params = factory.Params;
    return this.httpClient.get<EducationDirection[]>(
      `${this.baseApiUri}/searchByCode`,
      { params }
    );
  }

  public filterPagedByName(
    factory: IRequestParamsFactory
  ): Observable<EducationDirection[]> {
    const params = factory.Params;
    return this.httpClient.get<EducationDirection[]>(
      `${this.baseApiUri}/searchByName`,
      { params }
    );
  }

  public createSearchRequestParamFactory(
    direction: EducationDirection
  ): IRequestParamsFactory {
    return new FilterPagedRequestParamFactory(direction);
  }
}

class FilterPagedRequestParamFactory implements IRequestParamsFactory {
  private readonly _httpParams: HttpParams;

  public constructor(direction: EducationDirection) {
    this._httpParams = new HttpParams()
      .set('Direction.Code', direction.code)
      .set('Direction.Name', direction.name)
      .set('Direction.Type', direction.type);
  }

  public get Params(): HttpParams {
    return this._httpParams;
  }
}
