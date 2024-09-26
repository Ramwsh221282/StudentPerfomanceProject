import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { IRequestBodyFactory } from '../../../../../models/RequestParamsFactory/irequest-body-factory.interface';
import { EducationPlan } from '../models/education-plan-interface';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'any',
})
export class DeleteService extends BaseService {
  public constructor() {
    super();
  }

  public delete(factory: IRequestBodyFactory): Observable<EducationPlan> {
    const body = factory.Body;
    return this.httpClient.delete<EducationPlan>(this.baseApiUri, { body });
  }

  public createRequestBodyFactory(plan: EducationPlan): IRequestBodyFactory {
    return new RequestBodyFactory(plan);
  }
}

class RequestBodyFactory implements IRequestBodyFactory {
  private readonly _body: object;

  public constructor(plan: EducationPlan) {
    this._body = {
      plan: {
        year: plan.year,
        direction: {
          code: plan.direction.code,
          name: plan.direction.name,
          type: plan.direction.type,
        },
      },
    };
  }

  public get Body(): object {
    return this._body;
  }
}
