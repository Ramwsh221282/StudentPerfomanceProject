import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { IRequestBodyFactory } from '../../../../../models/RequestParamsFactory/irequest-body-factory.interface';
import { EducationDirection } from '../models/education-direction-interface';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'any',
})
export class UpdateService extends BaseService {
  public constructor() {
    super();
  }

  public update(factory: IRequestBodyFactory): Observable<EducationDirection> {
    const body = factory.Body;
    return this.httpClient.put<EducationDirection>(
      `${this.managementApiUri}update`,
      body
    );
  }

  public createRequestBodyFactory(
    oldDirection: EducationDirection,
    newDirection: EducationDirection
  ): IRequestBodyFactory {
    return new RequestBodyFactory(oldDirection, newDirection);
  }
}

class RequestBodyFactory implements IRequestBodyFactory {
  private readonly _body: object;
  public constructor(
    oldDirection: EducationDirection,
    newDirection: EducationDirection
  ) {
    this._body = {
      initial: {
        code: oldDirection.code,
        name: oldDirection.name,
        type: oldDirection.type,
      },
      updated: {
        code: newDirection.code,
        name: newDirection.name,
        type: newDirection.type,
      },
    };
  }
  public get Body(): object {
    return this._body;
  }
}
