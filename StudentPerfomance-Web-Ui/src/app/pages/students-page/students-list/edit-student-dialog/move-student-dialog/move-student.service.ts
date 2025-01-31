import { Injectable } from '@angular/core';
import { BaseHttpService } from '../../../../../shared/models/common/base-http/base-http.service';
import { Student } from '../../../../../modules/administration/submodules/students/models/student.interface';
import { StudentGroup } from '../../../../../modules/administration/submodules/student-groups/services/studentsGroup.interface';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'any',
})
export class MoveStudentService extends BaseHttpService {
  public moveStudent(
    student: Student,
    oldGroup: StudentGroup,
    newGroup: StudentGroup,
  ): Observable<Student> {
    const headers = this.buildHttpHeaders();
    const payload = this.buildPayload(student, oldGroup, newGroup);
    const url = `${this._config.baseApiUri}/api/students/change-group`;
    return this._http.put<Student>(url, payload, { headers: headers });
  }

  private buildPayload(
    student: Student,
    oldGroup: StudentGroup,
    newGroup: StudentGroup,
  ): object {
    return {
      studentId: student.id,
      currentGroupId: oldGroup.id,
      otherGroupId: newGroup.id,
    };
  }
}
