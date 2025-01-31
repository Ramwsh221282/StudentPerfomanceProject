import { Injectable } from '@angular/core';
import { SemesterPlan } from '../../../../../../modules/administration/submodules/semester-plans/models/semester-plan.interface';
import { Semester } from '../../../../../../modules/administration/submodules/semesters/models/semester.interface';
import { EducationPlan } from '../../../../../../modules/administration/submodules/education-plans/models/education-plan-interface';
import { EducationDirection } from '../../../../../../modules/administration/submodules/education-directions/models/education-direction-interface';
import { Teacher } from '../../../../../../modules/administration/submodules/teachers/models/teacher.interface';
import { Department } from '../../../../../../modules/administration/submodules/departments/models/departments.interface';
import { DirectionPayloadBuilder } from '../../../../../../modules/administration/submodules/education-directions/models/contracts/direction-payload-builder';
import { EducationPlanPayloadBuilder } from '../../../../../../modules/administration/submodules/education-plans/models/contracts/education-plan-contract/education-plan-payload-builder';
import { SemesterPayloadBuilder } from '../../../../../../modules/administration/submodules/education-plans/models/contracts/semester-contract/semester-payload-builder';
import { TeacherPayloadBuilder } from '../../../../../../modules/administration/submodules/teachers/contracts/teacher-contract/teacher-payload-builder';
import { DepartmentPayloadBuilder } from '../../../../../../modules/administration/submodules/departments/models/contracts/department-contract/department-payload-builder';
import { TokenPayloadBuilder } from '../../../../../../shared/models/common/token-contract/token-payload-builder';
import { Observable } from 'rxjs';
import { BaseHttpService } from '../../../../../../shared/models/common/base-http/base-http.service';
import { SemesterPlanPayloadBuilder } from '../../../create-discipline-dialog/semester-plan-payload-builder';

@Injectable({
  providedIn: 'any',
})
export class DisciplineTeacherAttachmentService extends BaseHttpService {
  public attachTeacher(
    discipline: SemesterPlan,
    semester: Semester,
    educationPlan: EducationPlan,
    direction: EducationDirection,
    teacher: Teacher,
    department: Department,
  ): Observable<SemesterPlan> {
    const url: string = `${this._config.baseApiUri}/api/semester-plans/attach-teacher`;
    const headers = this.buildHttpHeaders();
    const payload = this.buildAttachmentPayload(
      discipline,
      semester,
      educationPlan,
      direction,
      teacher,
      department,
    );
    return this._http.put<SemesterPlan>(url, payload, { headers: headers });
  }

  public detachTeacher(
    discipline: SemesterPlan,
    semester: Semester,
    educationPlan: EducationPlan,
    direction: EducationDirection,
  ): Observable<SemesterPlan> {
    const url: string = `${this._config.baseApiUri}/api/semester-plans/deattach-teacher`;
    const headers = this.buildHttpHeaders();
    const payload = this.buildDetachmentPayload(
      discipline,
      semester,
      educationPlan,
      direction,
    );
    return this._http.put<SemesterPlan>(url, payload, { headers: headers });
  }

  private buildAttachmentPayload(
    discipline: SemesterPlan,
    semester: Semester,
    educationPlan: EducationPlan,
    direction: EducationDirection,
    teacher: Teacher,
    department: Department,
  ): object {
    return {
      direction: DirectionPayloadBuilder(direction),
      plan: EducationPlanPayloadBuilder(educationPlan),
      semester: SemesterPayloadBuilder(semester),
      discipline: SemesterPlanPayloadBuilder(discipline),
      teacher: TeacherPayloadBuilder(teacher),
      department: DepartmentPayloadBuilder(department),
      token: TokenPayloadBuilder(this._auth.userData),
    };
  }

  private buildDetachmentPayload(
    discipline: SemesterPlan,
    semester: Semester,
    educationPlan: EducationPlan,
    direction: EducationDirection,
  ): object {
    return {
      direction: DirectionPayloadBuilder(direction),
      plan: EducationPlanPayloadBuilder(educationPlan),
      semester: SemesterPayloadBuilder(semester),
      discipline: SemesterPlanPayloadBuilder(discipline),
      token: TokenPayloadBuilder(this._auth.userData),
    };
  }
}
