import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { IRequestBodyFactory } from '../../../../../models/RequestParamsFactory/irequest-body-factory.interface';
import { EducationDirection } from '../models/education-direction-interface';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'any',
})
export class DeleteService extends BaseService {
  constructor() {
    super();
  }

  public delete(factory: IRequestBodyFactory): Observable<EducationDirection> {
    const body = factory.Body;
    return this.httpClient.delete<EducationDirection>(
      `${this.managementApiUri}remove`,
      {
        body,
      }
    );
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
      code: direction.code,
      name: direction.name,
      type: direction.type,
    };
  }
  get Body(): object {
    return this._body;
  }
}
