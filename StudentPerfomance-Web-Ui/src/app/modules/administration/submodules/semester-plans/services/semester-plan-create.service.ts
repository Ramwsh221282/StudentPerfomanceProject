import { Injectable } from '@angular/core';
import { SemesterPlanBaseService } from './semester-plan-base.service';
import { IRequestBodyFactory } from '../../../../../models/RequestParamsFactory/irequest-body-factory.interface';
import { SemesterPlan } from '../../../../../modules/administration/submodules/semester-plans/models/semester-plan.interface';
import { Observable } from 'rxjs';
import { SemesterPlansModule } from '../semester-plans.module';

@Injectable({
  providedIn: 'any',
})
export class SemesterPlanCreateService extends SemesterPlanBaseService {
  public constructor() {
    super();
  }

  public create(factory: IRequestBodyFactory): Observable<SemesterPlan> {
    const body = factory.Body;
    return this.httpClient.post<SemesterPlan>(`${this.baseApiUri}`, body);
  }

  public createRequestBody(plan: SemesterPlan): IRequestBodyFactory {
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
      group: {
        name: plan.groupName,
      },
    };
  }

  public get Body(): object {
    return this._body;
  }
}
