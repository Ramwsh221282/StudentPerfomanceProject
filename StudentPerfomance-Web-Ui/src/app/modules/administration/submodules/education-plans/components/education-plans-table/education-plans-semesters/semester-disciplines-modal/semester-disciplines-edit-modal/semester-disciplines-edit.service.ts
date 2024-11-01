import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { SemesterPlan } from '../../../../../../semester-plans/models/semester-plan.interface';
import { Observable } from 'rxjs';
import { BASE_API_URI } from '../../../../../../../../../shared/models/api/api-constants';
import { AuthService } from '../../../../../../../../users/services/auth.service';
import { DirectionPayloadBuilder } from '../../../../../../education-directions/models/contracts/direction-payload-builder';
import { EducationPlanPayloadBuilder } from '../../../../../models/contracts/education-plan-contract/education-plan-payload-builder';
import { SemesterPayloadBuilder } from '../../../../../models/contracts/semester-contract/semester-payload-builder';
import { SemesterPlanPayloadBuilder } from '../models/contracts/semester-plan-contract/semester-plan-payload.builder';
import { TokenPayloadBuilder } from '../../../../../../../../../shared/models/common/token-contract/token-payload-builder';
import { DepartmentPayloadBuilder } from '../../../../../../departments/models/contracts/department-contract/department-payload-builder';
import { TeacherPayloadBuilder } from '../../../../../../teachers/contracts/teacher-contract/teacher-payload-builder';

@Injectable({
  providedIn: 'any',
})
export class SemesterDisciplinesEditService {
  private readonly _httpClient: HttpClient;

  public constructor(private readonly _authService: AuthService) {
    this._httpClient = inject(HttpClient);
  }

  public attachTeacher(semesterPlan: SemesterPlan): Observable<SemesterPlan> {
    console.log(semesterPlan);
    const apiUri: string = `${BASE_API_URI}/api/semester-plans/attach-teacher`;
    const body = {
      direction: DirectionPayloadBuilder(
        semesterPlan.semester.educationPlan.direction
      ),
      plan: EducationPlanPayloadBuilder(semesterPlan.semester.educationPlan),
      semester: SemesterPayloadBuilder(semesterPlan.semester),
      discipline: SemesterPlanPayloadBuilder(semesterPlan),
      department: DepartmentPayloadBuilder(semesterPlan.teacher.department),
      teacher: TeacherPayloadBuilder(semesterPlan.teacher),
      token: TokenPayloadBuilder(this._authService.userData),
    };
    return this._httpClient.post<SemesterPlan>(apiUri, body);
  }

  public deattachTeacher(semesterPlan: SemesterPlan): Observable<SemesterPlan> {
    const apiUri: string = `${BASE_API_URI}/api/semester-plans/deattach-teacher`;
    const body = {
      direction: DirectionPayloadBuilder(
        semesterPlan.semester.educationPlan.direction
      ),
      plan: EducationPlanPayloadBuilder(semesterPlan.semester.educationPlan),
      semester: SemesterPayloadBuilder(semesterPlan.semester),
      discipline: SemesterPlanPayloadBuilder(semesterPlan),
      token: TokenPayloadBuilder(this._authService.userData),
    };
    return this._httpClient.post<SemesterPlan>(apiUri, body);
  }

  public changeName(
    updated: SemesterPlan,
    initial: SemesterPlan
  ): Observable<SemesterPlan> {
    const apiUri: string = `${BASE_API_URI}/api/semester-plans`;
    const body: object = {
      direction: DirectionPayloadBuilder(
        initial.semester.educationPlan.direction
      ),
      plan: EducationPlanPayloadBuilder(initial.semester.educationPlan),
      semester: SemesterPayloadBuilder(initial.semester),
      initial: SemesterPlanPayloadBuilder(initial),
      updated: SemesterPlanPayloadBuilder(updated),
      token: TokenPayloadBuilder(this._authService.userData),
    };
    return this._httpClient.put<SemesterPlan>(apiUri, body);
  }
}
