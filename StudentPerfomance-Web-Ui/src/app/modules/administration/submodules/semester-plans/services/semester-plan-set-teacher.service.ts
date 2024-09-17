import { Injectable } from '@angular/core';
import { SemesterPlanBaseService } from './semester-plan-base.service';
import { IRequestBodyFactory } from '../../../../../models/RequestParamsFactory/irequest-body-factory.interface';
import { SemesterPlan } from '../../../../../modules/administration/submodules/semester-plans/models/semester-plan.interface';
import { Observable } from 'rxjs';
import { Teacher } from '../../teachers/models/teacher.interface';
import { SemesterPlansModule } from '../semester-plans.module';

@Injectable({
  providedIn: 'any',
})
export class SemesterPlanSetTeacherService extends SemesterPlanBaseService {
  public constructor() {
    super();
  }

  public assign(factory: IRequestBodyFactory): Observable<SemesterPlan> {
    const body = factory.Body;
    return this.httpClient.post<SemesterPlan>(
      `${this.baseApiUri}/setTeacher`,
      body
    );
  }

  public createRequestParamFactory(
    teacher: Teacher,
    plan: SemesterPlan
  ): IRequestBodyFactory {
    return new HttpRequestBody(teacher, plan);
  }
}
class HttpRequestBody implements IRequestBodyFactory {
  private readonly _object: object;

  public constructor(teacher: Teacher, plan: SemesterPlan) {
    this._object = {
      teacher: {
        name: teacher.name,
        surname: teacher.surname,
        thirdname: teacher.thirdname,
      },
      semester: {
        number: plan.semesterNumber,
      },
      discipline: {
        name: plan.disciplineName,
      },
    };
  }

  public get Body(): object {
    return this._object;
  }
}
