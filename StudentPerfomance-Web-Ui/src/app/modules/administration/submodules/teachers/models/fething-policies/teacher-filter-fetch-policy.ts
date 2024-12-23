import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IFetchPolicy } from '../../../../../../shared/models/fetch-policices/fetch-policy-interface';
import { Teacher } from '../teacher.interface';
import { AppConfigService } from '../../../../../../app.config.service';

//import { BASE_API_URI } from '../../../../../../shared/models/api/api-constants';

export class TeacherFilterFetchPolicy implements IFetchPolicy<Teacher[]> {
  private readonly _apiUri: string;
  private readonly _httpparams: HttpParams;

  public constructor(
    teacher: Teacher,
    private readonly _appConfig: AppConfigService,
  ) {
    this._apiUri = `${this._appConfig.baseApiUri}/teacher/api/read/search`;
    //this._apiUri = `${BASE_API_URI}/teacher/api/read/search`;
    this._httpparams = new HttpParams()
      .set('Teacher.Department.Name', teacher.department.name)
      .set('Teacher.Department.Shortname', teacher.department.acronymus)
      .set('Teacher.Name', teacher.name)
      .set('Teacher.Surname', teacher.surname)
      .set('Teacher.Thirdname', teacher.patronymic)
      .set('Teacher.workingCondition', teacher.state)
      .set('Teacher.jobTitle', teacher.jobTitle);
  }

  public executeFetchPolicy(httpClient: HttpClient): Observable<Teacher[]> {
    const params = this._httpparams;
    return httpClient.post<Teacher[]>(this._apiUri, { params });
  }

  public addPages(page: number, pageSize: number): void {}
}
