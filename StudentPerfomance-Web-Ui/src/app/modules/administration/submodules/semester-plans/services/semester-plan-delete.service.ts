import { Injectable } from '@angular/core';
import { SemesterPlanBaseService } from './semester-plan-base.service';
import { IRequestBodyFactory } from '../../../../../models/RequestParamsFactory/irequest-body-factory.interface';
import { SemesterPlan } from '../../../../../modules/administration/submodules/semester-plans/models/semester-plan.interface';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'any',
})
export class SemesterPlanDeleteService extends SemesterPlanBaseService {
  constructor() {
    super();
  }

  public delete(factory: IRequestBodyFactory): Observable<SemesterPlan> {
    const body = factory.Body;
    return this.httpClient.delete<SemesterPlan>(`${this.baseApiUri}`, { body });
  }

  public createRequestBodyFactory(plan: SemesterPlan): IRequestBodyFactory {
    return new HttpRequestBody(plan);
  }
}

class HttpRequestBody implements IRequestBodyFactory {
  private readonly _body: object;

  public constructor(plan: SemesterPlan) {
    this._body = {
      semester: {
        number: plan.semesterNumber,
      },
      discipline: {
        name: plan.disciplineName,
      },
    };
  }

  public get Body(): object {
    return this._body;
  }
}
