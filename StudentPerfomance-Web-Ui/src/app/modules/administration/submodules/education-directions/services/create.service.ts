import { Injectable } from '@angular/core';
import { IRequestBodyFactory } from '../../../../../models/RequestParamsFactory/irequest-body-factory.interface';
import { EducationDirection } from '../models/education-direction-interface';
import { BaseService } from './base.service';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'any',
})
export class CreateService extends BaseService {
  constructor() {
    super();
  }

  public create(factory: IRequestBodyFactory): Observable<EducationDirection> {
    const body = factory.Body;
    return this.httpClient.post<EducationDirection>(this.baseApiUri, body);
  }

  public createRequestBodyFactory(
    direction: EducationDirection
  ): IRequestBodyFactory {
    return new RequestBodyFactory(direction);
  }
}

class RequestBodyFactory implements IRequestBodyFactory {
  private readonly _body: object;

  public constructor(direction: EducationDirection) {
    this._body = {
      direction: {
        code: direction.code,
        name: direction.name,
        type: direction.type,
      },
    };
  }

  get Body(): object {
    return this._body;
  }
}
